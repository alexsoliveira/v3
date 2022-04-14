using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Services;


namespace TGS.Cartorio.Application.AppServices
{
    public class TiposDocumentosPcAppService : ITiposDocumentosPcAppService
    {
        private readonly ITiposDocumentosPCService _tiposDocumentosPCService;
		
        public TiposDocumentosPcAppService(ITiposDocumentosPCService tiposDocumentosPCService)
        {
            _tiposDocumentosPCService = tiposDocumentosPCService;
        }
        
        public async Task<List<TiposDocumentosPc>> BuscarTodos(int pagina)
        {
            return await _tiposDocumentosPCService.BuscarTodos(pagina);
        }

        public async Task<List<TiposDocumentosPc>> BuscarTodosComNoLock(int pagina)
        {
            return await _tiposDocumentosPCService.BuscarTodosComNoLock(pagina);
        }

        public async Task<TiposDocumentosPc> BuscarId(int IdTipoDocumento)
        {
            return await _tiposDocumentosPCService.BuscarId(IdTipoDocumento);
        }
    }
}