using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TGS.Cartorio.Domain.Interfaces.Services
{
    public interface  ISolicitacoesNotificacoesService
    {
        Task Incluir(SolicitacoesNotificacoes solicitacanotificacao);
		Task<List<SolicitacoesNotificacoes>> BuscarTodos(int pagina = 0);
        Task<List<SolicitacoesNotificacoes>> BuscarTodosComNoLock(Expression<Func<SolicitacoesNotificacoes, bool>> func, int pagina = 0);
    }
}
