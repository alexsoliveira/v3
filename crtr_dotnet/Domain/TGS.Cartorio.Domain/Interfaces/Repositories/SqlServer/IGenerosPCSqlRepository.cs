
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;


namespace TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer
{
    public interface IGenerosPCSqlRepository : ISqlRepository<GenerosPc>
    {
        Task<List<GenerosPc>> BuscarTodos(Expression<Func<GenerosPc, bool>> func, int pagina = 0);
        Task<List<GenerosPc>> BuscarTodosComNoLock(Expression<Func<GenerosPc, bool>> func, int pagina = 0);
    }
}