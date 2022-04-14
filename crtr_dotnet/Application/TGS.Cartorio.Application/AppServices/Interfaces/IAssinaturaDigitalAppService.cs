using System.Threading.Tasks;
using TGS.Cartorio.Application.DTO;
using TGS.Cartorio.Domain.Entities.Procuracoes;

namespace TGS.Cartorio.Application.AppServices.Interfaces
{
    public interface IAssinaturaDigitalAppService
    {
        Task<DocumentoParaAssinarDto> AssinarDocumentoPrimeiroPasso(long idMatrimonio, long idPessoaSolicitante, long idUsuario, string base64certificado, byte[] documentoPdf);
        Task<byte[]> AssinarDocumentoSegundoPasso(string signature, string transferData, long idMatrimonioDocumento);
        Task<MatrimoniosDocumentos> ValidacaoDocumento(long idMatrimonioDocumento);
    }
}
