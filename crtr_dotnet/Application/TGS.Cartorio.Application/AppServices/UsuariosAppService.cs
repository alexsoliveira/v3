using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Application.DTO;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Services;
using TGS.Cartorio.Infrastructure.Utility.ApiClient;

namespace TGS.Cartorio.Application.AppServices
{
    public class UsuariosAppService : IUsuariosAppService
    {
        private readonly IUsuariosService _usuarioService;
        private readonly ILogSistemaAppService _logSistemaAppService;

        public UsuariosAppService(IUsuariosService usuarioService, ILogSistemaAppService logSistemaAppService)
        {
            _usuarioService = usuarioService;
            _logSistemaAppService = logSistemaAppService;
        }

        public async Task Incluir(Usuarios usuario)
        {
            // TODO: Verificar se usuário existe na base
            // usuario.IdUsuario = _usuarioService.Incluir(usuario);
            await _usuarioService.Incluir(usuario);                                
        }
        public async Task Atualizar(Usuarios usuario)
        {
            await _usuarioService.Atualizar(usuario);
        }
        public async Task<Usuarios> Buscar(Usuarios usuario)
        {
            // TODO: Criar validações

            return await _usuarioService.Buscar(usuario);
        }

        public async Task<Usuarios> BuscarId(long id)
        {
            return await _usuarioService.BuscarId(id);
        }

        public async Task<Usuarios> BuscarPorIdPessoa(long idPessoa)
        {
            try
            {
                return await _usuarioService.BuscarPorIdPessoa(idPessoa);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_UsuariosAppService_BuscarPorIdPessoa, new
                {
                    IdPessoa = idPessoa
                }, ex);

                throw;
            }
        }

        public async Task<List<Usuarios>> BuscarTodos(int pagina = 0)
        {
            // TODO: Obter todos os usuários com paginação
            return await _usuarioService.BuscarTodos(pagina);
        }


        public async Task<List<Usuarios>> BuscarTodos(Expression<Func<Usuarios, bool>> func, int pagina = 0)
        {
            // TODO: Obter todos os usuários com paginação
            return await _usuarioService.BuscarTodos(func,pagina);
        }

        public async Task<List<Usuarios>> BuscarTodosComNoLock(int pagina = 0)
        {
            // TODO: Obter todos os usuários com paginação sem no lock
            return await _usuarioService.BuscarTodosComNoLock(pagina);            
        }        
    }
}