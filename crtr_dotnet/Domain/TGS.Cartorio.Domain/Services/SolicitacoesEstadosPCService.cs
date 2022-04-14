using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Domain.Interfaces.Services;

namespace TGS.Cartorio.Domain.Services
{
    
	  public class SolicitacoesEstadosPCService : ISolicitacoesEstadosPCService
    {		    
        private readonly ISolicitacoesEstadosPCSqlRepository _solicitacoesEstadosPcRepository;

        public SolicitacoesEstadosPCService(ISolicitacoesEstadosPCSqlRepository solicitacoesEstadosPcRepository)
        {
            _solicitacoesEstadosPcRepository = solicitacoesEstadosPcRepository;
        }

        public async Task<List<SolicitacoesEstadosPc>> BuscarTodos(int pagina)
        {
            return await _solicitacoesEstadosPcRepository.BuscarTodos(u => true, pagina);
        }

        public async Task<List<SolicitacoesEstadosPc>> BuscarTodosComNoLock(int pagina)
        {
            return await _solicitacoesEstadosPcRepository.BuscarTodosComNoLock(u => true, pagina);
        }

    }
}
