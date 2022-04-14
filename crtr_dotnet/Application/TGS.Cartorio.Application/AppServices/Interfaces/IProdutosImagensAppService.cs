using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Application.AppServices.Interfaces
{
    public interface IProdutosImagensAppService
    {
        Task Incluir(ProdutosImagens produtoimagem);
        Task Atualizar(ProdutosImagens produtoimagem);
        Task<ProdutosImagens> BuscarId(int id);
        Task<List<ProdutosImagens>> BuscarTodos(int pagina = 0);
        Task<List<ProdutosImagens>> BuscarTodosComNoLock(int pagina = 0);
    }
}
