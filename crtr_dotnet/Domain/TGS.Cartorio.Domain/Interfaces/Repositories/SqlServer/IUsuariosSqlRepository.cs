using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer
{
    public interface IUsuariosSqlRepository : ISqlRepository<Usuarios>
    {
        Task Incluir(Usuarios usuario);
		Task Atualizar(Usuarios usuario);
        Task<List<Usuarios>> BuscarTodos(Expression<Func<Usuarios, bool>> func, int pagina = 0);
        Task<List<Usuarios>> BuscarTodosComNoLock(Expression<Func<Usuarios, bool>> func, int pagina = 0);
        Task<Usuarios> Buscar(Expression<Func<Usuarios, bool>> func);
        Task<Usuarios> BuscarId(long id);
        Task<Usuarios> BuscarEmail(string email);
        Task<Usuarios> BuscarPorIdPessoa(long idPessoa);
    }
}