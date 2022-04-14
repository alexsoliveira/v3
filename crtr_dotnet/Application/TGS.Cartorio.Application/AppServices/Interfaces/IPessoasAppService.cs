using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace TGS.Cartorio.Application.AppServices.Interfaces
{
    public interface IPessoasAppService
    {
        Task Atualizar(Pessoas pessoa);
        Task Incluir(Pessoas pessoa);
        Task<List<Pessoas>> BuscarTodos(int pagina = 0);
        Task<List<Pessoas>> BuscarTodosComNoLock(int pagina = 0);
        Task<List<Pessoas>> BuscarTodosComNoLock(Expression<Func<Pessoas, bool>> func, int pagina = 0);
        Task<Pessoas> BuscarId(long id);
        Task<long?> PessoaExiste(int idTipoDocumento, long documento);
        Task<Pessoas> BuscarPorIdCompleto(long id);
    }
}