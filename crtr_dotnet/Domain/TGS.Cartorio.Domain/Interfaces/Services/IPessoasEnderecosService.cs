using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Domain.Interfaces.Services
{
    public interface  IPessoasEnderecosService
    {
        Task Incluir(PessoasEnderecos pessoaendereco);
        Task Atualizar(PessoasEnderecos pessoaendereco);

        Task<List<PessoasEnderecos>> BuscarTodos(int pagina = 0);
        Task<List<PessoasEnderecos>> BuscarTodosComNoLock(int pagina = 0);
        Task<PessoasEnderecos> BuscarId(long id);
        Task<List<PessoasEnderecos>> BuscarPorPessoa(long idPessoa);
        Task<PessoasEnderecos> BuscarPorEndereco(long idEndereco);
        Task<object> ConsultarCep(long cep);
        Task RemoverPorIdEndereco(long idEndereco);
        Task<int> CountByIdPessoa(long idPessoa);
        Task<long?> BuscarPessoaPorEndereco(long idEndereco);
    }
}
