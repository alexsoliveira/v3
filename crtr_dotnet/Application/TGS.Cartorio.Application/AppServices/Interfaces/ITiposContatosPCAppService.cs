using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Application.AppServices.Interfaces
{
    public interface ITiposContatosPcAppService
    {
        Task<List<TiposContatosPc>> BuscarTodos(int pagina = 0);
        Task<List<TiposContatosPc>> BuscarTodosComNoLock(int pagina = 0);
    }
}