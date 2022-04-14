using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;


namespace TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer
{
    public interface ICartoriosEnderecosSqlRepository : ISqlRepository<CartoriosEnderecos>
    {
        Task Atualizar(CartoriosEnderecos cartorioendereco);

        Task Incluir(CartoriosEnderecos cartorioendereco);
        
        Task<CartoriosEnderecos> BuscarId(int id);
        Task<List<CartoriosEnderecos>> BuscarTodos(Expression<Func<CartoriosEnderecos, bool>> func, int pagina = 0);
        Task<List<CartoriosEnderecos>> BuscarTodosComNoLock(Expression<Func<CartoriosEnderecos, bool>> func, int pagina = 0);
    }
}