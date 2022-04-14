using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Domain.Interfaces.Services;

namespace TGS.Cartorio.Domain.Services
{
    public class SolicitacoesEstadosService : ISolicitacoesEstadosService
    {
        private readonly ISolicitacoesEstadosSqlRepository _solicitacoesEstadosSqlRepository;

        public SolicitacoesEstadosService(ISolicitacoesEstadosSqlRepository solicitacoesEstadosSqlRepository)
        {
            _solicitacoesEstadosSqlRepository = solicitacoesEstadosSqlRepository;
        }

        public async Task<List<SolicitacoesEstados>> BuscarTodos(int pagina)
        {
            return await _solicitacoesEstadosSqlRepository.BuscarTodos(u => true, pagina);
        }

        public async Task<List<SolicitacoesEstados>> BuscarPorSolicitacao(long idSolicitacao)
        {
            return await _solicitacoesEstadosSqlRepository.BuscarPorSolicitacao(idSolicitacao);
        }

        public async Task<List<SolicitacoesEstados>> BuscarTodosComNoLock(int pagina)
        {
            return await _solicitacoesEstadosSqlRepository.BuscarTodosComNoLock(u => true, pagina);
        }

        public async Task Incluir(SolicitacoesEstados solicitacoesestados)
        {
            await _solicitacoesEstadosSqlRepository.Incluir(solicitacoesestados);
        }

        public async Task<SolicitacoesEstados> BuscarId(long idSolicitacoesEstados)
        {
            return await _solicitacoesEstadosSqlRepository.BuscarId(idSolicitacoesEstados);
        }        
    }
}
