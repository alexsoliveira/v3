using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Application.DTO;

namespace TGS.Cartorio.Application.AppServices.Interfaces
{
    public interface ITaxasAppService
    {
        Task<ICollection<SolicitacoesTaxasDto>> BuscarTaxasPorSolicitacao(long idSolicitacao);
        Task<ComposicaoProdutoValorTotalDto> BuscarComposicaoPrecoProdutoTotal(long idSolicitacao);
        Task<decimal> BuscarTaxaPorBoleto();
    }
}