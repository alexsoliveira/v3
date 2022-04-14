using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace TGS.Cartorio.Domain.Interfaces.Services
{
    public interface  IPessoasJuridicasService
    {
        Task Incluir(PessoasJuridicas pessoajuridica);
        Task Atualizar(PessoasJuridicas pessoajuridica);

        Task<List<PessoasJuridicas>> BuscarTodos(Expression<Func<PessoasJuridicas, bool>> func, int pagina = 0);
        Task<List<PessoasJuridicas>> BuscarTodosComNoLock(int pagina = 0);
        Task<PessoasJuridicas> BuscarId(long id);
        Task<List<PessoasJuridicas>>  BuscarTodos(int pagina);
    }
}
