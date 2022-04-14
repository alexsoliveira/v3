using Hangfire.Server;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Application.DTO;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Entities.Procuracoes;

namespace TGS.Cartorio.Application.AppServices.Interfaces
{
    public interface ISolicitacoesAppService
    {
        Task<long> Incluir(SolicitacoesSimplificadoDto solicitacaoDto);
        Task Atualizar(SolicitacoesDto solicitacao);
        Task<SolicitacaoExistenteDto> BuscarSolicitacao(long idSolicitacao);
        Task<List<Solicitacoes>> BuscarTodosComNoLock(Expression<Func<Solicitacoes,bool>> func, int pagina = 0);
        Task AtualizarSolicitacaoParaCarrinho(long idSolicitacao);
        Task<Solicitacoes> BuscarId(long id);
        Task<StatusSolicitacaoHeaderDto> BuscarDadosStatusSolicitacao(long idsolicitacao);
        Task<List<MinhasSolicitacoes>> MinhasSolicitacoes(long id);
        Task<MinhaSolicitacao> ConsultarBoleto(long id);
        Task<string> GerarBoleto(Solicitacoes solicitacao, BoletoDto boleto, string token);
        Task<List<StatusSolicitacao>> BuscarEstadoDoPedidoPorParticipante(long idSolicitacao);
        Task<List<ProcuracoesPartesEstadosPc>> BuscarTodasProcuracoesPartesEstados(Expression<Func<ProcuracoesPartesEstadosPc, bool>> func, int pagina = 0);
        Task AtualizarSolicitacaoParaAguardandoAssinaturaDigitalSolicitante(long idSolicitacao);
        Task AtualizarSolicitacaoParaAguardandoPagamento(long idSolicitacao);
        void BuscarSolicitacoesProntasParaEnvioParaCartorio(PerformContext consoleHangFire);
        void AtualizarSolicitacaoParaProntaParaEnvioCartorio(SolicitacoesDto solicitacaoDto);
        void FinalizarJob();
        Task<string> ConsultarStatusSolicitacao(long idSolicitacao);
        Task<ProcuracoesPartesSimplificadoDto> BuscarDadosProcuracaoSolicitante(long idSolicitacao);
    }
}