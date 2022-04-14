using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Domain.Interfaces.Services;

namespace TGS.Cartorio.Domain.Services
{

    public class CartoriosService : ICartoriosService
    {
        private readonly ICartoriosSqlRepository _cartoriosRepository;

        public CartoriosService(ICartoriosSqlRepository cartoriosRepository)
        {
            _cartoriosRepository = cartoriosRepository;
        }

        public async Task Incluir(Cartorios cartorio)
        {
            await _cartoriosRepository.Incluir(cartorio);
        }
        public async Task Atualizar(Cartorios cartorio)
        {
            cartorio.DataOperacao = DateTime.Now;
            await _cartoriosRepository.Atualizar(cartorio);
        }

        public async Task<Cartorios> BuscarId(int id)
        {
            return await _cartoriosRepository.BuscarId(id);
        }

        public async Task<List<Cartorios>> BuscarTodos(int pagina)
        {
            return await _cartoriosRepository.BuscarTodos(u => true, pagina);
        }

        public async Task<List<Cartorios>> BuscarTodosComNoLock(int pagina)
        {
            return await _cartoriosRepository.BuscarTodosComNoLock(u => true, pagina);
        }

        public Cartorios BuscarUltimoCartorioValido()
        {
            return _cartoriosRepository.BuscarUltimoCartorioValido();
        }

    }
}
