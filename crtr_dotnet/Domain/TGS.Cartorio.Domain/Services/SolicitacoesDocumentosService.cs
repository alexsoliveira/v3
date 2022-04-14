using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Domain.Interfaces.Services;
using System.Linq;
using TGS.Cartorio.Domain.Enumerables;

namespace TGS.Cartorio.Domain.Services
{

    public class SolicitacoesDocumentosService : ISolicitacoesDocumentosService
    {
        private readonly ISolicitacoesDocumentosSqlRepository _solicitacoesDocumentosRepository;
        private readonly ISolicitacoesService _solicitacoesservice;




        public SolicitacoesDocumentosService(ISolicitacoesDocumentosSqlRepository solicitacoesDocumentosRepository,
                                                           ISolicitacoesService solicitacoesservice
        )
        {
            _solicitacoesDocumentosRepository = solicitacoesDocumentosRepository;
            _solicitacoesservice = solicitacoesservice;
        }

        public async Task Incluir(SolicitacoesDocumentos solicitacaodocumento)
        {
            await ValidarSolicitacao(solicitacaodocumento.IdSolicitacao);

            await _solicitacoesDocumentosRepository.Incluir(solicitacaodocumento);
        }

        public async Task AssinarDocumento(SolicitacoesDocumentos solicitacaodocumento)
        {
            await ValidarSolicitacao(solicitacaodocumento.IdSolicitacao);

            await _solicitacoesDocumentosRepository.Atualizar(solicitacaodocumento);
        }

        public async Task ValidarSolicitacao(long idsolicitacao)
        {
            var _solicitacao = (await _solicitacoesservice.BuscarTodosComNoLock(p => p.IdSolicitacao == idsolicitacao)).FirstOrDefault();

            if (_solicitacao == null || _solicitacao.IdSolicitacaoEstado != (int)ESolicitacoesEstadosPC.Cadastrada)
                throw new Exception("A solicitação não pode ser editada.Verifique!");

        }

        public async Task Atualizar(SolicitacoesDocumentos solicitacaodocumento)
        {
            await _solicitacoesDocumentosRepository.Atualizar(solicitacaodocumento);
        }


        public async Task<SolicitacoesDocumentos> BuscarId(long id)
        {
            return await _solicitacoesDocumentosRepository.BuscarId(id);
        }

        public async Task<List<SolicitacoesDocumentos>> BuscarTodos(int pagina)
        {
            return await _solicitacoesDocumentosRepository.BuscarTodos(u => true, pagina);
        }

        public async Task<List<SolicitacoesDocumentos>> BuscarTodosComNoLock(int pagina)
        {
            return await _solicitacoesDocumentosRepository.BuscarTodosComNoLock(u => true, pagina);
        }

        public async Task DeletarId(long id)
        {
            await _solicitacoesDocumentosRepository.DeletarId(id);
        }

    }
}
