
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;


namespace TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer
{
    public interface ISolicitacoesEstadosPCSqlRepository : ISqlRepository<SolicitacoesEstadosPc>
    {
        Task<List<SolicitacoesEstadosPc>> BuscarTodos(Expression<Func<SolicitacoesEstadosPc, bool>> func, int pagina = 0);
        Task<List<SolicitacoesEstadosPc>> BuscarTodosComNoLock(Expression<Func<SolicitacoesEstadosPc, bool>> func, int pagina = 0);
    }
}