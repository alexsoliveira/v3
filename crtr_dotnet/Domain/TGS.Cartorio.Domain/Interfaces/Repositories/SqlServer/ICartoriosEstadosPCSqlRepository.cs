
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;


namespace TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer
{
    public interface ICartoriosEstadosPCSqlRepository : ISqlRepository<CartoriosEstadosPc>
    {
        Task<List<CartoriosEstadosPc>> BuscarTodos(Expression<Func<CartoriosEstadosPc, bool>> func, int pagina = 0);
        Task<List<CartoriosEstadosPc>> BuscarTodosComNoLock(Expression<Func<CartoriosEstadosPc, bool>> func, int pagina = 0);
    }
}