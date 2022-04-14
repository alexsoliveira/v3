using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Services;

namespace TGS.Cartorio.Application.AppServices
{
    public class ContatosAppService : IContatosAppService
    {
        private readonly IContatosService _contatoService;

        public ContatosAppService(IContatosService contatoService)
        {
            _contatoService = contatoService;
        }

        public async Task Incluir(Contatos contato)
        {
            await _contatoService.Incluir(contato);
        }

        public async Task Atualizar(Contatos contato)
        {
            await _contatoService.Atualizar(contato);
        }

        
        public async Task<Contatos> BuscarId(int id)
        {
            return await _contatoService.BuscarId(id);
        }


        public async Task<List<Contatos>> BuscarTodos(int pagina)
        {
            return await _contatoService.BuscarTodos(pagina);
        }


        public async Task<List<Contatos>> BuscarTodosComNoLock(int pagina)
        {
            return await _contatoService.BuscarTodosComNoLock(pagina);
        }

        public async Task<List<Contatos>> BuscarTodosPorUsuario(long idUsuario)
        {
            return await _contatoService.BuscarTodosPorUsuario(idUsuario);
        }
    }
}
