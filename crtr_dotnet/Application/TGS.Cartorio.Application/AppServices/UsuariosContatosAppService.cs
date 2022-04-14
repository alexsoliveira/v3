using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Services;

namespace TGS.Cartorio.Application.AppServices
{
    public class UsuariosContatosAppService : IUsuariosContatosAppService
    {
        private readonly IUsuariosContatosService _usuariosContatosService;       

        public UsuariosContatosAppService(IUsuariosContatosService usuariosContatosService)
        {
            _usuariosContatosService = usuariosContatosService;
        }

        public Task Atualizar(UsuariosContatos usuario)
        {
            return _usuariosContatosService.Atualizar(usuario);
        }

        public async Task<UsuariosContatos> Buscar(UsuariosContatos usuario)
        {
            return await _usuariosContatosService.Buscar(usuario);
        }

        public async Task<UsuariosContatos> BuscarId(int id)
        {
            return await _usuariosContatosService.BuscarId(id);
        }

        public async Task<List<UsuariosContatos>> BuscarTodos(Expression<Func<UsuariosContatos, bool>> func, int pagina = 0)
        {
            return await _usuariosContatosService.BuscarTodos(func, pagina);
        }

        public async Task<List<UsuariosContatos>> BuscarTodos(int pagina = 0)
        {
            return await _usuariosContatosService.BuscarTodos(pagina);
        }

        public async Task<List<UsuariosContatos>> BuscarTodosComNoLock(int pagina = 0)
        {
            return await _usuariosContatosService.BuscarTodosComNoLock(pagina);
        }

        public Task Incluir(UsuariosContatos usuario)
        {
            return _usuariosContatosService.Incluir(usuario);
        }
    }
}