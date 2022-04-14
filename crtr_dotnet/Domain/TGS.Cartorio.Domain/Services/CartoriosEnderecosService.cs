using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Domain.Interfaces.Services;

namespace TGS.Cartorio.Domain.Services
{
    
	  public class CartoriosEnderecosService : ICartoriosEnderecosService
    {		    
        private readonly ICartoriosEnderecosSqlRepository _cartoriosEnderecosRepository;

        public CartoriosEnderecosService(ICartoriosEnderecosSqlRepository cartoriosEnderecosRepository)
        {
            _cartoriosEnderecosRepository = cartoriosEnderecosRepository;
        }

        public async Task Incluir(CartoriosEnderecos cartorioenderecos)
        {
            await _cartoriosEnderecosRepository.Incluir(cartorioenderecos);
        }
        public async Task Atualizar(CartoriosEnderecos cartorioenderecos)
        {
            cartorioenderecos.DataOperacao = DateTime.Now;
            await _cartoriosEnderecosRepository.Atualizar(cartorioenderecos);
        }

        public async Task<CartoriosEnderecos> BuscarId(int id)
        {
            return await _cartoriosEnderecosRepository.BuscarId(id);
        }

        public async Task<List<CartoriosEnderecos>> BuscarTodos(int pagina)
        {
            return await _cartoriosEnderecosRepository.BuscarTodos(u => true, pagina);
        }

        public async Task<List<CartoriosEnderecos>> BuscarTodosComNoLock(int pagina)
        {
            return await _cartoriosEnderecosRepository.BuscarTodosComNoLock(u => true, pagina);
        }

    }
}
