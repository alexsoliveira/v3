using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;
namespace TGS.Cartorio.Application.AppServices.Interfaces
{
    public interface IPessoasFisicasAppService
    {
        Task Incluir(PessoasFisicas pessoafisica);
        Task Atualizar(PessoasFisicas pessoafisica);

        Task<List<PessoasFisicas>> BuscarTodos(Expression<Func<PessoasFisicas,bool>> func, int pagina = 0);
        Task<List<PessoasFisicas>> BuscarTodos(int pagina = 0);
        Task<List<PessoasFisicas>> BuscarTodosComNoLock(int pagina = 0);
        Task<PessoasFisicas> BuscarId(long id);
        Task<PessoasFisicas> BuscarPorIdPessoa(long idPessoa);
    }
}
