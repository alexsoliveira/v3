using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Application.AppServices.Interfaces
{
    public interface ISolicitacoesNotificacoesAppService
    {
        Task Incluir(SolicitacoesNotificacoes solicitacaonotificacao);
        Task<List<SolicitacoesNotificacoes>> BuscarTodos(int pagina = 0);
        Task<List<SolicitacoesNotificacoes>> BuscarTodosComNoLock(Expression<Func<SolicitacoesNotificacoes,bool>> func, int pagina = 0);
    }
}