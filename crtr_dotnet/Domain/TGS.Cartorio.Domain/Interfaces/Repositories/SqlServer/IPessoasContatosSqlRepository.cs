using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;


namespace TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer
{
    public interface IPessoasContatosSqlRepository : ISqlRepository<PessoasContatos>
    {
        Task Incluir(PessoasContatos pessoacontato);
		Task Atualizar(PessoasContatos pessoacontato);

        Task<PessoasContatos> BuscarId(long id);
        Task<IEnumerable<PessoasContatos>> BuscarPorPessoa(long idPessoa);
        Task<List<PessoasContatos>> BuscarTodos(Expression<Func<PessoasContatos, bool>> func, int pagina = 0);
        Task<List<PessoasContatos>> BuscarTodosComNoLock(Expression<Func<PessoasContatos, bool>> func, int pagina = 0);
    }
}
