using AutoMapper;
using Hangfire.Server;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Application.DTO;
using TGS.Cartorio.Application.DTO.Pagamento;
using TGS.Cartorio.Application.Enumerables;
using TGS.Cartorio.Application.Extensions;
using TGS.Cartorio.Application.ViewModel;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Services;
using TGS.Cartorio.Infrastructure.Utility.ApiClient;

namespace TGS.Cartorio.Application.AppServices
{
    public class PagamentoAppService : IPagamentoAppService
    {
        private readonly ApiPagamento _apiPagamento;
        private readonly ISolicitacoesService _solicitacoesService;
        private readonly ILogSistemaAppService _logSistemaAppService;
        private readonly IMapper _mapper;
        public PagamentoAppService(ApiPagamento apiPagamento,
            ISolicitacoesService solicitacoesService,
            IMapper mapper,
            ILogSistemaAppService logSistemaAppService)
        {
            _apiPagamento = apiPagamento;
            _solicitacoesService = solicitacoesService;
            _mapper = mapper;
            _logSistemaAppService = logSistemaAppService;
        }

        #region Cartão de Crédito
        public async Task<SimuladorParcelamentoDto> SimularParcelamento(decimal valorTotal, string token)
        {
            try
            {
                var response = await _apiPagamento.PostRetorno<SimuladorParcelamentoDto>($"api/v1/CartaoCredito/SimularParcelamento",
                    new { ValorTotal = valorTotal },
                    token: token);

                await _logSistemaAppService.Add(CodLogSistema.PagamentoAppService_SimularParcelamento, new
                {
                    ValorTotal = valorTotal,
                    LogServico = response.Log
                });

                if (response.Sucesso)
                {
                    response.ObjRetorno.ProcessarParcelas();
                    response.ObjRetorno.Sucesso = true;
                    return response.ObjRetorno;
                }
                else if (!string.IsNullOrEmpty(response.MensagemErro))
                {
                    if (response.ObjRetorno == null)
                        response.ObjRetorno = new SimuladorParcelamentoDto();
                    response.ObjRetorno.MensagemErro = response.MensagemErro;
                    return response.ObjRetorno;
                }

                return null;
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_PagamentoAppService_SimularParcelamento, new
                {
                    ValorTotal = valorTotal,
                    Token = token
                }, ex);

                throw;
            }
        }

        public async Task<Retorno<RespostaPagamentoCartaoCreditoDto>> PagarComCartaoCredito(CartaoCreditoDto cartaoCredito, string token)
        {
            try
            {
                var response = await _apiPagamento.PostRetorno<RespostaPagamentoCartaoCreditoDto>($"api/v1/CartaoCredito/PagamentoCartaoCredito",
                    cartaoCredito,
                    token: token);

                if (response.Sucesso 
                    && response.ObjRetorno != null
                    && response.ObjRetorno.StatusCodeSucesso)
                {
                    response.ObjRetorno.Sucesso = true;
                    return response;
                }
                else if (!string.IsNullOrEmpty(response.MensagemErro))
                {
                    response.ObjRetorno = JsonConvert.DeserializeObject<RespostaPagamentoCartaoCreditoDto>(response.MensagemErro);
                    return response;
                }

                return null;
            }
            catch (Exception)
            {
                cartaoCredito.Cartao.RemoverDadosPorSeguranca();
                cartaoCredito.Cliente.RemoverDadosPorSeguranca();

                throw;
            }
        }

        public async Task<RespostaConsultaTransacaoDto> ConsultarTransacaoCartaoCreditoPorIdentifier(string identifier, string token)
        {
            try
            {
                if (string.IsNullOrEmpty(identifier))
                    return null;

                var response = await _apiPagamento.PostRetorno<RespostaConsultaTransacaoDto>($"api/v1/CartaoCredito/ConsultarTransacaoCartaoCredito",
                    new { idTransacao = identifier },
                    token: token);

                await _logSistemaAppService.Add(CodLogSistema.PagamentoAppService_ConsultarTransacaoCartaoCreditoPorIdentifier, new
                {
                    Identifier = identifier,
                    LogServico = response.Log
                });

                if (response.Sucesso)
                {
                    response.ObjRetorno.Sucesso = true;
                    return response.ObjRetorno;
                }
                else if (!string.IsNullOrEmpty(response.MensagemErro))
                {
                    response.ObjRetorno.MensagemErro = response.MensagemErro;
                    return response.ObjRetorno;
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<RespostaConsultaTransacaoDto> ConsultarTransacaoCartaoCreditoPorSolicitacao(long idSolicitacao, string token)
        {
            Solicitacoes solicitacao = null;
            try
            {
                solicitacao = await _solicitacoesService.BuscarId(idSolicitacao);
                if (solicitacao == null || string.IsNullOrEmpty(solicitacao.CamposPagamento))
                {
                    await _logSistemaAppService.Add(CodLogSistema.PagamentoAppService_ConsultarTransacaoCartaoCreditoPorSolicitacao_BuscarId, new
                    {
                        IdSolicitacao = idSolicitacao
                    });

                    return null;
                }
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_PagamentoAppService_ConsultarTransacaoCartaoCreditoPorSolicitacao_BuscarId, new
                {
                    IdSolicitacao = idSolicitacao
                }, ex);

                throw;
            }

            CamposPagamentoViewModel campos = null;
            try
            {
                campos = JsonConvert.DeserializeObject<CamposPagamentoViewModel>(solicitacao.CamposPagamento);
                if (campos == null || string.IsNullOrEmpty(campos.Identifier) || campos.TipoPagamento == TiposPagamento.Boleto.ToString())
                {
                    await _logSistemaAppService.Add(CodLogSistema.PagamentoAppService_ConsultarTransacaoCartaoCreditoPorSolicitacao_CamposPagamento, new
                    {
                        IdSolicitacao = idSolicitacao,
                        Solicitacao = solicitacao
                    });

                    return null;
                }
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_PagamentoAppService_ConsultarTransacaoCartaoCreditoPorSolicitacao_CamposPagamento, new
                {
                    IdSolicitacao = idSolicitacao,
                    Solicitacao = solicitacao
                }, ex);

                throw;
            }

            try
            {
                var response = await _apiPagamento.PostRetorno<RespostaConsultaTransacaoDto>($"api/v1/CartaoCredito/ConsultarTransacaoCartaoCredito",
                new { idTransacao = campos.Identifier },
                token: token);

                await _logSistemaAppService.Add(CodLogSistema.PagamentoAppService_ConsultarTransacaoCartaoCreditoPorSolicitacao_Response, new
                {
                    Solicitacao = solicitacao,
                    CamposPagamento = campos,
                    LogServico = response.Log
                });

                if (response.Sucesso)
                {
                    response.ObjRetorno.Sucesso = true;
                    return response.ObjRetorno;
                }
                else if (!string.IsNullOrEmpty(response.MensagemErro))
                {
                    response.ObjRetorno.MensagemErro = response.MensagemErro;
                    return response.ObjRetorno;
                }

                if (response.ObjRetorno == null)
                    response.ObjRetorno = new RespostaConsultaTransacaoDto();

                response.ObjRetorno.chargeStatus = "Não existe pagamento com cartão de crédito para esta solicitação!";
                return response.ObjRetorno;
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_PagamentoAppService_ConsultarTransacaoCartaoCreditoPorSolicitacao_Response, new
                {
                    Solicitacao = solicitacao,
                    CamposPagamento = campos
                }, ex);

                throw;
            }
        }

        public async Task<RespostaConsultaBoleto> ConsultarPagamentoBoleto(PerformContext consoleHangFire, long idSolicitacao)
        {
            CodLogSistema log = CodLogSistema.Desconhecido;

            Solicitacoes solicitacao = null;
            try
            {
                solicitacao = await _solicitacoesService.BuscarId(idSolicitacao);
                if (solicitacao == null)
                    throw new Exception($"Não foi possível localizar a Solicitação {idSolicitacao}!");
            }
            catch (Exception ex)
            {
                string msg = $"IdSolicitacao = {solicitacao.IdSolicitacao}";
                log = CodLogSistema.Erro_PagamentoAppService_ConsultarPagamentoBoleto_BuscarSolicitacao;

                if (consoleHangFire != null)
                    consoleHangFire.CreateExceptionMessage(ex, log, msg);

                _logSistemaAppService.AddByJob(log, new
                {
                    Solicitacao = solicitacao
                }, ex);

                throw;
            }


            if (!string.IsNullOrEmpty(solicitacao.Conteudo) && !string.IsNullOrEmpty(solicitacao.CamposPagamento))
            {
                SolicitacaoConteudoViewModel conteudoSolicitacao = null;
                try
                {
                    conteudoSolicitacao = JsonConvert.DeserializeObject<SolicitacaoConteudoViewModel>(solicitacao.Conteudo);
                }
                catch (Exception ex)
                {
                    string msg = $"IdSolicitacao = {solicitacao.IdSolicitacao}";
                    log = CodLogSistema.Erro_PagamentoAppService_ConsultarPagamentoBoleto_AoDesserializarSolicitacaoConteudo;

                    if (consoleHangFire != null)
                        consoleHangFire.CreateExceptionMessage(ex, log, msg);

                    _logSistemaAppService.AddByJob(log, new
                    {
                        Solicitacao = solicitacao
                    }, ex);

                    throw;
                }
                
                if (conteudoSolicitacao != null
                    && conteudoSolicitacao.TipoPagamentoAtual == TiposPagamento.Boleto.ToString()
                    && (string.IsNullOrEmpty(conteudoSolicitacao.EstadoPagamento) || conteudoSolicitacao.EstadoPagamento != EstadosPagamento.Aprovado.ToString()))
                {
                    CamposPagamentoViewModel camposPagamento = null;
                    try
                    {
                        camposPagamento = JsonConvert.DeserializeObject<CamposPagamentoViewModel>(solicitacao.CamposPagamento);
                    }
                    catch (Exception ex)
                    {
                        string msg = $"IdSolicitacao = {solicitacao.IdSolicitacao}";
                        log = CodLogSistema.Erro_PagamentoAppService_ConsultarPagamentoBoleto_AoDesserializarCamposPagamento;
                        
                        if (consoleHangFire != null)
                            consoleHangFire.CreateExceptionMessage(ex, log, msg);

                        _logSistemaAppService.AddByJob(log, new
                        {
                            Solicitacao = solicitacao
                        }, ex);

                        throw;
                    }
                    
                    if (camposPagamento != null && !string.IsNullOrEmpty(camposPagamento.Identifier))
                    {

                        try
                        {
                            try
                            {
                                if (consoleHangFire != null)
                                    consoleHangFire.CreateConsoleMessage(null, $"Dados de pagamento: {solicitacao.CamposPagamento}");
                            }
                            catch { }

                            try
                            {
                                if (consoleHangFire != null)
                                    consoleHangFire.CreateConsoleMessage(null, "Requisitando para Api de Pagamentos para verificar dados do boleto!");

                                if (_apiPagamento != null
                                    && _apiPagamento._client != null
                                    && _apiPagamento._client.BaseAddress != null
                                    && !string.IsNullOrEmpty(_apiPagamento._client.BaseAddress.AbsoluteUri))
                                {
                                    var endpoint = _apiPagamento._client.BaseAddress.AbsoluteUri + 
                                        "api/v1/Boleto/ConsultaBoletoIdentificador?identificadorBoleto=" +
                                        $"{camposPagamento.Identifier}";
                                    if (consoleHangFire != null)
                                        consoleHangFire.CreateConsoleMessage(null, $"Requisitando para {endpoint}");
                                }
                                else
                                {
                                    if (consoleHangFire != null)
                                        consoleHangFire.CreateConsoleMessage(null, $"###### ERRO: Objeto '_apiPagamento' possui propriedades nulas!!!");
                                }
                                    
                            }
                            catch { }
                            

                            var response = await _apiPagamento.PostRetorno<RespostaConsultaBoleto>($"api/v1/Boleto/ConsultaBoletoIdentificador?identificadorBoleto=" +
                            $"{camposPagamento.Identifier}");

                            log = CodLogSistema.PagamentoAppService_ConsultarPagamentoBoleto_ConsultaBoletoIdentificador_Response;
                            try
                            {
                                string logText = JsonConvert.SerializeObject(response.Log);
                                string responseText = JsonConvert.SerializeObject(response);
                                string msgLog = $"IdSolicitacao = {idSolicitacao}, LogServico = {logText}, Identifier = {camposPagamento.Identifier}, ResponsePuro = {responseText}";
                                if (consoleHangFire != null)
                                    consoleHangFire.CreateConsoleMessage(log, msgLog);
                            }
                            catch { }

                            _logSistemaAppService.AddByJob(log, new
                            {
                                IdSolicitacao = idSolicitacao,
                                LogServico = response.Log,
                                Identifier = camposPagamento.Identifier
                            });

                            if (response.Sucesso)
                            {
                                if (response.ObjRetorno == null)
                                    response.ObjRetorno = new RespostaConsultaBoleto();
                                response.ObjRetorno.Sucesso = true;
                                return response.ObjRetorno;
                            }
                            else if (!string.IsNullOrEmpty(response.MensagemErro))
                            {
                                if (response.ObjRetorno == null)
                                    response.ObjRetorno = new RespostaConsultaBoleto();
                                response.ObjRetorno.MensagemErro = response.MensagemErro;
                                return response.ObjRetorno;
                            }

                            if (response.ObjRetorno == null)
                                response.ObjRetorno = new RespostaConsultaBoleto();

                            string statusResponse = $"Não existe boleto para esta solicitação {idSolicitacao}!";
                            try
                            {
                                if (consoleHangFire != null)
                                    consoleHangFire.CreateConsoleMessage(null, statusResponse);
                            }
                            catch { }

                            response.ObjRetorno.Status = statusResponse;
                            return response.ObjRetorno;
                        }
                        catch (Exception ex)
                        {
                            string msg = $"IdSolicitacao = {solicitacao.IdSolicitacao}";
                            log = CodLogSistema.Erro_PagamentoAppService_ConsultarPagamentoBoleto_ConsultaBoletoIdentificador_Response;

                            if (consoleHangFire != null)
                                consoleHangFire.CreateExceptionMessage(ex, log, msg);

                            _logSistemaAppService.AddByJob(log, new
                            {
                                Solicitacao = solicitacao
                            }, ex);


                            throw;
                        }
                        
                    }
                    else
                    {
                        try
                        {
                            if (consoleHangFire != null)
                                consoleHangFire.CreateConsoleMessage(null, $"Não foi localizado os dados de pagamento da solicitação {solicitacao.IdSolicitacao}");
                        }
                        catch { }
                    }
                }
            }

            return null;
        }

        public async Task<IEnumerable<SolicitacoesDto>> TodasSolicitacoesAguardandoPagamentoBoleto()
        {
            try
            {
                var solicitacoes = await _solicitacoesService.TodasSolicitacoesAguardandoPagamentoBoleto();
                if (solicitacoes == null)
                    return null;
                
                return _mapper.Map<IEnumerable<SolicitacoesDto>>(solicitacoes.ToList());
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}
