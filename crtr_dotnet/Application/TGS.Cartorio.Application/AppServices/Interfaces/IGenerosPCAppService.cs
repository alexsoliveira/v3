using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Application.AppServices.Interfaces
{
    public interface IGenerosPcAppService
    {
        Task<List<GenerosPc>> BuscarTodos(int pagina = 0);
        Task<List<GenerosPc>> BuscarTodosComNoLock(int pagina = 0);
    }
}