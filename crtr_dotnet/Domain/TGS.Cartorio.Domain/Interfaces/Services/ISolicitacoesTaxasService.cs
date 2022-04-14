using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Domain.Interfaces.Services
{
    public interface ISolicitacoesTaxasService
    {
        Task Incluir(SolicitacoesTaxas solicitacoesTaxas);
        Task<ICollection<SolicitacoesTaxas>> BuscarPorSolicitacao(long idSolicitacao);
    }
}
