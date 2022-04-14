
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;


namespace TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer
{
    public interface ITiposContatosPCSqlRepository : ISqlRepository<TiposContatosPc>
    {
        Task<List<TiposContatosPc>> BuscarTodos(Expression<Func<TiposContatosPc, bool>> func, int pagina = 0);
        Task<List<TiposContatosPc>> BuscarTodosComNoLock(Expression<Func<TiposContatosPc, bool>> func, int pagina = 0);
    }
}