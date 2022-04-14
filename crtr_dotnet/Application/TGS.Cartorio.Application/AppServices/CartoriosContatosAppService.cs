using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Services;


namespace TGS.Cartorio.Application.AppServices
{
    public class CartoriosContatosAppService : ICartoriosContatosAppService
    {
        private readonly ICartoriosContatosService _cartoriocontatoService;
        public CartoriosContatosAppService(ICartoriosContatosService cartoriocontatoService)
        {
            _cartoriocontatoService = cartoriocontatoService;
        }
        public async Task Incluir(CartoriosContatos cartoriocontato)
        {
            await _cartoriocontatoService.Incluir(cartoriocontato);
        }


        public async Task<CartoriosContatos> BuscarId(int id)
        {
            return await _cartoriocontatoService.BuscarId(id);
        }

        public async Task<List<CartoriosContatos>> BuscarTodos(int pagina)
        {
            return await _cartoriocontatoService.BuscarTodos(pagina);
        }

        public async Task<List<CartoriosContatos>> BuscarTodosComNoLock(int pagina)
        {
            return await _cartoriocontatoService.BuscarTodosComNoLock(pagina);
        }

        public async Task Atualizar(CartoriosContatos cartoriocontato)
        {
            await _cartoriocontatoService.Atualizar(cartoriocontato);
        }


       
    }
}