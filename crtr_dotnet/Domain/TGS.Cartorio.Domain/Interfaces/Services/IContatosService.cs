using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TGS.Cartorio.Domain.Interfaces.Services
{
    public interface  IContatosService
    {
        Task Incluir(Contatos contato);
        Task Atualizar(Contatos contato);
        Task<List<Contatos>> BuscarTodosPorUsuario(long idUsuario);
        Task<List<Contatos>> BuscarTodos(int pagina = 0);
        Task<List<Contatos>> BuscarTodosComNoLock(int pagina = 0);
        Task<Contatos> BuscarId(int id);
    }
}
