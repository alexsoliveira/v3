using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Domain.Interfaces.Services;

namespace TGS.Cartorio.Domain.Services
{
    
	  public class CartoriosContatosService : ICartoriosContatosService
    {		    
        private readonly ICartoriosContatosSqlRepository _cartorioscontatosRepository;

        public CartoriosContatosService(ICartoriosContatosSqlRepository cartorioscontatosRepository)
        {
            _cartorioscontatosRepository = cartorioscontatosRepository;
        }

        public async Task Incluir(CartoriosContatos cartoriocontatos)
        {
            await _cartorioscontatosRepository.Incluir(cartoriocontatos);
        }

        public async Task Atualizar(CartoriosContatos cartoriocontatos)
        {
            cartoriocontatos.DataOperacao = DateTime.Now;
            await _cartorioscontatosRepository.Atualizar(cartoriocontatos);
        }

        public async Task<CartoriosContatos> BuscarId(int id)
        {
            return await _cartorioscontatosRepository.BuscarId(id);
        }

        public async Task<List<CartoriosContatos>> BuscarTodos(int pagina)
        {
            return await _cartorioscontatosRepository.BuscarTodos(u => true, pagina);
        }

        public async Task<List<CartoriosContatos>> BuscarTodosComNoLock(int pagina)
        {
            return await _cartorioscontatosRepository.BuscarTodosComNoLock(u => true, pagina);
        }

    }
}
