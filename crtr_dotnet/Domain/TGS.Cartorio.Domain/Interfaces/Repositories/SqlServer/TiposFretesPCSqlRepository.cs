using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer
{
    public interface ITiposFretesPCSqlRepository : ISqlRepository<TiposFretesPc>
    {
        Task<List<TiposFretesPc>> BuscarTodos(Expression<Func<TiposFretesPc, bool>> func, int pagina = 0);
        Task<List<TiposFretesPc>> BuscarTodosComNoLock(Expression<Func<TiposFretesPc, bool>> func, int pagina = 0);
    }
}