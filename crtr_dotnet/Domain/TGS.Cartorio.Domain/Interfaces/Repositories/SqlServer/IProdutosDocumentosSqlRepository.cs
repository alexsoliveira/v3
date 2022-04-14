using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;


namespace TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer
{
    public interface IProdutosDocumentosSqlRepository : ISqlRepository<ProdutosDocumentos>
    {
        Task Incluir(ProdutosDocumentos produtosdocumentos);
		Task Atualizar(ProdutosDocumentos produtosdocumentos);
        Task<ProdutosDocumentos> BuscarId(int id);

        Task<List<ProdutosDocumentos>> BuscarTodos(Expression<Func<ProdutosDocumentos, bool>> func, int pagina = 0);
        Task<List<ProdutosDocumentos>> BuscarTodosComNoLock(Expression<Func<ProdutosDocumentos, bool>> func, int pagina = 0);
    }
}
