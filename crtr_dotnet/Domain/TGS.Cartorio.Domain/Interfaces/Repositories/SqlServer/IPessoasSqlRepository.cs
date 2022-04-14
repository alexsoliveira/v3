using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer
{
    public interface IPessoasSqlRepository : ISqlRepository<Pessoas>
    {
        Task Atualizar(Pessoas pessoa);
        Task Incluir(Pessoas pessoa);
        Task<List<Pessoas>> BuscarTodos(Expression<Func<Pessoas, bool>> func, int pagina = 0);
        Task<List<Pessoas>> BuscarTodosComNoLock(Expression<Func<Pessoas, bool>> func, int pagina = 0);
        Task<Pessoas> BuscarId(long id);
        Task<long?> ExisteAsync(int idTipoDocumento, long documento);
        Task Remover(Pessoas pessoa);
        Task<Pessoas> BuscarPorIdCompleto(long id);
    }
}