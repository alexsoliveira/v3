using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;


namespace TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer
{
    public interface ICartoriosContatosSqlRepository : ISqlRepository<CartoriosContatos>
    {
        Task Atualizar(CartoriosContatos cartorio);
        Task Incluir(CartoriosContatos cartorio);
        Task<CartoriosContatos> BuscarId(int id);
        Task<List<CartoriosContatos>> BuscarTodos(Expression<Func<CartoriosContatos, bool>> func, int pagina = 0);
        Task<List<CartoriosContatos>> BuscarTodosComNoLock(Expression<Func<CartoriosContatos, bool>> func, int pagina = 0);
    }
}