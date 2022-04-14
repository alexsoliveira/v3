using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;


namespace TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer
{
    public interface ITiposPartesPCSqlRepository : ISqlRepository<TiposPartesPc>
    {
        
        Task<List<TiposPartesPc>> BuscarTodos(Expression<Func<TiposPartesPc, bool>> func, int pagina = 0);
        Task<List<TiposPartesPc>> BuscarTodosComNoLock(Expression<Func<TiposPartesPc, bool>> func, int pagina = 0);
    }
}