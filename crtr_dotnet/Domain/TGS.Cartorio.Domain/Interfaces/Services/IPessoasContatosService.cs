using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace TGS.Cartorio.Domain.Interfaces.Services
{
    public interface  IPessoasContatosService
    {
        Task Incluir(PessoasContatos pessoacontato);
        Task Atualizar(PessoasContatos pessoacontato);

        Task<List<PessoasContatos>> BuscarTodos(int pagina = 0);
        Task<List<PessoasContatos>> BuscarTodosComNoLock(int pagina = 0);
        Task<PessoasContatos> BuscarId(long id);
        Task<IEnumerable<PessoasContatos>> BuscarPorPessoa(long idPessoa);
    }
}
