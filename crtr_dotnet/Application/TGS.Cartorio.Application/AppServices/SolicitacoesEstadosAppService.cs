using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Services;

namespace TGS.Cartorio.Application.AppServices
{
    public class SolicitacoesEstadosAppService : ISolicitacoesEstadosAppService
    {
        private readonly ISolicitacoesEstadosService _solicitacoesEstadosService;

        public SolicitacoesEstadosAppService(ISolicitacoesEstadosService solicitacoesEstadosService)
        {
            _solicitacoesEstadosService = solicitacoesEstadosService;
        }

        public async Task<List<SolicitacoesEstados>> BuscarTodos(int pagina)
        {
            return await _solicitacoesEstadosService.BuscarTodos(pagina);
        }

        public async Task<List<SolicitacoesEstados>> BuscarPorSolicitacao(long idSolicitacao)
        {
            return await _solicitacoesEstadosService.BuscarPorSolicitacao(idSolicitacao);
        }

        public async Task<List<SolicitacoesEstados>> BuscarTodosComNoLock(int pagina)
        {
            return await _solicitacoesEstadosService.BuscarTodosComNoLock(pagina);
        }

        public async Task Incluir(SolicitacoesEstados solicitacoesestados)
        {
            await _solicitacoesEstadosService.Incluir(solicitacoesestados);
        }

        //public async Task<SolicitacoesEstados> BuscarId(long id)
        //{
        //    return await _solicitacoesEstadosService.BuscarId(id);
        //}
    }
}
