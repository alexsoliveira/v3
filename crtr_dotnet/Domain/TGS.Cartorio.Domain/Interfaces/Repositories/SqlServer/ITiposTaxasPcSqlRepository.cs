using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;


namespace TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer
{
    public interface ITiposTaxasPcSqlRepository : ISqlRepository<TiposTaxasPc>
    {
        Task<List<TiposTaxasPc>> BuscarTodos();
        Task<TiposTaxasPc> Find(int id);
    }
}