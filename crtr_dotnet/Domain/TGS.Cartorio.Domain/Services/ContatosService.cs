using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Domain.Interfaces.Services;

namespace TGS.Cartorio.Domain.Services
{
	
    public class ContatosService : IContatosService
    {
		private readonly IContatosSqlRepository _contatosRepositorio;

        public ContatosService(IContatosSqlRepository contatosRepository)
        {
            _contatosRepositorio = contatosRepository;
        }

        public async Task Incluir(Contatos contato)
        {
           await _contatosRepositorio.Incluir(contato);
        }
        public async Task Atualizar(Contatos contato)
        {
            contato.DataOperacao = DateTime.Now;
            await _contatosRepositorio.Atualizar(contato);
        }

        public async Task<Contatos> BuscarId(int id)
        {
            return await _contatosRepositorio.BuscarId(id);
        }
        public async Task<List<Contatos>> BuscarTodos(int pagina)
        {
            return await _contatosRepositorio.BuscarTodos(u => true, pagina);
        }

        public async Task<List<Contatos>> BuscarTodosPorUsuario(long idUsuario)
        {
            return await _contatosRepositorio.BuscarTodos(u => u.IdUsuario == idUsuario);
        }

        public async Task<List<Contatos>> BuscarTodosComNoLock(int pagina)
        {
            return await _contatosRepositorio.BuscarTodosComNoLock(u => true, pagina);
        }
    }
}
