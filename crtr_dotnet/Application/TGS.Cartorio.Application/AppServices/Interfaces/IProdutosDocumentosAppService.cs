using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Application.AppServices.Interfaces
{
    public interface IProdutosDocumentosAppService
    {
        Task Incluir(ProdutosDocumentos produtosdocumentos);
        Task Atualizar(ProdutosDocumentos produtosdocumentos);
        Task<List<ProdutosDocumentos>> BuscarTodos(int pagina = 0);
        Task<List<ProdutosDocumentos>> BuscarTodosComNoLock(int pagina = 0);
        Task<ProdutosDocumentos> BuscarId(int pagina = 0);
    }
}
