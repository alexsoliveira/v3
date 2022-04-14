using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Application.AppServices.Interfaces
{
    public interface ICartoriosEstadosPcAppService
    {
        Task<List<CartoriosEstadosPc>> BuscarTodos(int pagina = 0);
        Task<List<CartoriosEstadosPc>> BuscarTodosComNoLock(int pagina = 0);
    }
}