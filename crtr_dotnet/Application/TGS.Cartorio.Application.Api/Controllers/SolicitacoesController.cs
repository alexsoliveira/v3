using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Application.Api.Controllers.Base;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Application.DTO;
using TGS.Cartorio.Application.DTO.Products.Matrimonio;
using TGS.Cartorio.Application.ViewModel;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Entities.Procuracoes;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer.Procuracoes;
using TGS.Cartorio.Domain.Services.OutorgadoProcuracoesPartesStrategy;

namespace TGS.Cartorio.Application.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class SolicitacoesController : MainController
    {
        #region Variaveis 
        private readonly ISolicitacoesAppService _solicitacoesAppService;
        private readonly ISolicitacoesEstadosAppService _solicitacoesEstadosAppService;
        private readonly ILogger<SolicitacoesController> _logger;
        private readonly IMapper _mapper;
        private readonly IPessoasAppService _pessoasAppService;
        private readonly IPessoasFisicasAppService _pessoasfisicasappservice;
        private readonly IPessoasJuridicasAppService _pessoasjuridicasappservice;
        private readonly ICartoriosAppService _cartoriosAppService;
        private readonly ISolicitacoesDocumentosAppService _solicitacoesDocumentosAppService;
        private readonly ISolicitacoesNotificacoesAppService _solicitacoesNotificacoesAppService;
        private readonly IPessoasEnderecosAppService _pessoasEnderecosAppService;
        private readonly IProdutosAppService _produtosAppService;
        private readonly IProcuracoesPartesAppService _procuracoesPartesAppService;
        private readonly IUsuariosSqlRepository _usuarioRepository;
        private readonly IProcuracoesPartesSqlRepository _procuracoesPartesSqlRepository;
        private readonly IProcuracoesPartesEstadosSqlRepository _procuracoesPartesEstadosSqlRepository;
        private readonly IPessoasSqlRepository _pessoaSqlRepository;
        private readonly IMatrimoniosAppService _matrimoniosAppService;
        private readonly ILogSistemaAppService _logSistemaAppService;

        #endregion

        public SolicitacoesController(ILogger<SolicitacoesController> logger,
            ISolicitacoesAppService solicitacoesAppService,
            ICartoriosAppService cartoriosAppService,
            IMapper mapper,
            ISolicitacoesDocumentosAppService solicitacoesDocumentosAppService,
            IPessoasAppService pessoasAppService,
            IPessoasFisicasAppService pessoasfisicasappservice,
            IPessoasJuridicasAppService pessoasjuridicasappservice,
            ISolicitacoesNotificacoesAppService solicitacoesNotificacoesAppService,
            IPessoasEnderecosAppService pessoasEnderecosAppService,
            IProdutosAppService produtosAppService,
            IUsuariosSqlRepository usuarioRepository,
            IProcuracoesPartesSqlRepository procuracoesPartesSqlRepository,
            IPessoasSqlRepository pessoasSqlRepository,
            IMatrimoniosAppService matrimoniosAppService,
            IProcuracoesPartesEstadosSqlRepository procuracoesPartesEstadosSqlRepository,
            IProcuracoesPartesAppService procuracoesPartesAppService,
            ISolicitacoesEstadosAppService solicitacoesEstadosAppService,
            ILogSistemaAppService logSistemaAppService)
        {
            _logger = logger;
            _solicitacoesAppService = solicitacoesAppService;
            _cartoriosAppService = cartoriosAppService;
            _mapper = mapper;
            _solicitacoesDocumentosAppService = solicitacoesDocumentosAppService;
            _pessoasAppService = pessoasAppService;
            _pessoasfisicasappservice = pessoasfisicasappservice;
            _pessoasjuridicasappservice = pessoasjuridicasappservice;
            _solicitacoesNotificacoesAppService = solicitacoesNotificacoesAppService;
            _pessoasEnderecosAppService = pessoasEnderecosAppService;
            _produtosAppService = produtosAppService;
            _usuarioRepository = usuarioRepository;
            _procuracoesPartesSqlRepository = procuracoesPartesSqlRepository;
            _pessoaSqlRepository = pessoasSqlRepository;
            _matrimoniosAppService = matrimoniosAppService;
            _procuracoesPartesEstadosSqlRepository = procuracoesPartesEstadosSqlRepository;
            _procuracoesPartesAppService = procuracoesPartesAppService;
            _solicitacoesEstadosAppService = solicitacoesEstadosAppService;
            _logSistemaAppService = logSistemaAppService;
        }

        [HttpPost("IncluirSolicitacao")]
        public async Task<IActionResult> IncluirSolicitacao([FromBody] SolicitacoesSimplificadoDto solicitacao)
        {
            try
            {
                return Ok(await _solicitacoesAppService.Incluir(solicitacao));
            }
            catch (Exception ex)
            {
                return StatusCode(500, GetMessageError(ex));
            }
        }

        [HttpGet("MinhasSolicitacoes/{id:long}")]
        public async Task<IActionResult> MinhasSolicitacoes(long id)
        {
            try
            {
                var minhasSolicitacoes = await _solicitacoesAppService.MinhasSolicitacoes(id);
                if (minhasSolicitacoes == null)
                    return NotFound();

                List<MinhasSolicitacoesDto> minhasSolicitacoesDto = new List<MinhasSolicitacoesDto>();
                foreach (var item in minhasSolicitacoes)
                {
                    MinhasSolicitacoesDto minhasSolicitacao = new MinhasSolicitacoesDto();
                    minhasSolicitacao.IdSolicitacao = item.IdSolicitacao;
                    minhasSolicitacao.Participacao = item.Participacao;
                    minhasSolicitacao.Produto = item.Produto;
                    if (!string.IsNullOrEmpty(item.Conteudo))
                    {
                        var campos = JsonConvert.DeserializeObject<SolicitacaoConteudoViewModel>(item.Conteudo);
                        minhasSolicitacao.TipoPagamento = campos.TipoPagamentoAtual;
                        minhasSolicitacao.EstadoPagamento = campos.EstadoPagamento;
                    }
                    minhasSolicitacao.DataSolicitacao = item.DataSolicitacao.Value;
                    minhasSolicitacao.Estado = item.Estado;
                    minhasSolicitacao.UltimaInteracao = item.UltimaInteracao;
                    minhasSolicitacoesDto.Add(minhasSolicitacao);
                }

                return Ok(minhasSolicitacoesDto);
            }
            catch (Exception ex)
            {
#if DEBUG
                return BadRequest(ex.InnerException?.Message ?? ex.Message);
#else
                return BadRequest("Não foi possível retornar os dados.");
#endif
            }
        }

        [HttpGet("ConsultarBoleto/{id:long}")]
        public async Task<IActionResult> ConsultarBoleto(long id)
        {
            MinhaSolicitacao minhaSolicitacao = null;
            try
            {
                minhaSolicitacao = await _solicitacoesAppService.ConsultarBoleto(id);
            }
            catch (Exception ex)
            {
                return InternalServerError("Não foi possível gerar o boleto!", ex);
            }


            PagadorBoletoDto pagador = new PagadorBoletoDto();
            EnderecosDto enderecoEntrega = null;
            BoletoDto boletoDto = null;
            try
            {
                if (minhaSolicitacao == null
                || string.IsNullOrEmpty(minhaSolicitacao.CamposPagamento)
                || string.IsNullOrEmpty(minhaSolicitacao.EnderecoPagador)
                || minhaSolicitacao.PessoaSolicitante == null
                || minhaSolicitacao.PessoaSolicitante.IdUsuarioNavigation == null)
                    return NotFound();

                var campos = JsonConvert.DeserializeObject<CamposPagamentoViewModel>(minhaSolicitacao.CamposPagamento);

                if (campos.Valor <= 0)
                    return BadRequest("Valor abaixo do permitido para gerar cobrança!");

                if (campos != null && campos.IsBoletoDeHoje())
                    return Ok(campos.Identifier);

                else
                {
                    if (!string.IsNullOrEmpty(minhaSolicitacao.EnderecoPagador))
                        enderecoEntrega = JsonConvert.DeserializeObject<EnderecosDto>(minhaSolicitacao.EnderecoPagador);

                    pagador.Uf = enderecoEntrega.Conteudo.Uf;
                    pagador.Cidade = enderecoEntrega.Conteudo.Localidade;
                    pagador.Endereco = enderecoEntrega.Conteudo.Logradouro + " " + enderecoEntrega.Conteudo.Numero;
                    pagador.Bairro = enderecoEntrega.Conteudo.Bairro;
                    pagador.Cep = enderecoEntrega.Conteudo.Cep.Replace("-", "");
                    pagador.Nome = minhaSolicitacao.PessoaSolicitante.IdUsuarioNavigation.NomeUsuario;
                    pagador.NumeroDocumento = minhaSolicitacao.PessoaSolicitante.Documento.ToString();

                    string[] emails = null;
                    var contatosConteudo = minhaSolicitacao.GetContatosConteudo();
                    if (contatosConteudo != null && !string.IsNullOrEmpty(contatosConteudo.Email))
                        emails = new string[] { contatosConteudo.Email };

                    boletoDto = new BoletoDto(id, campos.Valor, emails, pagador);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError("Não foi possível gerar o boleto!", ex);
            }


            string token = null;
            try
            {
                token = GetToken();
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_SolicitacoesController_ConsultarBoleto_GetToken, new
                {
                    IdSolicitacao = id,
                    MinhaSolicitacao = minhaSolicitacao
                }, ex);

                return InternalServerError(GetTextoPadraoParaErroToken(), ex);
            }

            try
            {
                var novoIdentifier = await _solicitacoesAppService.GerarBoleto(minhaSolicitacao.Solicitacao, boletoDto, token);

                if (!string.IsNullOrEmpty(novoIdentifier))
                    return Ok(novoIdentifier);

                return InternalServerError("Ocorreu um erro ao tentar gerar o boleto!");
            }
            catch (Exception ex)
            {
                return InternalServerError(GetTextoPadraoParaErroToken(), ex);
            }
        }

        [HttpGet("BuscarId/{id:int}")]
        public async Task<IActionResult> BuscarId(int id)
        {
            try
            {
                return Ok(await _solicitacoesAppService.BuscarId(id));
            }
            catch (Exception ex)
            {
#if DEBUG
                return BadRequest(ex.InnerException?.Message ?? ex.Message);
#else
                return BadRequest("Não foi possível retornar os dados.");
#endif
            }
        }

        [HttpPost("CriarOutorgantes")]
        public async Task<IActionResult> CriarOutorgantes([FromBody] SolicitacoesOutorgantesDto solicitacaoDto)
        {
            try
            {
                if (solicitacaoDto == null || solicitacaoDto.IdSolicitacao == 0 || solicitacaoDto.IdPessoaSolicitante == 0)
                    return BadRequest("idSolicitacao ou idPessoaSolicitante incorreto!");

                if (solicitacaoDto.AlteracaoDaSolicitacao)
                {
                    await _procuracoesPartesAppService.AtualizarOutorgantes(solicitacaoDto);
                }
                else
                {
                    var solicitacao = await _solicitacoesAppService.BuscarId(solicitacaoDto.IdSolicitacao);
                    solicitacao.IdPessoa = solicitacaoDto.IdPessoaSolicitante;
                    await _solicitacoesAppService.Atualizar(_mapper.Map<SolicitacoesDto>(solicitacao));

                    if (solicitacaoDto.Outogantes != null)
                    {
                        foreach (var item in solicitacaoDto.Outogantes)
                        {
                            await item.ValidarRegrasCriacaoOutorgante(
                                  solicitacaoDto.IdPessoaSolicitante,
                                  this._procuracoesPartesSqlRepository,
                                  this._procuracoesPartesEstadosSqlRepository,
                                  this._usuarioRepository,
                                  this._pessoaSqlRepository,
                                  this._mapper);
                        }
                    }
                }

                return NoContent();
            }
            catch (Exception ex)
            {
#if DEBUG
                return BadRequest(ex.InnerException?.Message ?? ex.Message);
#else
                return BadRequest("Não foi possível retornar os dados.");
#endif
            }
        }


        [HttpPost("CriarOutorgados")]
        public async Task<IActionResult> CriarOutorgados([FromBody] SolicitacoesOutorgadosDto solicitacoesOutorgados)
        {
            try
            {
                if (solicitacoesOutorgados.Outorgados == null
                    || !solicitacoesOutorgados.IdSolicitacao.HasValue
                    || string.IsNullOrEmpty(solicitacoesOutorgados.RepresentacaoPartes))
                    return BadRequest();


                var solicitacao = await _solicitacoesAppService.BuscarId(solicitacoesOutorgados.IdSolicitacao.Value);
                SolicitacaoConteudoViewModel solicitacoesConteudo = new SolicitacaoConteudoViewModel();

                try
                {
                    if (!string.IsNullOrEmpty(solicitacao.Conteudo))
                        solicitacoesConteudo = JsonConvert.DeserializeObject<SolicitacaoConteudoViewModel>(solicitacao.Conteudo);
                }
                catch { }

                solicitacoesConteudo.RepresentacaoPartes = "{\"representacaoPartes\":\"" + solicitacoesOutorgados.RepresentacaoPartes + "\"}";
                solicitacao.Conteudo = JsonConvert.SerializeObject(solicitacoesConteudo);
                await _solicitacoesAppService.Atualizar(_mapper.Map<SolicitacoesDto>(solicitacao));



                if (solicitacoesOutorgados.AlteracaoDaSolicitacao)
                {
                    await _procuracoesPartesAppService.AtualizarOutorgados(solicitacoesOutorgados);
                }
                else
                {
                    foreach (var outorgadoDto in solicitacoesOutorgados.Outorgados)
                    {
                        await OutorgadoContext.Resolve(_mapper.Map<Outorgados>(outorgadoDto),
                                                       _mapper,
                                                       _procuracoesPartesSqlRepository,
                                                       _usuarioRepository,
                                                       _pessoaSqlRepository);
                    }
                }

                return NoContent();
            }
            catch (Exception ex)
            {
#if DEBUG
                return BadRequest(ex.InnerException?.Message ?? ex.Message);
#else
                return BadRequest("Não foi possível retornar os dados.");
#endif
            }
        }


        [HttpPost("CriarMatrimonio")]
        public async Task<IActionResult> CriarMatrimonio([FromBody] DadosMatrimonioDto matrimoniosDto)
        {
            try
            {
                if (matrimoniosDto == null)
                    return BadRequest();

                long idMatrimonio = 0;

                if (matrimoniosDto.AlteracaoDaSolicitacao)
                    idMatrimonio = await _matrimoniosAppService.Atualizar(matrimoniosDto);
                else
                    idMatrimonio = await _matrimoniosAppService.Incluir(matrimoniosDto);

                if (idMatrimonio == 0)
                    throw new Exception("Ocorreu um erro ao tentar incluir ou atualizar os dados do matrimônio!");

                return Ok(new { idMatrimonio = idMatrimonio });
            }
            catch (Exception ex)
            {
#if DEBUG
                return BadRequest(ex.InnerException?.Message ?? ex.Message);
#else
                return BadRequest("Não foi possível retornar os dados.");
#endif
            }
        }



        [HttpPost("CadastrarInfomacoesImportantes")]
        public async Task<IActionResult> CadastrarInfomacoesImportantes([FromBody] InformacoesImportantes info)
        {
            try
            {
                if (info == null
                    || string.IsNullOrEmpty(info.Informacoes)
                    || info.IdSolicitacao < 1)
                    return BadRequest();

                var solicitacoes = await _solicitacoesAppService.BuscarId(info.IdSolicitacao);
                SolicitacaoConteudoViewModel solicitacoesConteudo = new SolicitacaoConteudoViewModel();

                try
                {
                    if (!string.IsNullOrEmpty(solicitacoes.Conteudo))
                        solicitacoesConteudo = JsonConvert.DeserializeObject<SolicitacaoConteudoViewModel>(solicitacoes.Conteudo);
                }
                catch { }

                solicitacoesConteudo.InformacoesImportantes = info.Informacoes;
                solicitacoes.Conteudo = JsonConvert.SerializeObject(solicitacoesConteudo);

                await _solicitacoesAppService.Atualizar(_mapper.Map<SolicitacoesDto>(solicitacoes));

                return NoContent();
            }
            catch (Exception ex)
            {
#if DEBUG
                return BadRequest(ex.InnerException?.Message ?? ex.Message);
#else
                return BadRequest("Não foi possível retornar os dados.");
#endif
            }
        }


        [HttpPost("AtualizarSolicitacaoParaCarrinho/{idSolicitacao:long}")]
        public async Task<IActionResult> AtualizarSolicitacaoParaCarrinho(long idSolicitacao)
        {
            try
            {
                if (idSolicitacao < 1)
                    return BadRequest("Solicitação não existente!");

                await _solicitacoesAppService.AtualizarSolicitacaoParaCarrinho(idSolicitacao);
                return Ok(true);
            }
            catch (Exception ex)
            {
                return InternalServerError("Ocorreu um erro ao atualizar a solicitação.", ex);
            }
        }

        [HttpPost("AtualizarSolicitacaoParaAguardandoAssinaturaDigitalSolicitante/{idSolicitacao:long}")]
        public async Task<IActionResult> AtualizarSolicitacaoParaAguardandoAssinaturaDigitalSolicitante(long idSolicitacao)
        {
            try
            {
                if (idSolicitacao < 1)
                    return BadRequest("Solicitação não existente!");

                await _solicitacoesAppService.AtualizarSolicitacaoParaAguardandoAssinaturaDigitalSolicitante(idSolicitacao);
                return Ok(true);
            }
            catch (Exception ex)
            {
                return InternalServerError("Ocorreu um erro ao atualizar a solicitação.", ex);
            }
        }

        [HttpGet("BuscarDadosStatusSolicitacao/{idSolicitacao:long}")]
        public async Task<IActionResult> BuscarDadosStatusSolicitacao(long idSolicitacao)
        {
            try
            {
                if (idSolicitacao < 1)
                    return BadRequest();

                var dados = await _solicitacoesAppService.BuscarDadosStatusSolicitacao(idSolicitacao);
                if (dados == null)
                    return NotFound();

                return Ok(dados);
            }
            catch (Exception ex)
            {
                return InternalServerError("Ocorreu um erro ao buscar o status da solicitação.", ex);
            }
        }

        [HttpGet("BuscarEstadoDoPedidoPorParticipante/{idSolicitacao:long}")]
        public async Task<IActionResult> BuscarEstadoDoPedidoPorParticipante(long idSolicitacao)
        {
            try
            {
                if (idSolicitacao < 1)
                    return BadRequest();

                var statusSolicitacaos = await _solicitacoesAppService.BuscarEstadoDoPedidoPorParticipante(idSolicitacao);

                return Ok(statusSolicitacaos);
            }
            catch (Exception ex)
            {
                return InternalServerError("Ocorreu um erro ao buscar o status da solicitação.", ex);
            }
        }

        [HttpGet("BuscarTodasProcuracoesPartesEstadosQueEstaoAtivos")]
        public async Task<IActionResult> BuscarTodasProcuracoesPartesEstadosQueEstaoAtivas()
        {
            try
            {
                var procuracoesPartesEstadosAtivas = await _solicitacoesAppService.BuscarTodasProcuracoesPartesEstados(p => p.FlagAtivo == true
                                                                                                                         && p.NuOrdem > 0);

                return Ok(procuracoesPartesEstadosAtivas);
            }
            catch (Exception ex)
            {
                return InternalServerError("Ocorreu um erro ao buscar o status da solicitação.", ex);
            }
        }


        [HttpGet("BuscarSolicitacao/{id:long}")]
        public async Task<IActionResult> BuscarSolicitacao(long id)
        {
            try
            {
                var dadosSolicitacao = await _solicitacoesAppService.BuscarSolicitacao(id);
                if (dadosSolicitacao == null)
                    return NotFound();

                return Ok(dadosSolicitacao);
            }
            catch (Exception ex)
            {
                string erro = ex.Message;

                if (ex.InnerException != null)
                    erro += $"\n\n\nInner Exception: {ex.InnerException.Message}";

                return StatusCode(500, erro);
            }
        }

        [HttpGet("BuscarDadosProduto/{id:long}")]
        public async Task<IActionResult> BuscarDadosProduto(long id)
        {
            try
            {
                var dadosSolicitacao = await _solicitacoesAppService.BuscarSolicitacao(id);
                if (dadosSolicitacao == null)
                    return NotFound();

                return Ok(dadosSolicitacao);
            }
            catch (Exception ex)
            {
                string erro = ex.Message;

                if (ex.InnerException != null)
                    erro += $"\n\n\nInner Exception: {ex.InnerException.Message}";

                return StatusCode(500, erro);
            }
        }


        [HttpGet("BuscarSolicitacoesEstados/{idSolicitacao:long}")]
        public async Task<IActionResult> BuscarSolicitacoesEstados(long idSolicitacao)
        {
            try
            {
                if (idSolicitacao < 1)
                    return BadRequest("Solicitação inválida!");

                var dadosSolicitacao = await _solicitacoesEstadosAppService.BuscarPorSolicitacao(idSolicitacao);
                if (dadosSolicitacao == null)
                    return NotFound("Não retornou nenhum estado!");

                return Ok(dadosSolicitacao);
            }
            catch (Exception ex)
            {
                string erro = ex.Message;

                if (ex.InnerException != null)
                    erro += $"\n\n\nInner Exception: {ex.InnerException.Message}";

                return StatusCode(500, erro);
            }
        }

        [HttpGet("BuscarSolicitacoesProntasParaEnvioParaCartorio")]
        public async Task<IActionResult> BuscarSolicitacoesProntasParaEnvioParaCartorio()
        {
            try
            {
                _solicitacoesAppService.BuscarSolicitacoesProntasParaEnvioParaCartorio(null);
                
                return Ok();
            }
            catch (Exception ex)
            {
                string erro = ex.Message;

                if (ex.InnerException != null)
                    erro += $"\n\n\nInner Exception: {ex.InnerException.Message}";

                return StatusCode(500, erro);
            }
        }

        [HttpGet("ConsultarStatusSolicitacao/{idSolicitacao:long}")]
        public async Task<IActionResult> ConsultarStatusSolicitacao(long idSolicitacao)
        {
            try
            {
                string estadoAtual = await _solicitacoesAppService.ConsultarStatusSolicitacao(idSolicitacao);

                return Ok(new { estadoAtual });
            }
            catch (Exception ex)
            {
                return InternalServerError("Ocorreu um erro ao buscar o status da solicitação.", ex);
            }
        }

        [HttpGet("BuscarDadosProcuracaoSolicitante/{idSolicitacao:long}")]
        public async Task<IActionResult> BuscarDadosProcuracaoSolicitante(long idSolicitacao)
        {
            try
            {
                var dadosOutorgante = await _solicitacoesAppService.BuscarDadosProcuracaoSolicitante(idSolicitacao);
                return Ok(dadosOutorgante);
            }
            catch (Exception ex)
            {
                return InternalServerError("Ocorreu um erro ao buscar o Outorgante da Solicitação.", ex);
            }
        }
    }
}
