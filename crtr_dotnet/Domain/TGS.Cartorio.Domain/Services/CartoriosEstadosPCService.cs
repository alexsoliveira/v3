using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Domain.Interfaces.Services;

namespace TGS.Cartorio.Domain.Services
{
    
	  public class CartoriosEstadosPCService : ICartoriosEstadosPCService
    {		    
        private readonly ICartoriosEstadosPCSqlRepository _cartoriosEstadosPcRepository;

        public CartoriosEstadosPCService(ICartoriosEstadosPCSqlRepository cartoriosEstadosPcRepository)
        {
            _cartoriosEstadosPcRepository = cartoriosEstadosPcRepository;
        }

        public async Task<List<CartoriosEstadosPc>> BuscarTodos(int pagina)
        {
            return await _cartoriosEstadosPcRepository.BuscarTodos(u => true, pagina);
        }

        public async Task<List<CartoriosEstadosPc>> BuscarTodosComNoLock(int pagina)
        {
            return await _cartoriosEstadosPcRepository.BuscarTodosComNoLock(u => true, pagina);
        }

    }
}
