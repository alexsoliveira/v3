using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Domain.Interfaces.Services;

namespace TGS.Cartorio.Domain.Services
{
    
	  public class TiposContatosPCService : ITiposContatosPCService
    {		    
        private readonly ITiposContatosPCSqlRepository _tiposContatosPcRepository;

        public TiposContatosPCService(ITiposContatosPCSqlRepository tiposContatosPcRepository)
        {
            _tiposContatosPcRepository = tiposContatosPcRepository;
        }

        public async Task<List<TiposContatosPc>> BuscarTodos(int pagina)
        {
            return await _tiposContatosPcRepository.BuscarTodos(u => true, pagina);
        }

        public async Task<List<TiposContatosPc>> BuscarTodosComNoLock(int pagina)
        {
            return await _tiposContatosPcRepository.BuscarTodosComNoLock(u => true, pagina);
        }

    }
}
