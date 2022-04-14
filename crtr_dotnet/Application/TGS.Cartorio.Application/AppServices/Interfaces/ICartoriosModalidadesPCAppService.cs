using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Application.AppServices.Interfaces
{
    public interface ICartoriosModalidadesPcAppService
    {
        Task<List<CartoriosModalidadesPc>> BuscarTodos(int pagina = 0);
        Task<List<CartoriosModalidadesPc>> BuscarTodosComNoLock(int pagina = 0);
    }
}