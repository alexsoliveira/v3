using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;


namespace TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer
{
    public interface ICartoriosSqlRepository : ISqlRepository<Cartorios>
    {
        Task Atualizar(Cartorios cartorio);
        Task Incluir(Cartorios cartorio);
        Task<Cartorios> BuscarId(int id);
        Task<List<Cartorios>> BuscarTodos(Expression<Func<Cartorios, bool>> func, int pagina = 0);
        Task<List<Cartorios>> BuscarTodosComNoLock(Expression<Func<Cartorios, bool>> func, int pagina = 0);
        Cartorios BuscarUltimoCartorioValido();
    }
}