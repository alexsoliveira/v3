
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;


namespace TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer
{
    public interface IProdutosModalidadesPCSqlRepository : ISqlRepository<ProdutosModalidadesPc>
    {
        Task<ProdutosModalidadesPc> BuscarId(int id);
        Task<List<ProdutosModalidadesPc>> BuscarTodos(Expression<Func<ProdutosModalidadesPc, bool>> func, int pagina = 0);
        Task<List<ProdutosModalidadesPc>> BuscarTodos();
        Task<List<ProdutosModalidadesPc>> BuscarTodosComNoLock(Expression<Func<ProdutosModalidadesPc, bool>> func, int pagina = 0);
    }
}