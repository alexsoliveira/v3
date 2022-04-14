using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Domain.Interfaces.Services;

namespace TGS.Cartorio.Domain.Services
{
    
	  public class TiposDocumentosPCService : ITiposDocumentosPCService
    {		    
        private readonly ITiposDocumentosPCSqlRepository _tiposDocumentosPcRepository;

        public TiposDocumentosPCService(ITiposDocumentosPCSqlRepository tiposDocumentosPcRepository)
        {
            _tiposDocumentosPcRepository = tiposDocumentosPcRepository;
        }

        public async Task<List<TiposDocumentosPc>> BuscarTodos(int pagina)
        {
            return await _tiposDocumentosPcRepository.BuscarTodos(u => true, pagina);
        }

        public async Task<List<TiposDocumentosPc>> BuscarTodosComNoLock(int pagina)
        {
            return await _tiposDocumentosPcRepository.BuscarTodosComNoLock(u => true, pagina);
        }

        public async Task<TiposDocumentosPc> BuscarId(int IdTipoDocumento)
        {
            return await _tiposDocumentosPcRepository.BuscarId(IdTipoDocumento);
        }
    }
}
