using Lacuna.Pki;
using Lacuna.Pki.Pades;
using System;
using System.Threading.Tasks;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Application.CertificadoDigital.interfaces;
using TGS.Cartorio.Application.DTO;
using TGS.Cartorio.Application.Enumerables;
using TGS.Cartorio.Domain.Entities.Procuracoes;
using TGS.Cartorio.Domain.Interfaces.Services;

namespace TGS.Cartorio.Application.AppServices
{
    public class AssinaturaDigitalAppService : IAssinaturaDigitalAppService
    {
        private readonly IRepresentacaoVisual _representacaoVisual;
        private readonly IPadesPolicy _padesPolicy;
        private readonly IMatrimoniosDocumentosService _matrimoniosDocumentosService;
        private readonly IMatrimoniosService _matrimoniosService;
        private readonly IProcuracoesPartesService _procuracoesPartesService;
        private readonly IProcuracoesPartesEstadosService _procuracoesPartesEstadosService;

        public AssinaturaDigitalAppService(IRepresentacaoVisual representacaoVisual,
            IPadesPolicy padesPolicy,
            IMatrimoniosDocumentosService matrimoniosDocumentosService,
            IMatrimoniosService matrimoniosService, 
            IProcuracoesPartesService procuracoesPartesService, 
            IProcuracoesPartesEstadosService procuracoesPartesEstadosService)
        {
            _representacaoVisual = representacaoVisual;
            _padesPolicy = padesPolicy;
            _matrimoniosDocumentosService = matrimoniosDocumentosService;
            _matrimoniosService = matrimoniosService;
            _procuracoesPartesService = procuracoesPartesService;
            _procuracoesPartesEstadosService = procuracoesPartesEstadosService;
        }

        public async Task<DocumentoParaAssinarDto> AssinarDocumentoPrimeiroPasso(long idMatrimonio, long idPessoaSolicitante, long idUsuario, string base64certificado, byte[] documentoPdf)
        {
            try
            {
                SignatureAlgorithm sigAlgorithm;
                byte[] transData, toSign;

                PKCertificate pkCertificate = PKCertificate.Decode(base64certificado);

                var signer = new PadesSigner();
                signer.SetPdfToSign(documentoPdf);
                signer.SetSigningCertificate(pkCertificate);
                signer.SetPolicy(_padesPolicy.GetTrustArbitrator());

                if (!await _procuracoesPartesService.ValidarSeExisteProcuracaoParteComMatrimonio(idPessoaSolicitante, idMatrimonio))
                    throw new Exception("Ocorreu um erro ao buscar as informações da solicitação!");

                var matrimonioDocumento = await _matrimoniosDocumentosService.BuscarPorMatrimonioComProcuracaoParte(idMatrimonio, idPessoaSolicitante);
                if (matrimonioDocumento == null)
                {
                    var procuracaoParteSolicitante = await _procuracoesPartesService.BuscarSolicitantePorMatrimonio(idMatrimonio, idPessoaSolicitante);
                    if (procuracaoParteSolicitante == null)
                        throw new Exception("Ocorreu um erro ao buscar as informações do responsável da solicitação!");

                    matrimonioDocumento = new MatrimoniosDocumentos(idMatrimonio,
                    procuracaoParteSolicitante.IdProcuracaoParte,
                    idUsuario,
                    (int)TiposDocumentosMatrimonio.RG,
                    documentoPdf);

                    await _matrimoniosDocumentosService.Incluir(matrimonioDocumento);
                }

                if (matrimonioDocumento == null)
                    throw new Exception("Ocorreu um erro ao tentar criar um novo Matrimonio Documento!");

                signer.SetVisualRepresentation(_representacaoVisual.Set(pkCertificate: pkCertificate, idValidadorLong: matrimonioDocumento.IdMatrimonioDocumento));
                
                toSign = signer.GetToSignBytes(out sigAlgorithm, out transData);

                return new DocumentoParaAssinarDto
                {
                    DigestAlgorithmOid = sigAlgorithm.DigestAlgorithm.Oid,
                    ToSignHashBase64 = Convert.ToBase64String(sigAlgorithm.DigestAlgorithm.ComputeHash(toSign)),
                    TranferDataBase64 = Convert.ToBase64String(transData),
                    IdMatrimonioDocumento = matrimonioDocumento.IdMatrimonioDocumento
                };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<byte[]> AssinarDocumentoSegundoPasso(string signature, string transferData, long idMatrimonioDocumento)
        {
            try
            {
                var signer = new PadesSigner();

                var bytesSignature = Convert.FromBase64String(signature);
                var bytesTransferData = Convert.FromBase64String(transferData);
                
                signer.SetPreComputedSignature(bytesSignature, bytesTransferData);
                signer.SetPolicy(_padesPolicy.GetTrustArbitrator());
                signer.ComputeSignature();
                var docAssinado = signer.GetPadesSignature();


                var matrimonioDocumento = await _matrimoniosDocumentosService.BuscarPorId(idMatrimonioDocumento);
                if (matrimonioDocumento == null)
                    throw new Exception("Ocorreu um erro ao tentar buscar pelo Matrimonio Documento!");

                matrimonioDocumento.BlobAssinaturaDigital = docAssinado;
                await _matrimoniosDocumentosService.Atualizar(matrimonioDocumento);

                await _procuracoesPartesEstadosService.Incluir(ProcuracoesPartesEstados.Create(
                    matrimonioDocumento.IdProcuracaoParte.Value,
                    (int)ProcuracaoParteEstado.DocumentosAssinados
                ));

                return docAssinado;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<MatrimoniosDocumentos> ValidacaoDocumento(long idMatrimonioDocumento)
        {
            try
            {
                var matrimonioDocumento = await _matrimoniosDocumentosService.BuscarPorId(idMatrimonioDocumento);
                if (matrimonioDocumento == null)
                    throw new Exception("Ocorreu um erro ao tentar buscar pelo Matrimonio Documento!");
                return matrimonioDocumento;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
