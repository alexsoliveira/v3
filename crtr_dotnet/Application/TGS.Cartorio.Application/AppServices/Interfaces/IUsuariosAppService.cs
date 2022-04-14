using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Application.AppServices.Interfaces
{
    public interface IUsuariosAppService
    {
        Task Incluir(Usuarios usuario);
        Task Atualizar(Usuarios usuario);

        Task<List<Usuarios>> BuscarTodos(Expression<Func<Usuarios,bool>> func, int pagina = 0);
        Task<List<Usuarios>> BuscarTodos(int pagina = 0);
        Task<List<Usuarios>> BuscarTodosComNoLock(int pagina = 0);
        Task<Usuarios> Buscar(Usuarios usuario);
        Task<Usuarios> BuscarId(long id);
        Task<Usuarios> BuscarPorIdPessoa(long idPessoa);
    }
}