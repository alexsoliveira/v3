using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Application.AppServices.Interfaces
{
    public interface ICartoriosEnderecosAppService
    {
        Task Atualizar(CartoriosEnderecos cartorioenderecos);
        Task Incluir(CartoriosEnderecos cartorioenderecos);
        Task<List<CartoriosEnderecos>> BuscarTodos(int pagina = 0);
        Task<List<CartoriosEnderecos>> BuscarTodosComNoLock(int pagina = 0);
        Task<CartoriosEnderecos> BuscarId(int id);
    }
}