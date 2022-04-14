using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer
{
    public interface ISolicitacoesEstadosSqlRepository
    {
        Task<List<SolicitacoesEstados>> BuscarTodos(Expression<Func<SolicitacoesEstados, bool>> func, int pagina = 0);
        Task<List<SolicitacoesEstados>> BuscarTodosComNoLock(Expression<Func<SolicitacoesEstados, bool>> func, int pagina = 0);

        Task Incluir(SolicitacoesEstados solicitacoesestados);
        Task<SolicitacoesEstados> BuscarId(long id);

        Task<List<SolicitacoesEstados>> BuscarPorSolicitacao(long idSolicitacao);
    }
}
