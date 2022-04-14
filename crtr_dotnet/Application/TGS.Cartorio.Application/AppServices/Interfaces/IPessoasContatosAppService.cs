using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Application.AppServices.Interfaces
{
    public interface IPessoasContatosAppService
    {
        Task Incluir(PessoasContatos pessoacontatos);
        Task Atualizar(PessoasContatos pessoacontatos);

        Task<List<PessoasContatos>> BuscarTodos(int pagina = 0);
        Task<List<PessoasContatos>> BuscarTodosComNoLock(int pagina = 0);
        Task<PessoasContatos> BuscarId(long id);
        Task<IEnumerable<PessoasContatos>> BuscarPorPessoa(long idPessoa);
    }
}
