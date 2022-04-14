using TGS.Cartorio.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System;

namespace TGS.Cartorio.Domain.Interfaces.Services
{
    public interface IUsuariosService
    {
        Task Incluir(Usuarios usuario);
        Task Atualizar(Usuarios usuario);
        Task<List<Usuarios>> BuscarTodos(int pagina = 0);
        Task<List<Usuarios>> BuscarTodos(Expression<Func<Usuarios, bool>> func, int pagina = 0);
        Task<List<Usuarios>> BuscarTodosComNoLock(int pagina = 0);
        Task<Usuarios> Buscar(Usuarios usuario);
        Task<Usuarios> BuscarId(long id);
        Task<Usuarios> BuscarEmail(string email);
        Task<Usuarios> BuscarPorIdPessoa(long idPessoa);
    }
}
