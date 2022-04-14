using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Services;


namespace TGS.Cartorio.Application.AppServices
{
    public class TiposContatosPcAppService : ITiposContatosPcAppService
    {
        private readonly ITiposContatosPCService _tiposContatosPCService;
		
        public TiposContatosPcAppService(ITiposContatosPCService tiposContatosPCService)
        {
            _tiposContatosPCService = tiposContatosPCService;
        }
        
        public async Task<List<TiposContatosPc>> BuscarTodos(int pagina)
        {
            return await _tiposContatosPCService.BuscarTodos(pagina);
        }

        public async Task<List<TiposContatosPc>> BuscarTodosComNoLock(int pagina)
        {
            return await _tiposContatosPCService.BuscarTodosComNoLock(pagina);
        }

    }
}