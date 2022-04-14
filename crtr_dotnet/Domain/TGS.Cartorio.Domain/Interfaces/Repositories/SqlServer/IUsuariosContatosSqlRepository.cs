using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer
{
    public interface IUsuariosContatosSqlRepository : ISqlRepository<UsuariosContatos>
    {
        Task Incluir(UsuariosContatos usuario);
		Task Atualizar(UsuariosContatos usuario);
        Task<List<UsuariosContatos>> BuscarTodos(Expression<Func<UsuariosContatos, bool>> func, int pagina = 0);
        Task<List<UsuariosContatos>> BuscarTodosComNoLock(Expression<Func<UsuariosContatos, bool>> func, int pagina = 0);
        Task<UsuariosContatos> Buscar(Expression<Func<UsuariosContatos, bool>> func);
        Task<UsuariosContatos> BuscarId(int id);
    }
}