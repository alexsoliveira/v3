using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Domain.Interfaces.Services;

namespace TGS.Cartorio.Domain.Services
{
	public class UsuariosContatosService: IUsuariosContatosService
    {
        private readonly IUsuariosContatosSqlRepository _usuariosContatosSqlRepository;

        public UsuariosContatosService(IUsuariosContatosSqlRepository usuariosContatosSqlRepository)
        {
            _usuariosContatosSqlRepository = usuariosContatosSqlRepository;
        }

        public async Task Incluir(UsuariosContatos usuario)
        {
            await _usuariosContatosSqlRepository.Incluir(usuario);
        }
        public async Task Atualizar(UsuariosContatos usuario)
        {
            usuario.DataOperacao = DateTime.Now;
            await _usuariosContatosSqlRepository.Atualizar(usuario);
        }

        public async Task<UsuariosContatos> Buscar(UsuariosContatos usuario)
        {
            // TODO: Validar senha
            return await _usuariosContatosSqlRepository.Buscar(u => true);
        }

        public async Task<UsuariosContatos> BuscarId(int id)
        {
            return await _usuariosContatosSqlRepository.BuscarId(id);
        }

        public async Task<List<UsuariosContatos>> BuscarTodos(Expression<Func<UsuariosContatos, bool>> func, int pagina = 0)
        {
            return await _usuariosContatosSqlRepository.BuscarTodos(func, pagina);
        }

        public async Task<List<UsuariosContatos>> BuscarTodos(int pagina = 0)
        {
            return await _usuariosContatosSqlRepository.BuscarTodos(u => true, pagina);
        }

        public async Task<List<UsuariosContatos>> BuscarTodosComNoLock(int pagina = 0)
        {
            return await _usuariosContatosSqlRepository.BuscarTodosComNoLock(u => true, pagina);
        }           
    }
}
