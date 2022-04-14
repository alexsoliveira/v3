using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Application.AppServices.Interfaces
{
    public interface IUsuariosContatosAppService
    {
        Task Incluir(UsuariosContatos usuario);
        Task Atualizar(UsuariosContatos usuario);
        Task<List<UsuariosContatos>> BuscarTodos(Expression<Func<UsuariosContatos, bool>> func, int pagina = 0);
        Task<List<UsuariosContatos>> BuscarTodos(int pagina = 0);
        Task<List<UsuariosContatos>> BuscarTodosComNoLock(int pagina = 0);
        Task<UsuariosContatos> Buscar(UsuariosContatos usuario);
        Task<UsuariosContatos> BuscarId(int id);
    }
}