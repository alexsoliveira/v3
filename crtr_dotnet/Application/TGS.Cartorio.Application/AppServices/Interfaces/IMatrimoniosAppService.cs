using System.Threading.Tasks;
using TGS.Cartorio.Application.DTO.Products.Matrimonio;

namespace TGS.Cartorio.Application.AppServices.Interfaces
{
    public interface IMatrimoniosAppService
    {
        Task<long> Incluir(DadosMatrimonioDto motrimoniosDto);
        Task<long> Atualizar(DadosMatrimonioDto matrimoniosDto);
        Task<string> BuscarPorSolicitacao(long idSolicitacao);
    }
}
