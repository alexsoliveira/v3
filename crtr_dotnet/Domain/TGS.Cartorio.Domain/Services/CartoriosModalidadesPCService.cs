using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Domain.Interfaces.Services;

namespace TGS.Cartorio.Domain.Services
{
    
	  public class CartoriosModalidadesPCService : ICartoriosModalidadesPCService
    {		    
        private readonly ICartoriosModalidadesPCSqlRepository _cartoriosModalidadesPcRepository;

        public CartoriosModalidadesPCService(ICartoriosModalidadesPCSqlRepository cartoriosModalidadesPcRepository)
        {
            _cartoriosModalidadesPcRepository = cartoriosModalidadesPcRepository;
        }

        public async Task<List<CartoriosModalidadesPc>> BuscarTodos(int pagina)
        {
            return await _cartoriosModalidadesPcRepository.BuscarTodos(u => true, pagina);
        }

        public async Task<List<CartoriosModalidadesPc>> BuscarTodosComNoLock(int pagina)
        {
            return await _cartoriosModalidadesPcRepository.BuscarTodosComNoLock(u => true, pagina);
        }

    }
}
