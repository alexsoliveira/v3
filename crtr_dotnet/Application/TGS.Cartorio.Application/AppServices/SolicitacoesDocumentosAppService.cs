using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Services;


namespace TGS.Cartorio.Application.AppServices
{
    public class SolicitacoesDocumentosAppService : ISolicitacoesDocumentosAppService
    {
        private readonly ISolicitacoesDocumentosService _solicitacoesDocumentosService;

        public SolicitacoesDocumentosAppService(ISolicitacoesDocumentosService solicitacoesDocumentosService)
        {
            _solicitacoesDocumentosService = solicitacoesDocumentosService;
        }


        public async Task Atualizar(SolicitacoesDocumentos solicitacaodocumento)
        {
            await _solicitacoesDocumentosService.Atualizar(solicitacaodocumento);
        }

        public async Task<SolicitacoesDocumentos> BuscarId(long id)
        {
            return await _solicitacoesDocumentosService.BuscarId(id);
        }

        public async Task<List<SolicitacoesDocumentos>> BuscarTodos(int pagina)
        {
            return await _solicitacoesDocumentosService.BuscarTodos(pagina);
        }

        public async Task<List<SolicitacoesDocumentos>> BuscarTodosComNoLock(int pagina)
        {
            return await _solicitacoesDocumentosService.BuscarTodosComNoLock(pagina);
        }

        public async Task AssinarDocumento(SolicitacoesDocumentos solicitacaodocumento)
        {
            await _solicitacoesDocumentosService.AssinarDocumento(solicitacaodocumento);
        }

        public async Task Incluir(SolicitacoesDocumentos solicitacaodocumento)
        {
            await _solicitacoesDocumentosService.Incluir(solicitacaodocumento);

        }
    }
}