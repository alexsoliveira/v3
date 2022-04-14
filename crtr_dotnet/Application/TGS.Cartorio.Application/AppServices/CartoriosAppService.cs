using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Services;


namespace TGS.Cartorio.Application.AppServices
{
    public class CartoriosAppService : ICartoriosAppService
    {
        private readonly ICartoriosService _cartorioService;
        public CartoriosAppService(ICartoriosService cartorioService)
        {
            _cartorioService = cartorioService;
        }
        public async Task Incluir(Cartorios cartorio)
        {
            await _cartorioService.Incluir(cartorio);
        }

        public async Task<Cartorios> BuscarId(int id)
        {
            return await _cartorioService.BuscarId(id);
        }

        public async Task<List<Cartorios>> BuscarTodos(int pagina)
        {
            return await _cartorioService.BuscarTodos(pagina);
        }

        public async Task<List<Cartorios>> BuscarTodosComNoLock(int pagina)
        {
            return await _cartorioService.BuscarTodosComNoLock(pagina);
        }

        public async Task Atualizar(Cartorios cartorio)
        {
            await _cartorioService.Atualizar(cartorio);
        }
    }
}