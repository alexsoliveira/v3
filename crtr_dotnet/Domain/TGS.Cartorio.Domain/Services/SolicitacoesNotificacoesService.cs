using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Enumerables;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Domain.Interfaces.Services;

namespace TGS.Cartorio.Domain.Services
{
    
	  public class SolicitacoesNotificacoesService : ISolicitacoesNotificacoesService
    {		    
        private readonly ISolicitacoesNotificacoesSqlRepository _solicitacoesNotificacoesRepository;

        public SolicitacoesNotificacoesService(ISolicitacoesNotificacoesSqlRepository solicitacoesNotificacoesRepository)
        {
            _solicitacoesNotificacoesRepository = solicitacoesNotificacoesRepository;
        }

        public async Task Incluir(SolicitacoesNotificacoes solicitacaonotificacao)
        {
            solicitacaonotificacao.IdSolicitacaoNotificacaoEstado = (int)ESolicitacoesNotificacoesEstadospc.Cadastrada;

            await _solicitacoesNotificacoesRepository.Incluir(solicitacaonotificacao);
        }

        public async Task<List<SolicitacoesNotificacoes>> BuscarTodos(int pagina)
        {
            return await _solicitacoesNotificacoesRepository.BuscarTodos(u => true, pagina);
        }
        
        public async Task<List<SolicitacoesNotificacoes>> BuscarTodosComNoLock(Expression<Func<SolicitacoesNotificacoes, bool>> func, int pagina)
        {
            return await _solicitacoesNotificacoesRepository.BuscarTodosComNoLock(u => true, pagina);
        }
    }
}
