using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;


namespace TGS.Cartorio.Application.AppServices.Interfaces
{
    public interface IContatosAppService
    {
        Task Atualizar(Contatos contato);
        Task Incluir(Contatos contato);
        Task<List<Contatos>> BuscarTodos(int pagina = 0);
        Task<List<Contatos>> BuscarTodosPorUsuario(long idUsuario);
        Task<List<Contatos>> BuscarTodosComNoLock(int pagina = 0);
        Task<Contatos> BuscarId(int id);
    }
}
