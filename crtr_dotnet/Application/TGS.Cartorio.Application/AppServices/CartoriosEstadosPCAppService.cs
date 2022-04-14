using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Services;


namespace TGS.Cartorio.Application.AppServices
{
    public class CartoriosEstadosPcAppService : ICartoriosEstadosPcAppService
    {
        private readonly ICartoriosEstadosPCService _cartoriosEstadosPCService;
		
        public CartoriosEstadosPcAppService(ICartoriosEstadosPCService cartoriosEstadosPCService)
        {
            _cartoriosEstadosPCService = cartoriosEstadosPCService;
        }
        
        public async Task<List<CartoriosEstadosPc>> BuscarTodos(int pagina)
        {
            return await _cartoriosEstadosPCService.BuscarTodos(pagina);
        }

        public async Task<List<CartoriosEstadosPc>> BuscarTodosComNoLock(int pagina)
        {
            return await _cartoriosEstadosPCService.BuscarTodosComNoLock(pagina);
        }

    }
}