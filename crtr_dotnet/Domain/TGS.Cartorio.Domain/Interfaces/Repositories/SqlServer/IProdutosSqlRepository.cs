using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;


namespace TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer
{
    public interface IProdutosSqlRepository : ISqlRepository<Produtos>
    {
        Task Incluir(Produtos produto);
		Task Atualizar(Produtos produto);
        Task<List<Produtos>> BuscarTodos(Expression<Func<Produtos, bool>> func, int pagina = 0);
        Task<List<ProdutosCategoriasPc>> BuscarDadosVitrine();
        Task<List<Produtos>> BuscarTodosComNoLock(Expression<Func<Produtos, bool>> func, int pagina = 0);
        Task<Produtos>BuscarId(int id);
    }
}