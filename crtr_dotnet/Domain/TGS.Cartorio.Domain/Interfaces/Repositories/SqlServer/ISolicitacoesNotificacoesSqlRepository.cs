using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;


namespace TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer
{
    public interface ISolicitacoesNotificacoesSqlRepository : ISqlRepository<SolicitacoesNotificacoes>
    {
        Task Incluir(SolicitacoesNotificacoes solicitacaonotificacao);
		Task Atualizar(SolicitacoesNotificacoes solicitacaonotificacao);
        Task<List<SolicitacoesNotificacoes>> BuscarTodos(Expression<Func<SolicitacoesNotificacoes, bool>> func, int pagina = 0);
        Task<List<SolicitacoesNotificacoes>> BuscarTodosComNoLock(Expression<Func<SolicitacoesNotificacoes, bool>> func, int pagina = 0);
    }
}