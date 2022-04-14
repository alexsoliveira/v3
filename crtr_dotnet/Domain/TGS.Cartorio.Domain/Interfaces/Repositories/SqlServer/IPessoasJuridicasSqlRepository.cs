using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;


namespace TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer
{
    public interface IPessoasJuridicasSqlRepository : ISqlRepository<PessoasJuridicas>
    {
        Task Incluir(PessoasJuridicas pessoajuridica);
		Task Atualizar(PessoasJuridicas pessoajuridica);
        Task<PessoasJuridicas> BuscarId(long id);
        Task<List<PessoasJuridicas>> BuscarTodos(Expression<Func<PessoasJuridicas, bool>> func, int pagina = 0);
        Task<List<PessoasJuridicas>> BuscarTodosComNoLock(Expression<Func<PessoasJuridicas, bool>> func, int pagina = 0);
        
    }
}
