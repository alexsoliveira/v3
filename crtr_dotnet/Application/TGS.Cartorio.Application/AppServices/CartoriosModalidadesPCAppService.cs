using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Services;


namespace TGS.Cartorio.Application.AppServices
{
    public class CartoriosModalidadesPcAppService : ICartoriosModalidadesPcAppService
    {
        private readonly ICartoriosModalidadesPCService _cartoriosModalidadesPCService;
		
        public CartoriosModalidadesPcAppService(ICartoriosModalidadesPCService cartoriosModalidadesPCService)
        {
            _cartoriosModalidadesPCService = cartoriosModalidadesPCService;
        }

        public async Task<List<CartoriosModalidadesPc>> BuscarTodos(int pagina)
        {
            return await _cartoriosModalidadesPCService.BuscarTodos(pagina);
        }

        public async Task<List<CartoriosModalidadesPc>> BuscarTodosComNoLock(int pagina)
        {
            return await _cartoriosModalidadesPCService.BuscarTodosComNoLock(pagina);
        }

    }
}