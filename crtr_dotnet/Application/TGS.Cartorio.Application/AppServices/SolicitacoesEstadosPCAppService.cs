using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Services;


namespace TGS.Cartorio.Application.AppServices
{
    public class SolicitacoesEstadosPcAppService : ISolicitacoesEstadosPcAppService
    {
        private readonly ISolicitacoesEstadosPCService _solicitacoesEstadosPCService;
		
        public SolicitacoesEstadosPcAppService(ISolicitacoesEstadosPCService solicitacoesEstadosPCService)
        {
            _solicitacoesEstadosPCService = solicitacoesEstadosPCService;
        }
        
        public async Task<List<SolicitacoesEstadosPc>> BuscarTodos(int pagina)
        {
            return await _solicitacoesEstadosPCService.BuscarTodos(pagina);
        }

        public async Task<List<SolicitacoesEstadosPc>> BuscarTodosComNoLock(int pagina)
        {
            return await _solicitacoesEstadosPCService.BuscarTodosComNoLock(pagina);
        }

    }
}