
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;


namespace TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer
{
    public interface ICartoriosModalidadesPCSqlRepository : ISqlRepository<CartoriosModalidadesPc>
    {
        Task<List<CartoriosModalidadesPc>> BuscarTodos(Expression<Func<CartoriosModalidadesPc, bool>> func, int pagina = 0);
        Task<List<CartoriosModalidadesPc>> BuscarTodosComNoLock(Expression<Func<CartoriosModalidadesPc, bool>> func, int pagina = 0);
    }
}