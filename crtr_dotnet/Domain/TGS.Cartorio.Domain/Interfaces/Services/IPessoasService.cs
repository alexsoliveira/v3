using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;



namespace TGS.Cartorio.Domain.Interfaces.Services
{
    public interface  IPessoasService
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
