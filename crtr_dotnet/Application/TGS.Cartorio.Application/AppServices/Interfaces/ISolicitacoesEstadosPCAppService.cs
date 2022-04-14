using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Application.AppServices.Interfaces
{
    public interface ISolicitacoesEstadosPcAppService
    {
        Task<List<SolicitacoesEstadosPc>> BuscarTodos(int pagina = 0);
        Task<List<SolicitacoesEstadosPc>> BuscarTodosComNoLock(int pagina = 0);
    }
}