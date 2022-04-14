using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Domain.Interfaces.Services;

namespace TGS.Cartorio.Domain.Services
{
    
	  public class GenerosPCService : IGenerosPCService
    {		    
        private readonly IGenerosPCSqlRepository _generosPcRepository;

        public GenerosPCService(IGenerosPCSqlRepository generosPcRepository)
        {
            _generosPcRepository = generosPcRepository;
        }

        public async Task<List<GenerosPc>> BuscarTodos(int pagina)
        {
            return await _generosPcRepository.BuscarTodos(u => true, pagina);
        }

        public async Task<List<GenerosPc>> BuscarTodosComNoLock(int pagina)
        {
            return await _generosPcRepository.BuscarTodosComNoLock(u => true, pagina);
        }

    }
}
