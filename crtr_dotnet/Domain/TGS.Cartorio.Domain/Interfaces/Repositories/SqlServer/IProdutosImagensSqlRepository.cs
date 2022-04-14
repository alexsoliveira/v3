using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;


namespace TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer
{
    public interface IProdutosImagensSqlRepository : ISqlRepository<ProdutosImagens>
    {
        Task Incluir(ProdutosImagens produtoimagem);
		Task Atualizar(ProdutosImagens produtoimagem);
        Task<ProdutosImagens> BuscarId(int id);
        Task<List<ProdutosImagens>> BuscarTodos(Expression<Func<ProdutosImagens, bool>> func, int pagina = 0);
        Task<List<ProdutosImagens>> BuscarTodosComNoLock(Expression<Func<ProdutosImagens, bool>> func, int pagina = 0);
    }
}
