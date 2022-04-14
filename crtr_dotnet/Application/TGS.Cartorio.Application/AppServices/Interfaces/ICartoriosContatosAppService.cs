using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Application.AppServices.Interfaces
{
    public interface ICartoriosContatosAppService
    {
        Task Atualizar(CartoriosContatos cartoriocontatos);
        Task Incluir(CartoriosContatos cartoriocontatos);
        Task<List<CartoriosContatos>> BuscarTodos(int pagina = 0);
        Task<List<CartoriosContatos>> BuscarTodosComNoLock(int pagina = 0);
        Task<CartoriosContatos> BuscarId(int id);
    }
}