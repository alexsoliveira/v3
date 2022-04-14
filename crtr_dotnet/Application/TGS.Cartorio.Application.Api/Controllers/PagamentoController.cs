using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TGS.Cartorio.Application.Api.Controllers.Base;
using TGS.Cartorio.Application.AppServices;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Application.DTO;
using TGS.Cartorio.Application.DTO.Pagamento;
using TGS.Cartorio.Application.Enumerables;
using TGS.Cartorio.Application.ViewModel;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Infrastructure.Utility.ApiClient;
using TGS.Cartorio.Infrastructure.Utility.WebServerContracts.Interfaces;

namespace TGS.Cartorio.Application.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class PagamentoController : MainController
    {
        private readonly IPagamentoAppService _pagamentoAppService;
        private readonly ISolicitacoesAppService _solicitacoesAppService;
        private readonly IConfiguracoesAppService _configuracoesAppService;
        private readonly IPessoasAppService _pessoasAppService;
        private readonly IPessoasEnderecosAppService _pessoasEnderecosAppService;
        private readonly IUsuariosAppService _usuarioAppService;
        private readonly IMapper _mapper;
        private readonly ILogSistemaAppService _logSistemaAppService;
        private readonly ITaxasAppService _taxasAppService;
        private readonly ISmsAppService _smsAppService;
        private readonly IEmailAppService _emailAppService;

        public PagamentoController(IPagamentoAppService pagamentoAppService,
                                   ISolicitacoesAppService solicitacoesAppService,
                                   IMapper mapper, IPessoasAppService pessoasAppService,
                                   IUsuariosAppService usuarioAppService,
                                   IPessoasEnderecosAppService pessoasEnderecosAppService,
                                   ILogSistemaAppService logSistemaAppService,
                                   ISmsAppService smsAppService,
                                   IEmailAppService emailAppService, ITaxasAppService taxasAppService, IConfiguracoesAppService configuracoesAppService)
        {
            _pagamentoAppService = pagamentoAppService;
            _solicitacoesAppService = solicitacoesAppService;
            _mapper = mapper;
            _pessoasAppService = pessoasAppService;
            _usuarioAppService = usuarioAppService;
            _pessoasEnderecosAppService = pessoasEnderecosAppService;
            _logSistemaAppService = logSistemaAppService;
            _smsAppService = smsAppService;
            _emailAppService = emailAppService;
            _taxasAppService = taxasAppService;
            _configuracoesAppService = configuracoesAppService;
        }

        [HttpPost("SimularParcelamento/{valorTotal:decimal}")]
        public async Task<IActionResult> SimularParcelamento(decimal valorTotal)
        {
            string token = string.Empty;
            try
            {
                if (valorTotal <= 0)
                    return BadRequest("Valor total deve ser acima de 0!");

                token = GetToken();

            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_PagamentoAppService_SimularParcelamento_GetToken, new
                {
                    ValorTotal = valorTotal
                }, ex);

                return InternalServerError(GetTextoPadraoParaErroToken(), ex);
            }

            try
            {
                var parcelamento = await _pagamentoAppService.SimularParcelamento(valorTotal, token);

                if (parcelamento == null)
                    return BadRequest("Não foi possível retornar o valor de parcelamento!");

                else if (!parcelamento.Sucesso)
                    return StatusCode(500, parcelamento);

                return Ok(parcelamento);
            }
            catch (Exception ex)
            {
                return InternalServerError("Não foi possível simular o parcelamento!", ex);
            }
        }

        [HttpPost("ConsultarTaxaBoleto")]
        public async Task<IActionResult> ConsultarTaxaBoleto()
        {
            try
            {
                var taxaBoleto = await _taxasAppService.BuscarTaxaPorBoleto();
                return Ok(new { TaxaBoleto = taxaBoleto });
            }
            catch (Exception ex)
            {
                return InternalServerError("Ocorreu um erro ao consultar taxa do boleto!", ex);
            }
        }

        [HttpPost("PagarComCartaoCredito/{idSolicitacao:long}")]
        public async Task<IActionResult> PagarComCartaoCredito(long idSolicitacao, CartaoCreditoDto cartaoCredito)
        {

            CamposPagamentoViewModel camposPagamento = new CamposPagamentoViewModel();
            SolicitacaoConteudoViewModel solicitacaoConteudoValidacao = new SolicitacaoConteudoViewModel();

            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors.Select(err => err.ErrorMessage)));

            Solicitacoes solicitacao = null;
            try
            {
                solicitacao = await _solicitacoesAppService.BuscarId(idSolicitacao);
                if (solicitacao == null)
                    return StatusCode(500, $"A solicitação {idSolicitacao} não foi localizada!");
            }
            catch (Exception ex)
            {
                return InternalServerError("Ocorreu um erro ao buscar os dados da Solicitação!", ex);
            }


            if ((EstadosSolicitacao)solicitacao.IdSolicitacaoEstado != EstadosSolicitacao.AguardandoPagamento)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_PagamentoController_PagarComCartaoCredito_SolicitacaoComEstadoDiferenteDoMomentoDePagamento, new
                {
                    Solicitacao = solicitacao
                });

                return BadRequest($"A solicitação não está disponível para efetuar pagamentos.");
            }


            #region "7768 - Verificar se já existe pagamento"                
            // TODO: Verificar se existe campo pagamento dentro da tabela
            try
            {
                if (!string.IsNullOrEmpty(solicitacao.CamposPagamento))
                    camposPagamento = JsonConvert.DeserializeObject<CamposPagamentoViewModel>(solicitacao.CamposPagamento);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_PagamentoController_PagarComCartaoCredito_ErroAoDesserializarCamposPagamentoDaSolicitacao, new
                {
                    Solicitacao = solicitacao,
                    CamposPagamento = solicitacao.CamposPagamento
                });

                return InternalServerError("Ocorreu um erro ao buscar os dados da Solicitação.", ex);
            }


            try
            {
                if (!string.IsNullOrEmpty(solicitacao.Conteudo))
                    solicitacaoConteudoValidacao = JsonConvert.DeserializeObject<SolicitacaoConteudoViewModel>(solicitacao.Conteudo);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_PagamentoController_PagarComCartaoCredito_ErroAoDesserializarCampoConteudoDaSolicitacao, new
                {
                    Solicitacao = solicitacao,
                    CampoConteudo = solicitacao.Conteudo
                });

                return InternalServerError("Ocorreu um erro ao buscar os dados da Solicitação.", ex);
            }

            try
            {
                if (!solicitacao.IdPessoa.HasValue)
                    throw new Exception($"A solicitação {solicitacao.IdSolicitacao} não possui um Solicitante (campo IdPessoa)!");
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_PagamentoController_PagarComCartaoCredito_SolicitacaoEstaSemIdPessoa_Solicitante, new
                {
                    Solicitacao = solicitacao
                }, ex);

                return InternalServerError($"A solicitação {idSolicitacao} está sem um solicitante cadastrado, por favor, entre em contato com o suporte!");
            }



            string token = string.Empty;
            try
            {
                token = GetToken();
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_PagamentoController_PagarComCartaoCredito_GetToken, new
                {
                    Solicitacao = solicitacao
                }, ex);

                return InternalServerError(GetTextoPadraoParaErroToken(), ex);
            }


            if (camposPagamento.TipoPagamento == TiposPagamento.CartaoCredito.ToString()
                && (solicitacaoConteudoValidacao.EstadoPagamento == EstadosPagamento.PreAprovado.ToString()
                 || solicitacaoConteudoValidacao.EstadoPagamento == EstadosPagamento.Aprovado.ToString()))
            {
                RespostaConsultaTransacaoDto pagamentoCartao = null;

                if (!string.IsNullOrEmpty(camposPagamento.Identifier))
                {
                    try
                    {
                        pagamentoCartao = await _pagamentoAppService.ConsultarTransacaoCartaoCreditoPorIdentifier(camposPagamento.Identifier, token);

                        if (pagamentoCartao.chargeStatus == EstadosCartaoCredito.PAID.ToString()
                         || pagamentoCartao.chargeStatus == EstadosCartaoCredito.AUTHORIZED.ToString())
                            return Ok(new { Status = "PagamentoEfetuadoAnteriormente" });
                    }
                    catch (Exception ex)
                    {
                        await _logSistemaAppService.Add(CodLogSistema.Erro_PagamentoController_PagarComCartaoCredito_ConsultarTransacaoCartaoCreditoPorIdentifier,
                                new
                                {
                                    Sucesso = false,
                                    IdSolicitacao = idSolicitacao,
                                    Token = token,
                                    Identifier = camposPagamento.Identifier,
                                }, ex);

                        return InternalServerError("Ocorreu um erro ao tentar verificar se já existe um pagamento para esta solicitação.", ex);
                    }
                }
            }
            #endregion


            Pessoas solicitante = null;
            try
            {
                solicitante = await _pessoasAppService.BuscarPorIdCompleto(solicitacao.IdPessoa.Value);
                if (solicitante == null)
                {
                    await _logSistemaAppService.Add(CodLogSistema.Erro_PessoasAppService_BuscarPorIdCompleto_SolicitanteNaoLocalizado, new
                    {
                        Solicitacao = solicitacao,
                        IdPessoaSolicitante = solicitacao.IdPessoa.Value
                    });
                    return InternalServerError($"O solicitante da solicitação {idSolicitacao} não foi localizado!");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError($"Ocorreu um erro ao buscar os dados do solicitante!", ex);
            }

            if (solicitante.PessoasContatos == null
            || solicitante.PessoasContatos.Count == 0
            || !solicitante.PessoasContatos.Any(x => x.IdContatoNavigation != null))
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_PessoasAppService_BuscarPorIdCompleto_SolicitanteSemDadosContato, new
                {
                    Solicitacao = solicitacao,
                    IdPessoaSolicitante = solicitacao.IdPessoa.Value,
                    PessoasContatos = solicitante.PessoasContatos
                });
                return StatusCode(500, $"Não foi possível localizar o contato do solicitante da solicitação {idSolicitacao}!");
            }

            ContatoViewModel contatos = null;
            try
            {
                string conteudoContatos = solicitante.PessoasContatos.First(x => x.IdContatoNavigation != null).IdContatoNavigation.Conteudo;
                contatos = JsonConvert.DeserializeObject<ContatoViewModel>(conteudoContatos);
                if (contatos == null)
                {
                    await _logSistemaAppService.Add(CodLogSistema.Erro_PessoasAppService_BuscarPorIdCompleto_ErroAoDesserializarDadosContatoDoSolicitante, new
                    {
                        Solicitacao = solicitacao,
                        IdPessoaSolicitante = solicitacao.IdPessoa.Value,
                        PessoasContatos = solicitante.PessoasContatos,
                        ConteudoContatos = conteudoContatos
                    });
                    return InternalServerError($"Não foi possível localizar o contato do solicitante da solicitação {idSolicitacao}!");
                }
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_PessoasAppService_BuscarPorIdCompleto_ErroAoDesserializarDadosContatoDoSolicitante, new
                {
                    Solicitacao = solicitacao,
                    IdPessoaSolicitante = solicitacao.IdPessoa.Value,
                    PessoasContatos = solicitante.PessoasContatos
                }, ex);
                return InternalServerError($"Não foi possível localizar o contato do solicitante da solicitação {idSolicitacao}!", ex);
            }

            List<PessoasEnderecos> pessoasEnderecos = null;
            try
            {
                pessoasEnderecos = await _pessoasEnderecosAppService.BuscarPorPessoa(solicitante.IdPessoa);

                if (pessoasEnderecos == null
                    || pessoasEnderecos.Count == 0
                    || !pessoasEnderecos.Any(x => x.IdEnderecoNavigation != null))
                {
                    await _logSistemaAppService.Add(CodLogSistema.Erro_PagamentoController_PagarComCartaoCredito_BuscarPorIdPessoa_NaoFoiLocalizado, new
                    {
                        Solicitacao = solicitacao,
                        PessoasEnderecos = pessoasEnderecos
                    });

                    return InternalServerError($"Não foi possível localizar o endereço do solicitante da solicitação {idSolicitacao}!");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError($"Ocorreu um erro ao tentar localizar o endereço do solicitante da solicitação {idSolicitacao}!", ex);
            }

            EnderecoConteudoDto enderecoEntrega = null;
            try
            {
                var conteudoEndereco = pessoasEnderecos.First(x => x.IdEnderecoNavigation != null).IdEnderecoNavigation.Conteudo;
                enderecoEntrega = JsonConvert.DeserializeObject<EnderecoConteudoDto>(conteudoEndereco);
                if (enderecoEntrega == null)
                {
                    await _logSistemaAppService.Add(CodLogSistema.Erro_PagamentoController_ErroAoDesserializarEnderecoPessoaSolicitante, new
                    {
                        Solicitacao = solicitacao,
                        PessoasEnderecos = pessoasEnderecos,
                        ConteudoEndereco = conteudoEndereco
                    });
                    return InternalServerError($"Não foi possível localizar o endereço do solicitante da solicitação {idSolicitacao}!");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError($"Não foi possível localizar o endereço do solicitante da solicitação {idSolicitacao}!", ex);
            }

            Usuarios usuario = null;
            try
            {
                usuario = await _usuarioAppService.BuscarPorIdPessoa(solicitante.IdPessoa);
                if (usuario == null)
                {
                    await _logSistemaAppService.Add(CodLogSistema.Erro_PagamentoController_PagarComCartaoCredito_BuscarPorIdPessoa_NaoFoiLocalizado, new
                    {
                        Solicitacao = solicitacao,
                        IdPessoaSolicitante = solicitante.IdPessoa
                    });
                    return InternalServerError($"Não foi possível localizar o usuário do solicitante da solicitação {idSolicitacao}!");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError($"Não foi possível localizar o usuário do solicitante da solicitação {idSolicitacao}!", ex);
            }


            string ddd = "00";
            string celular = "000000000";
            try
            {
                var formatado = contatos.Celular.Replace("-", "").Replace(" ", "").Replace("(", "").Replace(")", "");
                ddd = formatado.Substring(0, 2);
                celular = formatado.Substring(2, formatado.Length - 2);
            }
            catch { }

            try
            {
                var composicao = await _taxasAppService.BuscarComposicaoPrecoProdutoTotal(idSolicitacao);
                if (composicao == null || !composicao.ValorTotal.HasValue)
                    throw new Exception("Não foi possível buscar a composição dos valores da solicitação.");

                var parcelamento = await _pagamentoAppService.SimularParcelamento(composicao.ValorTotal.Value, token);
                if (parcelamento == null || !parcelamento.Sucesso)
                    return BadRequest("Não foi possível retornar o valor de parcelamento!");

                var parcelaSelecionada = parcelamento.Parcelas.FirstOrDefault(p => p.Numero == cartaoCredito.NumeroParcelas);
                cartaoCredito.ValorTotal = parcelaSelecionada.ValorTotal;
                cartaoCredito.Cliente = new DadosClienteCartaoCreditoDto
                {
                    Endereco = new EnderecoCartaoCreditoDto
                    {
                        Bairro = enderecoEntrega.Bairro,
                        CEP = enderecoEntrega.Cep.Replace("-", ""),
                        Cidade = enderecoEntrega.Localidade,
                        Rua = enderecoEntrega.Logradouro,
                        Numero = enderecoEntrega.Numero,
                        UF = enderecoEntrega.Uf,
                        Complemento = enderecoEntrega.Complemento
                    },
                    NomeCompleto = cartaoCredito.Cartao.DonoCartao.Nome,
                    Email = usuario.Email,
                    Ddd = ddd,
                    Telefone = celular
                };

                cartaoCredito.Cartao.FormatarCartao();
            }
            catch (Exception ex)
            {
                cartaoCredito.Cartao.RemoverDadosPorSeguranca();
                cartaoCredito.Cliente.RemoverDadosPorSeguranca();

                await _logSistemaAppService.Add(CodLogSistema.Erro_PagamentoController_PagarComCartaoCredito_ErroAoMontarDadosPagamento, new
                {
                    Solicitacao = solicitacao,
                    IdPessoaSolicitante = solicitante.IdPessoa,
                    DadosCartaoCredito = cartaoCredito
                }, ex);

                return InternalServerError("Ocorreu um erro ao montar os dados de pagamento!", ex);
            }

            Retorno<RespostaPagamentoCartaoCreditoDto> responsePagamento = null;
            try
            {
                responsePagamento = await _pagamentoAppService.PagarComCartaoCredito(cartaoCredito, token);

                cartaoCredito.Cartao.RemoverDadosPorSeguranca();
                cartaoCredito.Cliente.RemoverDadosPorSeguranca();

                await _logSistemaAppService.Add(CodLogSistema.PagamentoAppService_PagarComCartaoCredito, new
                {
                    Sucesso = responsePagamento != null && responsePagamento.Sucesso,
                    Token = token,
                    Solicitacao = solicitacao,
                    DadosCartaoCredito = cartaoCredito,
                    LogServico = responsePagamento.Log
                });

                if (responsePagamento == null || !responsePagamento.Sucesso)
                {
                    if (responsePagamento.ObjRetorno != null
                        && responsePagamento.ObjRetorno.StatusCodeServicoPagamento == (int)HttpStatusCode.NotFound)
                        return InternalServerError($"Verifique os dados do seu cartão de crédito!",
                            new Exception(responsePagamento.MensagemErro));
                    else
                        return InternalServerError($"Ocorreu um erro ao processar o pagamento!",
                            new Exception(responsePagamento.MensagemErro));
                }
                    

            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_PagamentoController_PagarComCartaoCredito,
                    new
                    {
                        Sucesso = false,
                        Token = token,
                        Solicitacao = solicitacao,
                        DadosCartaoCredito = cartaoCredito
                    }, ex);

                return InternalServerError("Ocorreu um erro ao processar o pagamento!", ex);
            }

            SolicitacaoConteudoViewModel solicitacaoConteudo = new SolicitacaoConteudoViewModel();
            EstadosPagamento estadoPagamento = EstadosPagamento.StatusNaoReconhecido;
            try
            {
                estadoPagamento = solicitacaoConteudo.GetEstadoPagamentoCartaoCredito(responsePagamento.ObjRetorno.Status);
                solicitacao.Conteudo = solicitacaoConteudo.PreencherComRetornoSerializado(TiposPagamento.CartaoCredito,
                                                                                          estadoPagamento);

                if (estadoPagamento == EstadosPagamento.Aprovado)
                {
                    solicitacao.IdSolicitacaoEstado = GerenciadorEstadosSolicitacao.ProximoEstadoSolicitacao(
                        solicitacao.IdSolicitacaoEstado, TelasSolicitacao.Pagamento);

                    //enviar e-mail com confirmação de pagamento
                    string mensagemEmail = await _emailAppService.GetTemplateConfirmacaoPagamento(solicitacao.IdSolicitacao.ToString(),
                        usuario.NomeUsuario,
                        cartaoCredito.NumeroParcelas.ToString(),
                        cartaoCredito.ValorTotal.ToString(),
                        cartaoCredito.Cartao.NumeroCartao);
                    
                    await _emailAppService.Send(null,
                        mensagemEmail,
                        nomeUsuario: usuario.NomeUsuario,
                        emailUsuario: usuario.Email,
                        idSolicitacao: solicitacao.IdSolicitacao,
                        assunto: "Confirmação de Pagamento");
                }
                    

                if (!string.IsNullOrEmpty(solicitacao.CamposPagamento))
                    camposPagamento = JsonConvert.DeserializeObject<CamposPagamentoViewModel>(solicitacao.CamposPagamento);

                solicitacao.CamposPagamento = camposPagamento.PreencherCartaoCreditoSerializado(responsePagamento.ObjRetorno, cartaoCredito);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_PagamentoController_PagarComCartaoCredito_AoPrepararSolicitacaoParaAtualizarEstado,
                    new
                    {
                        EstadoPagamento = estadoPagamento,
                        SolicitacaoConteudo = solicitacaoConteudo,
                        Solicitacao = solicitacao,
                        CamposPagamento = solicitacao.CamposPagamento
                    }, ex);

                return InternalServerError("Ocorreu um erro ao atualizar a Solicitação! O pagamento foi efetuado com sucesso!", ex);
            }

            SolicitacoesDto solicitacaoDto = null;
            try
            {
                solicitacaoDto = _mapper.Map<SolicitacoesDto>(solicitacao);
                if (solicitacaoDto == null)
                    throw new Exception("");
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_PagamentoController_PagarComCartaoCredito_DesserializarSolicitacaoEmSolicitacaoDto,
                    new
                    {
                        Solicitacao = solicitacao
                    }, ex);

                return InternalServerError("Ocorreu um erro ao atualizar a Solicitação! O pagamento foi efetuado com sucesso!", ex);
            }

            try
            {
                await _solicitacoesAppService.Atualizar(solicitacaoDto);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_PagamentoController_PagarComCartaoCredito_AoAtualizarSolicitacao,
                    new
                    {
                        Solicitacao = solicitacao,
                        SolicitacaoDto = solicitacaoDto
                    }, ex);

                return InternalServerError("Ocorreu um erro ao atualizar a Solicitação! O pagamento foi efetuado com sucesso!", ex);
            }

            return Ok(new { Status = camposPagamento.Status });
        }

        [HttpGet("ConsultarTransacaoCartaoCredito/{idSolicitacao:long}")]
        public async Task<IActionResult> ConsultarTransacaoCartaoCredito(long idSolicitacao)
        {

            string token = string.Empty;
            try
            {
                token = GetToken();
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_PagamentoController_ConsultarTransacaoCartaoCredito_GetToken, new
                {
                    IdSolicitacao = idSolicitacao
                }, ex);

                return InternalServerError(GetTextoPadraoParaErroToken(), ex);
            }

            try
            {
                var pagamentoCartao = await _pagamentoAppService.ConsultarTransacaoCartaoCreditoPorSolicitacao(idSolicitacao, token);

                if (pagamentoCartao != null && !string.IsNullOrEmpty(pagamentoCartao.MensagemErro))
                    return InternalServerError($"Ocorreu um erro no serviço de cartão de crédito!", new Exception(pagamentoCartao.MensagemErro));

                // quando não há transação de crédito
                else if (pagamentoCartao == null)
                    return Ok(new { Status = "" });

                return Ok(new { Status = pagamentoCartao.chargeStatus });
            }
            catch (Exception ex)
            {
                return InternalServerError("Ocorreu um erro ao tentar consultar transação de cartão de crédito!", ex);
            }
        }

        [HttpPost("PagarViaBoleto/{idSolicitacao:long}")]
        public async Task<IActionResult> PagarViaBoleto(long idSolicitacao, BoletoDto boletoDto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors.Select(err => err.ErrorMessage)));

            Solicitacoes solicitacao = null;
            try
            {
                solicitacao = await _solicitacoesAppService.BuscarId(idSolicitacao);
                if (solicitacao == null)
                {
                    await _logSistemaAppService.Add(CodLogSistema.PagamentoController_PagarViaBoleto_SolicitacaoBuscarId, new
                    {
                        IdSolicitacao = idSolicitacao,
                        BoletoDto = boletoDto
                    });

                    return InternalServerError($"A solicitação {idSolicitacao} não foi localizada!");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError($"Ocorreu um erro ao buscar os dados da solicitação {idSolicitacao}!", ex);
            }


            if (!solicitacao.IdPessoa.HasValue)
            {
                await _logSistemaAppService.Add(CodLogSistema.PagamentoController_PagarViaBoleto_SolicitacaoSemIdPessoaSolicitante, new
                {
                    Solicitacao = solicitacao,
                    BoletoDto = boletoDto
                });

                return InternalServerError($"A solicitação {idSolicitacao} não possui um solicitante cadastrado, entre em contato com o Suporte!");
            }

            Pessoas solicitante = null;
            try
            {
                solicitante = await _pessoasAppService.BuscarPorIdCompleto(solicitacao.IdPessoa.Value);
                if (solicitante == null)
                {
                    await _logSistemaAppService.Add(CodLogSistema.PagamentoController_PagarViaBoleto_SolicitanteNaoLocalizado, new
                    {
                        IdPessoaSolicitante = solicitacao.IdPessoa.Value,
                        Solicitacao = solicitacao,
                        BoletoDto = boletoDto
                    });

                    return InternalServerError($"O solicitante da solicitação {idSolicitacao} não foi localizado!");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError($"Ocorreu um erro ao buscar os dados do solicitante da solicitação {idSolicitacao}!", ex);
            }

            List<PessoasEnderecos> pessoasEnderecos = null;
            try
            {
                pessoasEnderecos = await _pessoasEnderecosAppService.BuscarPorPessoa(solicitante.IdPessoa);
                if (pessoasEnderecos == null
                    || pessoasEnderecos.Count == 0
                    || !pessoasEnderecos.Any(x => x.IdEnderecoNavigation != null))
                {
                    await _logSistemaAppService.Add(CodLogSistema.PagamentoController_PagarViaBoleto_SolicitanteEstaSemEndereco, new
                    {
                        IdPessoaSolicitante = solicitacao.IdPessoa.Value,
                        Solicitacao = solicitacao,
                        BoletoDto = boletoDto
                    });

                    return InternalServerError($"Não foi possível localizar o endereço do solicitante da solicitação {idSolicitacao}!");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError($"Ocorreu um erro ao buscar os dados do endereço do solicitante da solicitação {idSolicitacao}!", ex);
            }

            EnderecoConteudoDto enderecoEntrega = null;
            string conteudoEndereco = null;
            try
            {
                conteudoEndereco = pessoasEnderecos.First(x => x.IdEnderecoNavigation != null).IdEnderecoNavigation.Conteudo;
                enderecoEntrega = JsonConvert.DeserializeObject<EnderecoConteudoDto>(conteudoEndereco);
                if (enderecoEntrega == null)
                {
                    await _logSistemaAppService.Add(CodLogSistema.Erro_PagamentoController_PagarViaBoleto_AoDesserializarEnderecoSolicitante, new
                    {
                        IdPessoaSolicitante = solicitacao.IdPessoa.Value,
                        Solicitacao = solicitacao,
                        BoletoDto = boletoDto,
                        ConteudoEndereco = conteudoEndereco
                    });

                    return InternalServerError($"Não foi possível localizar o endereço do solicitante da solicitação {idSolicitacao}!");
                }
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_PagamentoController_PagarViaBoleto_AoDesserializarEnderecoSolicitante, new
                {
                    IdPessoaSolicitante = solicitacao.IdPessoa.Value,
                    Solicitacao = solicitacao,
                    BoletoDto = boletoDto,
                    ConteudoEndereco = conteudoEndereco
                }, ex);
                return InternalServerError($"Não foi possível localizar o endereço do solicitante da solicitação {idSolicitacao}!", ex);
            }

            Usuarios usuarioEmail = null;
            try
            {
                usuarioEmail = await _usuarioAppService.BuscarPorIdPessoa(solicitacao.IdPessoa.Value);

                if (usuarioEmail == null)
                {
                    await _logSistemaAppService.Add(CodLogSistema.Erro_PagamentoController_PagarViaBoleto_UsuarioNaoLocalizado, new
                    {
                        IdPessoaSolicitante = solicitacao.IdPessoa.Value,
                        Solicitacao = solicitacao,
                        BoletoDto = boletoDto,
                        ConteudoEndereco = conteudoEndereco
                    });

                    return InternalServerError($"Não foi possível localizar o e-mail do solicitante da solicitação {idSolicitacao}!");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError($"Não foi possível localizar o e-mail do solicitante da solicitação {idSolicitacao}!", ex);
            }

            ComposicaoProdutoValorTotalDto composicao = null;
            try
            {
                composicao = await _taxasAppService.BuscarComposicaoPrecoProdutoTotal(idSolicitacao);
                if (composicao == null || !composicao.ValorTotal.HasValue)
                    throw new Exception("Não foi possível buscar a composição dos valores da solicitação.");
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_PagamentoController_PagarViaBoleto_TaxasAppService_BuscarComposicaoPrecoProdutoTotal, new
                {
                    Solicitacao = solicitacao
                }, ex);
                return InternalServerError($"Ocorreu um erro ao buscar os dados da solicitação {idSolicitacao}!", ex);
            }


            decimal? taxaBoleto = null;
            try
            {
                taxaBoleto = await _taxasAppService.BuscarTaxaPorBoleto();
                if (!taxaBoleto.HasValue)
                    throw new Exception("Taxa do boleto não retornou valor!", new Exception(JsonConvert.SerializeObject(taxaBoleto)));
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_PagamentoController_PagarViaBoleto_TaxasAppService_BuscarTaxaPorBoleto, new
                {
                    IdSolicitacao = idSolicitacao
                }, ex);
                return InternalServerError($"Ocorreu um erro ao buscar os dados da Solicitação!", ex);
            }

            boletoDto.Valor = decimal.Round(composicao.ValorTotal.Value + taxaBoleto.Value, 2);

            var dataVencimento = DateTime.Now.ToString("ddMMyyyy");
            boletoDto.DataVencimento = dataVencimento;
            boletoDto.Pagador.Uf = enderecoEntrega.Uf;
            boletoDto.Pagador.Cidade = enderecoEntrega.Localidade;
            boletoDto.Pagador.Endereco = enderecoEntrega.Logradouro + " " + enderecoEntrega.Numero;
            boletoDto.Pagador.Bairro = enderecoEntrega.Bairro;
            boletoDto.Pagador.Cep = enderecoEntrega.Cep.Replace("-", "");
            boletoDto.Emails = new string[] { usuarioEmail.Email.ToString() };

            string token = string.Empty;
            try
            {
                token = GetToken();
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_PagamentoController_PagarViaBoleto_GetToken, new
                {
                    Solicitacao = solicitacao,
                    Usuario = usuarioEmail,
                    Solicitante = solicitante
                }, ex);

                return InternalServerError(GetTextoPadraoParaErroToken(), ex);
            }

            string novoIdentifier = null;
            try
            {
                novoIdentifier = await _solicitacoesAppService.GerarBoleto(solicitacao, boletoDto, token);

                if (string.IsNullOrEmpty(novoIdentifier))
                    return InternalServerError("Ocorreu um erro ao tentar gerar o boleto!");
            }
            catch (Exception ex)
            {
                return InternalServerError("Ocorreu um erro ao tentar gerar o boleto!", ex);
                throw;
            }


            return Ok(new { identificador = novoIdentifier });
        }

        [HttpGet("ConsultarPagamentoBoleto/{idSolicitacao:long}")]
        public async Task<IActionResult> ConsultarPagamentoBoleto(long idSolicitacao)
        {

            try
            {
                var boletoPago = await _pagamentoAppService.ConsultarPagamentoBoleto(null, idSolicitacao);

                if (boletoPago != null && !string.IsNullOrEmpty(boletoPago.MensagemErro))
                    return InternalServerError($"Ocorreu um erro no serviço de boleto.", new Exception(boletoPago.MensagemErro));
                else if (boletoPago == null)
                    return Ok(new { Status = "" });
                else if (boletoPago.Sucesso)
                    return Ok(boletoPago);
                else
                    return BadRequest(boletoPago);
            }
            catch (Exception ex)
            {
                return InternalServerError($"Ocorreu um erro no serviço de boleto!", ex);
            }
        }
    }
}