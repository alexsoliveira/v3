using System.Threading.Tasks;
using TGS.Cartorio.Application.DTO.Products.Matrimonio;

namespace TGS.Cartorio.Application.AppServices.Interfaces
{
    public interface IMatrimoniosDocumentosAppService
    {
        Task Incluir(DadosMatrimonioDto motrimoniosDto);
        Task Atualizar(DadosMatrimonioDto matrimoniosDto);
        Task<string> BuscarPorSolicitacao(long idSolicitacao);
    }
}
