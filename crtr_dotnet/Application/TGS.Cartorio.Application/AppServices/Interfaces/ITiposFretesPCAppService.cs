using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Application.AppServices.Interfaces
{
    public interface ITiposFretesPCAppService
    {
        Task<List<TiposFretesPc>> BuscarTodos(int pagina = 0);
        Task<List<TiposFretesPc>> BuscarTodosComNoLock(int pagina = 0);

        //Task<object> BuscarCustos(string cep);
    }
}