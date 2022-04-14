using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Services;


namespace TGS.Cartorio.Application.AppServices
{
    public class SolicitacoesNotificacoesAppService : ISolicitacoesNotificacoesAppService
    {
        private readonly ISolicitacoesNotificacoesService _solicitacoesNotificacoesService;

        public SolicitacoesNotificacoesAppService(ISolicitacoesNotificacoesService solicitacoesNotificacoesService)
        {
            _solicitacoesNotificacoesService = solicitacoesNotificacoesService;
        }
        public async Task Incluir(SolicitacoesNotificacoes solicitacaonotificacao)
        {
            await _solicitacoesNotificacoesService.Incluir(solicitacaonotificacao);
        }

        public async Task<List<SolicitacoesNotificacoes>> BuscarTodos(int pagina)
        {
            return await _solicitacoesNotificacoesService.BuscarTodos(pagina);
        }


        public async Task<List<SolicitacoesNotificacoes>> BuscarTodosComNoLock(Expression<Func<SolicitacoesNotificacoes, bool>> func, int pagina)
        {
            return await _solicitacoesNotificacoesService.BuscarTodosComNoLock(func,pagina);
        }

    }
}
