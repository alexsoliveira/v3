using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;


namespace TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer
{
    public interface ISolicitacoesTaxasSqlRepository : ISqlRepository<SolicitacoesTaxas>
    {
        Task Incluir(SolicitacoesTaxas solicitacoesTaxas);
		Task<ICollection<SolicitacoesTaxas>> BuscarPorSolicitacao(long idSolicitacao);
    }
}