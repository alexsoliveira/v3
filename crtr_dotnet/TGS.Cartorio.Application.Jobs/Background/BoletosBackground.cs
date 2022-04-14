using Newtonsoft.Json;
using System;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Application.DTO;
using TGS.Cartorio.Application.Enumerables;
using TGS.Cartorio.Application.Jobs.Background.Base;
using TGS.Cartorio.Application.Jobs.Background.Interfaces;
using TGS.Cartorio.Application.Jobs.Configurations;
using TGS.Cartorio.Application.ViewModel;
using Hangfire.Server;
using Hangfire.Console;
using TGS.Cartorio.Application.Extensions;
using System.Linq;

namespace TGS.Cartorio.Application.Jobs.Background
{
    public class BoletosBackground : MainBackground, IBoletosBackground
    {
        private readonly ISolicitacoesAppService _solicitacoesAppService;
        private readonly IPagamentoAppService _pagamentoAppService;
        private readonly ILogSistemaAppService _logSistemaAppService;

        public BoletosBackground(ISolicitacoesAppService solicitacoesAppService,
            IPagamentoAppService pagamentoAppService,
            ILogSistemaAppService logSistemaAppService,
            IContaAppService contaAppService)
            : base(contaAppService)
        {
            _solicitacoesAppService = solicitacoesAppService;
            _pagamentoAppService = pagamentoAppService;
            _logSistemaAppService = logSistemaAppService;
        }

        public void AtualizarStatusBoletos(PerformContext consoleHangFire)
        {
            lock (JobConfiguration.objLockBoleto)
            {
                CodLogSistema log = CodLogSistema.Desconhecido;
                try
                {
                    var solicitacoes = _pagamentoAppService.TodasSolicitacoesAguardandoPagamentoBoleto().GetAwaiter().GetResult();
                    if (solicitacoes != null
                        && solicitacoes.Count() > 0)
                    {
                        foreach (var solicitacaoDto in solicitacoes)
                        {
                            var retorno = _pagamentoAppService.ConsultarPagamentoBoleto(consoleHangFire, solicitacaoDto.IdSolicitacao.Value).GetAwaiter().GetResult();
                            if (retorno.Sucesso && string.IsNullOrEmpty(retorno.Status))
                                throw new Exception("Retornou da API sucesso porém com propriedade 'Status' NULO!!");

                            if (retorno.Sucesso && retorno.Status.ToLower() == StatusBoleto.P.ToString().ToLower())
                            {
                                _solicitacoesAppService.AtualizarSolicitacaoParaProntaParaEnvioCartorio(solicitacaoDto);
                                var camposPagamento = JsonConvert.DeserializeObject<CamposPagamentoViewModel>(solicitacaoDto.CamposPagamento);

                                log = CodLogSistema.Job_AtualizacaoSolicitacaoBoletoPagoComSucesso;
                                string msg = $"IdSolicitacao = {solicitacaoDto.IdSolicitacao.Value}, boleto foi validado e verificado como pago";
                                try
                                {
                                    consoleHangFire.CreateConsoleMessage(log, msg);
                                }
                                catch { }

                                _logSistemaAppService.AddByJob(log, new
                                {
                                    IdSolicitacao = solicitacaoDto.IdSolicitacao.Value,
                                    CamposPagamento = camposPagamento,
                                    Msg = msg
                                });
                            }

                            _solicitacoesAppService.FinalizarJob();
                        }

                        try
                        {
                            consoleHangFire.CreateConsoleMessage(null, "Job Finalizado!!");
                        }
                        catch { }
                    }
                    else
                    {
                        try
                        {
                            consoleHangFire.CreateConsoleMessage(null, "Não foram localizadas solicitações para verificar pagamento por boleto!");
                        }
                        catch { }
                    }   
                }
                catch (Exception ex)
                {
                    log = CodLogSistema.Job_Erro_AtualizarStatusDaSolicitacaoPorBoletoPago;

                    string msg = "Ocorreu um ERRO na atualização da solicitação por Boleto Pago!";

                    consoleHangFire.CreateExceptionMessage(ex, log, msg);

                    _logSistemaAppService.AddByJob(log, new
                    {
                        Sucesso = false,
                        msg = msg
                    }, ex);

                    _solicitacoesAppService.FinalizarJob();
                }
            }
        }


    }
}
