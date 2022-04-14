using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Services;


namespace TGS.Cartorio.Application.AppServices
{
    public class CartoriosEnderecosAppService : ICartoriosEnderecosAppService
    {
        private readonly ICartoriosEnderecosService _cartorioenderecoService;
		
        public CartoriosEnderecosAppService(ICartoriosEnderecosService cartorioenderecoService)
        {
            _cartorioenderecoService = cartorioenderecoService;
        }
        public async Task Incluir(CartoriosEnderecos cartorioendereco)
        {
            await _cartorioenderecoService.Incluir(cartorioendereco);
        }

        public async Task<CartoriosEnderecos> BuscarId(int id)
        {
            return await _cartorioenderecoService.BuscarId(id);
        }
        public async Task<List<CartoriosEnderecos>> BuscarTodos(int pagina)
        {
            return await _cartorioenderecoService.BuscarTodos(pagina);
        }

        public async Task<List<CartoriosEnderecos>> BuscarTodosComNoLock(int pagina)
        {
            return await _cartorioenderecoService.BuscarTodosComNoLock(pagina);
        }

        public async Task Atualizar(CartoriosEnderecos cartorioendereco)
        {
            await _cartorioenderecoService.Atualizar(cartorioendereco);
        }
      
    }
}