using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;


namespace TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer
{
    public interface IPessoasEnderecosSqlRepository : ISqlRepository<PessoasEnderecos>
    {
        Task Incluir(PessoasEnderecos pessoaendereco);
		Task Atualizar(PessoasEnderecos pessoaendereco);
        Task<List<PessoasEnderecos>> BuscarTodos(Expression<Func<PessoasEnderecos, bool>> func, int pagina = 0);
        Task<List<PessoasEnderecos>> BuscarTodosComNoLock(Expression<Func<PessoasEnderecos, bool>> func, int pagina = 0);
        Task<PessoasEnderecos> BuscarId(long id);
        Task<List<PessoasEnderecos>> BuscarPorPessoa(long idPessoa);
        Task<List<PessoasEnderecos>> BuscarPorEndereco(long idEndereco);
        Task<long?> BuscarPessoaPorEndereco(long idEndereco);
        Task<PessoasEnderecos> BuscarPorEnderecoUnico(long idEndereco);
        Task RemoverPorIdEndereco(long idEndereco);
        Task<int> CountByIdPessoa(long idPessoa);
    }
}
