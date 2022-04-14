using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Domain.Interfaces.Services;

namespace TGS.Cartorio.Domain.Services
{
	
    public class EnderecosService : IEnderecosService
    {
		private readonly IEnderecosSqlRepository _enderecosRepositorio;

        public EnderecosService(IEnderecosSqlRepository enderecosRepositorio) 
        {
            _enderecosRepositorio = enderecosRepositorio;
        }

        public async Task Incluir(Enderecos endereco)
        {
            await _enderecosRepositorio.Incluir(endereco);
        }
        public async Task Atualizar(Enderecos endereco)
        {
            endereco.DataOperacao = DateTime.Now;
            await _enderecosRepositorio.Atualizar(endereco);
        }
        public async Task Apagar(int IdEndereco)
        {            
            await _enderecosRepositorio.Apagar(IdEndereco);
        }

        public async Task<Enderecos> Buscar(Enderecos endereco)
        {
            return null;
        }

        public async Task<Enderecos> BuscarId(int id)
        {
            return await _enderecosRepositorio.BuscarId(id);
        }

        public async Task<List<Enderecos>> BuscarTodos(int pagina)
        {
            return await _enderecosRepositorio.BuscarTodos(u => true, pagina);
        }

        public async Task<List<Enderecos>> BuscarTodosPorUsuario(int _IdUsuario)
        {
            return await _enderecosRepositorio.BuscarTodos(u => u.IdUsuario == _IdUsuario);
        }

        public async Task<List<Enderecos>> BuscarTodosComNoLock(int pagina)
        {
            return await _enderecosRepositorio.BuscarTodosComNoLock(u => true, pagina);
        }
    }
}
