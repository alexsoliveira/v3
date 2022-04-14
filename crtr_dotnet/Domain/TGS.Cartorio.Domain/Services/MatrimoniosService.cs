using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities.Procuracoes;
using TGS.Cartorio.Domain.Entities.Procuracoes.Produtos.Matrimonio;
using TGS.Cartorio.Domain.Enumerables;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer.Procuracoes;
using TGS.Cartorio.Domain.Interfaces.Services;

namespace TGS.Cartorio.Domain.Services
{
    public class MatrimoniosService : IMatrimoniosService
    {
        private readonly IMatrimoniosSqlRepository _matrimoniosSqlRepository;
        private readonly IMatrimoniosDocumentosService _matrimoniosDocumentosService;
        public MatrimoniosService(IMatrimoniosSqlRepository matrimoniosSqlRepository, IMatrimoniosDocumentosService matrimoniosDocumentosService)
        {
            _matrimoniosSqlRepository = matrimoniosSqlRepository;
            _matrimoniosDocumentosService = matrimoniosDocumentosService;
        }

        public async Task<long> Incluir(DadosMatrimonio matrimonio)
        {
            try
            {
                byte[] docProclamas = GetDocProclamasStringToBase64(matrimonio);

                DateTime dataOperacao = DateTime.Now;
                Matrimonios matriminio = new Matrimonios
                {
                    IdSolicitacao = matrimonio.IdSolicitacao,
                    IdUsuario = matrimonio.IdUsuario,
                    CamposJson = JsonConvert.SerializeObject(matrimonio),
                    DataOperacao = dataOperacao
                };
                await _matrimoniosSqlRepository.Incluir(matriminio);

                if (docProclamas != null
                    && matriminio.IdMatrimonio > 0)
                {
                    await _matrimoniosDocumentosService.Incluir(new MatrimoniosDocumentos
                    {
                        BlobConteudo = docProclamas,
                        DataOperacao = dataOperacao,
                        IdMatrimonio = matriminio.IdMatrimonio,
                        IdTipoDocumento = (int)EMatrimoniosTiposDocumentos.Proclamas,
                        IdUsuario = matrimonio.IdUsuario
                    });
                }

                return matriminio.IdMatrimonio;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<long> Atualizar(DadosMatrimonio dadosMatrimonio)
        {
            try
            {
                byte[] docProclamas = GetDocProclamasStringToBase64(dadosMatrimonio);
                var matrimonio = await _matrimoniosSqlRepository.BuscarPorSolicitacao(dadosMatrimonio.IdSolicitacao);
                matrimonio.CamposJson = JsonConvert.SerializeObject(dadosMatrimonio);
                await _matrimoniosSqlRepository.Atualizar(matrimonio);

                var docs = await _matrimoniosDocumentosService.BuscarPorMatrimonio(matrimonio.IdMatrimonio);
                if (docs != null
                    && docs.Count() > 0
                    && docs.Any(d => d.IdTipoDocumento == (int)EMatrimoniosTiposDocumentos.Proclamas))
                    await _matrimoniosDocumentosService.Remover(docs.FirstOrDefault(d => d.IdTipoDocumento == (int)EMatrimoniosTiposDocumentos.Proclamas));

                if (docProclamas != null)
                    await _matrimoniosDocumentosService.Incluir(new MatrimoniosDocumentos
                    {
                        BlobConteudo = docProclamas,
                        DataOperacao = DateTime.Now,
                        IdMatrimonio = matrimonio.IdMatrimonio,
                        IdTipoDocumento = (int)EMatrimoniosTiposDocumentos.Proclamas,
                        IdUsuario = matrimonio.IdUsuario
                    });

                return matrimonio.IdMatrimonio;

            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<bool> Existe(long idMatrimonio)
        {
            try
            {
                return await _matrimoniosSqlRepository.Existe(idMatrimonio);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Matrimonios> BuscarPorSolicitacao(long idSolicitacao)
        {
            try
            {
                return await _matrimoniosSqlRepository.BuscarPorSolicitacao(idSolicitacao);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Matrimonios BuscarPorSolicitacaoByJob(long idSolicitacao)
        {
            try
            {
                return _matrimoniosSqlRepository.BuscarPorSolicitacaoByJob(idSolicitacao);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Matrimonios> BuscarPorId(long idMatrimonio)
        {
            try
            {
                return await _matrimoniosSqlRepository.BuscarPorId(idMatrimonio);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private byte[] GetDocProclamasStringToBase64(DadosMatrimonio dadosMatrimonio)
        {
            try
            {
                if (!string.IsNullOrEmpty(dadosMatrimonio.DadosContracaoMatrimonio.DocumentoProclamas))
                {
                    //faço isso para o hash do base64 não ser serializado!!!!
                    string docProclamas = dadosMatrimonio.DadosContracaoMatrimonio.DocumentoProclamas
                                                         .Replace("data:application/pdf;base64,", "");
                    dadosMatrimonio.DadosContracaoMatrimonio.DocumentoProclamas = null;
                    return Convert.FromBase64String(docProclamas);
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
