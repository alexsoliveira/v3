using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;


namespace TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer
{
    public interface IPessoasFisicasSqlRepository : ISqlRepository<PessoasFisicas>
    {
        Task Incluir(PessoasFisicas pessoafisica);
		Task Atualizar(PessoasFisicas pessoafisica);
        Task<PessoasFisicas> BuscarId(long id);
        Task<List<PessoasFisicas>> BuscarTodos(Expression<Func<PessoasFisicas, bool>> func, int pagina = 0);
        Task<List<PessoasFisicas>> BuscarTodosComNoLock(Expression<Func<PessoasFisicas, bool>> func, int pagina = 0);
        Task<PessoasFisicas> BuscarPorIdPessoa(long idPessoa);
    }
}
