using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Application.AppServices.Interfaces
{
    public interface IProdutosModalidadesPcAppService
    {
        Task<ProdutosModalidadesPc> BuscarId(int id);
        Task<List<ProdutosModalidadesPc>> BuscarTodos(int pagina = 0);
        Task<List<ProdutosModalidadesPc>> BuscarTodos();
        Task<List<ProdutosModalidadesPc>> BuscarTodosComNoLock(int pagina = 0);
    }
}