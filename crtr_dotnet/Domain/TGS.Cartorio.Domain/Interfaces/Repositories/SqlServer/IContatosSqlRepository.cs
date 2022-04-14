using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer
{
    public interface IContatosSqlRepository : ISqlRepository<Contatos>
    {
        Task Incluir(Contatos contato);
		Task Atualizar(Contatos contato);

        Task<Contatos> BuscarId(int id);
        Task<List<Contatos>> BuscarTodos(Expression<Func<Contatos, bool>> func, int pagina = 0);
        Task<List<Contatos>> BuscarTodosComNoLock(Expression<Func<Contatos, bool>> func, int pagina = 0);
    }
}
