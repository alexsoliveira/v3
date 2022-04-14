
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;


namespace TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer
{
    public interface ITaxasExtrasSqlRepository : ISqlRepository<TaxasExtras>
    {
        Task<List<TaxasExtras>> Pesquisar(Expression<Func<TaxasExtras, bool>> func);
        Task<List<TaxasExtras>> BuscarTodos();
        Task<TaxasExtras> Find(long id);
    }
}