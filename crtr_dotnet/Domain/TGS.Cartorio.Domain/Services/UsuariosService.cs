using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Domain.Interfaces.Services;


namespace TGS.Cartorio.Domain.Services
{
    public class UsuariosService : IUsuariosService
    {
        private readonly IUsuariosSqlRepository _usuariosRepository;

        public UsuariosService(IUsuariosSqlRepository usuariosRepository)
        {
            _usuariosRepository = usuariosRepository;
        }

        public async Task Incluir(Usuarios usuario)
        {            
            await _usuariosRepository.Incluir(usuario);
        }
        public async Task Atualizar(Usuarios usuario)
        {
            usuario.DataOperacao = DateTime.Now;
            await _usuariosRepository.Atualizar(usuario);
        }

        public async Task<Usuarios> Buscar(Usuarios usuario)
        {
            // TODO: Validar senha
            return await _usuariosRepository.Buscar(u => u.Email == usuario.Email);
        }

        public async Task<Usuarios> BuscarId(long id)
        {
            return await _usuariosRepository.BuscarId(id);
        }

        public async Task<Usuarios> BuscarPorIdPessoa(long idPessoa)
        {
            return await _usuariosRepository.BuscarPorIdPessoa(idPessoa);
        }

        public async Task<List<Usuarios>> BuscarTodos(Expression<Func<Usuarios, bool>> func, int pagina = 0)
        {
            return await _usuariosRepository.BuscarTodos(func, pagina);
        }

        public async Task<List<Usuarios>> BuscarTodos(int pagina = 0)
        {
            return await _usuariosRepository.BuscarTodos(u => u.FlagAtivo == true,pagina);
        }

        public async Task<List<Usuarios>> BuscarTodosComNoLock(int pagina = 0)
        {
            return await _usuariosRepository.BuscarTodosComNoLock(u => u.FlagAtivo == true, pagina);            
        }

        public async Task<Usuarios> BuscarEmail(string email)
        {
            return await _usuariosRepository.BuscarEmail(email);
        }
    }
}
