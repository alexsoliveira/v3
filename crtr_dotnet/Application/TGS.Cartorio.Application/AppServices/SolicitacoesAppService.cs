using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Application.DTO;
using TGS.Cartorio.Application.DTO.Products.Matrimonio;
using TGS.Cartorio.Application.Enumerables;
using TGS.Cartorio.Application.ViewModel;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Entities.Procuracoes;
using TGS.Cartorio.Domain.Interfaces.Services;
using TGS.Cartorio.Infrastructure.Utility.ApiClient;
using TGS.Cartorio.Domain.Enumerables;
using iTextSharp.text;
using TGS.Cartorio.Application.Relatorios;
using TGS.Cartorio.Application.DTO.Relatorios;
using TGS.Cartorio.Application.Relatorios.Interfaces;
using Hangfire.Server;
using Hangfire.Console;
using TGS.Cartorio.Application.Extensions;

namespace TGS.Cartorio.Application.AppServices
{
    public class SolicitacoesAppService : ISolicitacoesAppService
    {
        private readonly ISolicitacoesService _solicitacoesService;
        private readonly ISolicitacoesEstadosService _solicitacoesEstadosService;
        private readonly ITaxasExtrasService _taxasExtrasService;
        private readonly ICartoriosService _cartoriosService;
        private readonly ISolicitacoesTaxasService _solicitacoesTaxasService;
        private readonly IProcuracoesPartesService _procuracoesPartesService;
        private readonly IMatrimoniosService _matrimonioService;
        private readonly IMatrimoniosDocumentosService _matrimoniosDocumentosService;
        private readonly ApiPagamento _apiPagamento;
        private readonly IMapper _mapper;
        private readonly ISmsAppService _smsAppService;
        private readonly IEmailAppService _emailAppService;
        private readonly IUsuariosService _usuarioService;
        private readonly IPessoasAppService _pessoaAppService;
        private readonly ILogSistemaAppService _logSistemaAppService;
        private readonly IPdfSolicitacaoReport _pdfSolicitacaoReport;
        private readonly IServiceProvider _serviceProvider;

        public SolicitacoesAppService(
            ISolicitacoesService solicitacoesService,
            ISolicitacoesEstadosService solicitacoesEstadosService,
            ApiPagamento apiPagamento,
            IMapper mapper,
            ISmsAppService smsAppService,
            IEmailAppService emailAppService,
            IUsuariosService usuarioService,
            IPessoasAppService pessoaAppService,
            IProcuracoesPartesService procuracoesPartesService,
            IMatrimoniosService matrimonioService,
            ISolicitacoesTaxasService solicitacoesTaxasService,
            ICartoriosService cartoriosService,
            ITaxasExtrasService taxasExtrasService,
            IMatrimoniosDocumentosService matrimoniosDocumentosService,
            ILogSistemaAppService logSistemaAppService, IPdfSolicitacaoReport pdfSolicitacaoReport, IServiceProvider serviceProvider)
        {
            _solicitacoesService = solicitacoesService;
            _solicitacoesEstadosService = solicitacoesEstadosService;
            _apiPagamento = apiPagamento;
            _mapper = mapper;
            _smsAppService = smsAppService;
            _emailAppService = emailAppService;
            _usuarioService = usuarioService;
            _pessoaAppService = pessoaAppService;
            _procuracoesPartesService = procuracoesPartesService;
            _matrimonioService = matrimonioService;
            _solicitacoesTaxasService = solicitacoesTaxasService;
            _cartoriosService = cartoriosService;
            _taxasExtrasService = taxasExtrasService;
            _matrimoniosDocumentosService = matrimoniosDocumentosService;
            _logSistemaAppService = logSistemaAppService;
            _pdfSolicitacaoReport = pdfSolicitacaoReport;
            _serviceProvider = serviceProvider;
        }

        public async Task<long> Incluir(SolicitacoesSimplificadoDto solicitacaoDto)
        {
            Solicitacoes solicitacao = null;
            try
            {
                solicitacao = _mapper.Map<Solicitacoes>(solicitacaoDto);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_SolicitacoesAppService_Incluir_Mapeamento_SolicitacoesSimplificadoDto_Solicitacoes,
                new
                {
                    SolicitacoesSimplificadoDto = solicitacaoDto
                }, ex);

                throw;
            }

            try
            {
                var cartorio = _cartoriosService.BuscarUltimoCartorioValido();
                if (cartorio == null)
                    throw new Exception($"Não foi possível localizar um cartório válido para a NOVA solicitação.");

                solicitacao.IdCartorio = cartorio.IdCartorio;
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_SolicitacoesAppService_Incluir_BuscarCartorioValido,
                new
                {
                    SolicitacoesSimplificadoDto = solicitacaoDto
                }, ex);

                throw;
            }

            try
            {
                solicitacao.IdSolicitacaoEstado = (int)EstadosSolicitacao.Cadastrada;
                await _solicitacoesService.Incluir(solicitacao);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_SolicitacoesAppService_Incluir_Solicitacao,
                new
                {
                    SolicitacoesSimplificadoDto = solicitacaoDto,
                    Solicitacoes = solicitacao
                }, ex);

                throw;
            }


            try
            {
                await IncluirTaxaEmolumento(solicitacao.IdSolicitacao);
            }
            catch (Exception)
            {
                //LOGS ESTÃO SENDO GRAVADOS DENTRO DO PRÓPRIO MÉTODO IncluirTaxaEmolumento
                throw;
            }

            Usuarios usuario = null;
            try
            {
                usuario = await _usuarioService.BuscarId(solicitacao.IdUsuario);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_SolicitacoesAppService_Incluir_BuscarUsuario,
                new
                {
                    SolicitacoesSimplificadoDto = solicitacaoDto,
                    Solicitacoes = solicitacao,
                    IdUsuario = solicitacao.IdUsuario
                }, ex);

                throw;
            }

            Pessoas pessoa = null;
            try
            {
                pessoa = await _pessoaAppService.BuscarId(usuario.IdPessoa.Value);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_SolicitacoesAppService_Incluir_BuscarPessoa,
                new
                {
                    SolicitacoesSimplificadoDto = solicitacaoDto,
                    Solicitacoes = solicitacao,
                    IdUsuario = solicitacao.IdUsuario
                }, ex);

                throw;
            }

            // Enviar SMS
            var templateSms = await _smsAppService.GetTemplateCriarSolicitacao(usuario.NomeUsuario, solicitacao.IdSolicitacao.ToString());
            await _smsAppService.Send(pessoa.IdPessoa, templateSms);


            // Enviar Email
            var templateEmail = await _emailAppService.GetTemplateCriarSolicitacao(usuario.NomeUsuario, solicitacao.IdSolicitacao.ToString());
            await _emailAppService.Send(pessoa.IdPessoa, templateEmail, assunto: "Nova Solicitação");

            return solicitacao.IdSolicitacao;

        }

        private async Task IncluirTaxaEmolumento(long idSolicitacao)
        {
            Cartorios cartorioSelecionado = null;
            string conteudoCartorio = null;
            bool erroBuscarUltimoCartorioValido = false;
            try
            {
                cartorioSelecionado = _cartoriosService.BuscarUltimoCartorioValido();
                if (cartorioSelecionado == null
                    || cartorioSelecionado.CartoriosEnderecos == null
                    || cartorioSelecionado.CartoriosEnderecos.Count == 0)
                {
                    Exception exCartorioSelecionadoSemEndereco = new Exception(cartorioSelecionado == null?
                        "Nenhum cartório foi localizado" : "Cartório selecionado não possui dados de endereço!");

                    await _logSistemaAppService.Add(CodLogSistema.Erro_SolicitacoesAppService_IncluirTaxaEmolumento_CartorioSelecionadoNaoPossuiDadosDeEndereco,
                            new
                            {
                                IdSolicitacao = idSolicitacao,
                                CartorioSelecionado = cartorioSelecionado
                            }, exCartorioSelecionadoSemEndereco);

                    erroBuscarUltimoCartorioValido = true;
                    throw exCartorioSelecionadoSemEndereco;
                }
                    
                conteudoCartorio = cartorioSelecionado.CartoriosEnderecos
                    .FirstOrDefault(ce => ce.IdEnderecoNavigation != null && !string.IsNullOrEmpty(ce.IdEnderecoNavigation.Conteudo))
                    ?.IdEnderecoNavigation?.Conteudo;

                if (string.IsNullOrEmpty(conteudoCartorio))
                {
                    Exception exCartorioSelecionadoSemEndereco = new Exception("Cartório selecionado não possui dados de endereço!");
                    await _logSistemaAppService.Add(CodLogSistema.Erro_SolicitacoesAppService_IncluirTaxaEmolumento_CartorioSelecionadoNaoPossuiDadosDeEndereco,
                            new
                            {
                                IdSolicitacao = idSolicitacao,
                                CartorioSelecionado = cartorioSelecionado
                            }, exCartorioSelecionadoSemEndereco);

                    erroBuscarUltimoCartorioValido = true;
                    throw exCartorioSelecionadoSemEndereco;
                }
            }
            catch (Exception ex)
            {
                if (!erroBuscarUltimoCartorioValido)
                    await _logSistemaAppService.Add(CodLogSistema.Erro_SolicitacoesAppService_IncluirTaxaEmolumento_AoBuscarUltimoCartorioValido,
                            new
                            {
                                IdSolicitacao = idSolicitacao,
                                CartorioSelecionado = cartorioSelecionado
                            }, ex);

                throw;
            }


            TaxasExtras taxaExtraEmolumento = null;
            bool erroRegistradoTaxaExtraEmolumento = false;
            try
            {
                var endereco = JsonConvert.DeserializeObject<EnderecoConteudoDto>(conteudoCartorio);
                taxaExtraEmolumento = await _taxasExtrasService.BuscarTaxaEmolumentoPorEstado(endereco.Uf);
                if (taxaExtraEmolumento == null)
                {
                    Exception exSemTaxasExtrasUF = new Exception($"Não foi localizado TaxasExtras para a UF {endereco.Uf}!");
                    await _logSistemaAppService.Add(CodLogSistema.Erro_SolicitacoesAppService_IncluirTaxaEmolumento_TaxasExtras,
                    new
                    {
                        IdSolicitacao = idSolicitacao,
                        CartorioSelecionado = cartorioSelecionado,
                        Uf = endereco.Uf
                    }, exSemTaxasExtrasUF);
                    erroRegistradoTaxaExtraEmolumento = true;
                    throw exSemTaxasExtrasUF;
                }
            }
            catch (Exception ex)
            {
                if (!erroRegistradoTaxaExtraEmolumento)
                    await _logSistemaAppService.Add(CodLogSistema.Erro_SolicitacoesAppService_IncluirTaxaEmolumento_BuscarTaxaEmolumentoPorEstado,
                    new
                    {
                        IdSolicitacao = idSolicitacao,
                        CartorioSelecionado = cartorioSelecionado
                    }, ex);

                throw;
            }

            try
            {
                await _solicitacoesTaxasService.Incluir(new SolicitacoesTaxas
                {
                    IdSolicitacao = idSolicitacao,
                    IdTaxaExtra = taxaExtraEmolumento.IdTaxaExtra
                });
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_SolicitacoesAppService_IncluirTaxaEmolumento_IncluirSolicitacoesTaxas,
                new
                {
                    IdSolicitacao = idSolicitacao,
                    CartorioSelecionado = cartorioSelecionado,
                    TaxaExtraEmolumento = taxaExtraEmolumento
                }, ex);

                throw;
            }
        }

        public async Task Atualizar(SolicitacoesDto solicitacao)
        {
            try
            {
                Solicitacoes _solicitacao = new Solicitacoes
                {
                    IdSolicitacao = solicitacao.IdSolicitacao.Value,
                    IdProduto = solicitacao.IdProduto,
                    IdSolicitacaoEstado = solicitacao.IdSolicitacaoEstado,
                    IdUsuario = solicitacao.IdUsuario,
                    IdCartorio = solicitacao.IdCartorio,
                    CamposPagamento = solicitacao.CamposPagamento,
                    ValorFrete = solicitacao.ValorFrete,
                    IdTipoFrete = solicitacao.IdTipoFrete,
                    Conteudo = solicitacao.Conteudo,
                    IdPessoa = solicitacao.IdPessoa
                };

                await _solicitacoesService.Atualizar(_solicitacao);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_SolicitacoesAppService_Atualizar,
                new
                {
                    Solicitacoes = solicitacao
                }, ex);

                throw;
            }
        }

        public async Task<Solicitacoes> BuscarId(long id)
        {
            try
            {
                return await _solicitacoesService.BuscarId(id);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_SolicitacoesAppService_BuscarId,
                new
                {
                    Id = id
                }, ex);

                throw;
            }
        }

        public async Task<List<Solicitacoes>> BuscarTodosComNoLock(Expression<Func<Solicitacoes, bool>> func, int pagina)
        {
            try
            {
                return await _solicitacoesService.BuscarTodosComNoLock(func, pagina);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_SolicitacoesAppService_BuscarTodosComNoLock,
                new
                {
                    func = func.ToString()
                }, ex);

                throw;
            }
        }

        public async Task<StatusSolicitacaoHeaderDto> BuscarDadosStatusSolicitacao(long idsolicitacao)
        {
            try
            {
                var statusSolicitacaoHeader = await _solicitacoesService.BuscarDadosStatusSolicitacao(idsolicitacao);
                return _mapper.Map<StatusSolicitacaoHeaderDto>(statusSolicitacaoHeader);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_SolicitacoesAppService_BuscarDadosStatusSolicitacao,
                new { IdSolicitacao = idsolicitacao }, ex);
                throw;
            }
        }

        public async Task AtualizarSolicitacaoParaCarrinho(long idSolicitacao)
        {
            Solicitacoes solicitacao = null;
            try
            {
                solicitacao = await _solicitacoesService.BuscarId(idSolicitacao);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_SolicitacoesAppService_AtualizarSolicitacaoParaCarrinho_BuscarSolicitacao,
                new { IdSolicitacao = idSolicitacao }, ex);
                throw;
            }

            int idEstadoAtual = solicitacao.IdSolicitacaoEstado;
            try
            {
                solicitacao.IdProdutoNavigation = null;
                solicitacao.IdSolicitacaoEstadoNavigation = null;
                solicitacao.IdSolicitacaoEstado = GerenciadorEstadosSolicitacao.ProximoEstadoSolicitacao(
                    solicitacao.IdSolicitacaoEstado, TelasSolicitacao.AssinaturaDigitalSolicitante);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_SolicitacoesAppService_AtualizarSolicitacaoParaCarrinho_AtualizarPropriedadeEstadoDominio,
                new { IdSolicitacao = idSolicitacao }, ex);
                throw;
            }

            try
            {
                if (solicitacao.IdSolicitacaoEstado != idEstadoAtual)
                    await _solicitacoesService.Atualizar(solicitacao);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_SolicitacoesAppService_AtualizarSolicitacaoParaCarrinho_AtualizarSolicitacao,
                new { IdSolicitacao = idSolicitacao }, ex);
                throw;
            }
        }

        public async Task AtualizarSolicitacaoParaAguardandoPagamento(long idSolicitacao)
        {

            Solicitacoes solicitacao = null;
            try
            {
                solicitacao = await _solicitacoesService.BuscarId(idSolicitacao);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_SolicitacoesAppService_AtualizarSolicitacaoParaAguardandoPagamento_BuscarSolicitacao,
                new { IdSolicitacao = idSolicitacao }, ex);
                throw;
            }

            int idEstadoAtual = solicitacao.IdSolicitacaoEstado;
            try
            {
                solicitacao.IdSolicitacaoEstadoNavigation = null;
                solicitacao.IdProdutoNavigation = null;
                solicitacao.IdSolicitacaoEstado = GerenciadorEstadosSolicitacao.ProximoEstadoSolicitacao(
                    solicitacao.IdSolicitacaoEstado, TelasSolicitacao.AssinaturaDigitalSolicitante);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_SolicitacoesAppService_AtualizarSolicitacaoParaAguardandoPagamento_AtualizarPropriedadeEstadoDominio,
                new { IdSolicitacao = idSolicitacao }, ex);
                throw;
            }

            try
            {
                if (solicitacao.IdSolicitacaoEstado != idEstadoAtual)
                    await _solicitacoesService.Atualizar(solicitacao);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_SolicitacoesAppService_AtualizarSolicitacaoParaAguardandoPagamento_AtualizarSolicitacao,
                new { IdSolicitacao = idSolicitacao }, ex);
                throw;
            }
        }

        public void AtualizarSolicitacaoParaProntaParaEnvioCartorio(SolicitacoesDto solicitacaoDto)
        {
            Solicitacoes solicitacao = null;
            try
            {
                if (solicitacaoDto == null)
                    throw new Exception("Solicitação nula!");

                solicitacao = _mapper.Map<Solicitacoes>(solicitacaoDto);

                if (solicitacao == null)
                    throw new Exception("Solicitação nula!");

                solicitacao.IdSolicitacaoEstadoNavigation = null;
                solicitacao.IdProdutoNavigation = null;
                var novoIdEstado = GerenciadorEstadosSolicitacao.ProximoEstadoSolicitacao(
                    solicitacao.IdSolicitacaoEstado, TelasSolicitacao.Job);

                if (solicitacao.IdSolicitacaoEstado != novoIdEstado)
                {
                    solicitacao.IdSolicitacaoEstado = novoIdEstado;
                    solicitacao.SolicitacoesEstados.Add(new SolicitacoesEstados
                    {
                        DataOperacao = DateTime.Now,
                        IdEstado = novoIdEstado,
                        IdSolicitacao = solicitacao.IdSolicitacao
                    });
                }
            }
            catch (Exception ex)
            {
                _logSistemaAppService.AddByJob(CodLogSistema.Erro_SolicitacoesAppService_AtualizarSolicitacaoParaProntaParaEnvioCartorio_AtualizarPropriedadeEstadoDominio,
                new { SolicitacaoDto = solicitacaoDto }, ex);
                throw;
            }

            try
            {
                CamposPagamentoViewModel camposPagamento = null;

                if (!string.IsNullOrEmpty(solicitacao.CamposPagamento))
                    camposPagamento = JsonConvert.DeserializeObject<CamposPagamentoViewModel>(solicitacao.CamposPagamento);

                if (camposPagamento == null)
                    throw new Exception("Não foi possível Desserializar os dados de pagamento da Solicitação!");

                camposPagamento.Status = StatusBoleto.P.ToString();
                camposPagamento.DataConfirmacaoPagamentoBoleto = DateTime.Now.ToString("yyyy-MM-dd");

                solicitacao.CamposPagamento = JsonConvert.SerializeObject(camposPagamento);

                SolicitacaoConteudoViewModel campoConteudoSolicitacao = null;
                if (!string.IsNullOrEmpty(solicitacao.Conteudo))
                    campoConteudoSolicitacao = JsonConvert.DeserializeObject<SolicitacaoConteudoViewModel>(solicitacao.Conteudo);

                if (campoConteudoSolicitacao == null)
                    throw new Exception("Não foi possível Desserializar os dados de pagamento da Solicitação!");

                campoConteudoSolicitacao.EstadoPagamento = EstadosPagamento.Aprovado.ToString();

                _solicitacoesService.AtualizarSolicitacaoPorJob(solicitacao);

                solicitacaoDto.Conteudo = solicitacao.Conteudo;
                solicitacaoDto.CamposPagamento = solicitacao.CamposPagamento;
            }
            catch (Exception ex)
            {
                _logSistemaAppService.AddByJob(CodLogSistema.Erro_SolicitacoesAppService_AtualizarSolicitacaoParaProntaParaEnvioCartorio_AtualizarSolicitacao,
                new { SolicitacaoDto = solicitacaoDto }, ex);
                throw;
            }
        }

        public void FinalizarJob()
        {
            try
            {
                _solicitacoesService.FinalizarJob();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task AtualizarSolicitacaoParaAguardandoAssinaturaDigitalSolicitante(long idSolicitacao)
        {
            Solicitacoes solicitacao = null;
            try
            {
                solicitacao = await _solicitacoesService.BuscarId(idSolicitacao);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_SolicitacoesAppService_AtualizarSolicitacaoParaAguardandoAssinaturaDigitalSolicitante_BuscarSolicitacao,
                new { IdSolicitacao = idSolicitacao }, ex);
                throw;
            }

            int idEstadoAtual = solicitacao.IdSolicitacaoEstado;
            try
            {
                solicitacao.IdSolicitacaoEstadoNavigation = null;
                solicitacao.IdProdutoNavigation = null;
                solicitacao.IdSolicitacaoEstado = GerenciadorEstadosSolicitacao.ProximoEstadoSolicitacao(
                    solicitacao.IdSolicitacaoEstado, TelasSolicitacao.NovaSolicitacao);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_SolicitacoesAppService_AtualizarSolicitacaoParaAguardandoAssinaturaDigitalSolicitante_AtualizarPropriedadeEstadoDominio,
                new { IdSolicitacao = idSolicitacao }, ex);
                throw;
            }

            try
            {
                if (solicitacao.IdSolicitacaoEstado != idEstadoAtual)
                    await _solicitacoesService.Atualizar(solicitacao);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_SolicitacoesAppService_AtualizarSolicitacaoParaAguardandoAssinaturaDigitalSolicitante_AtualizarSolicitacao,
                new { IdSolicitacao = idSolicitacao }, ex);
                throw;
            }
        }

        public async Task<SolicitacaoExistenteDto> BuscarSolicitacao(long idSolicitacao)
        {
            bool logRegistrado = false;
            try
            {
                Solicitacoes solicitacao = null;
                try
                {
                    solicitacao = await _solicitacoesService.BuscarId(idSolicitacao);
                    if (solicitacao == null)
                        return null;
                }
                catch (Exception ex)
                {
                    await _logSistemaAppService.Add(CodLogSistema.Erro_SolicitacoesAppService_BuscarSolicitacao_BuscarPorId,
                    new { IdSolicitacao = idSolicitacao }, ex);
                    logRegistrado = true;
                    throw;
                }


                //somente nestes estados que o cliente pode alterar a solicitação
                //nos demais estados da solicitação, o cliente não pode mais alterar
                if (!((EstadosSolicitacao)solicitacao.IdSolicitacaoEstado != EstadosSolicitacao.AguardandoAssinaturaDigitalSolicitante))
                {
                    Exception exSolicitacaoNaoDisponivelParaAlteracoes = new Exception("A solicitação não está mais disponível para alterações!");

                    await _logSistemaAppService.Add(CodLogSistema.Erro_SolicitacoesAppService_BuscarSolicitacao_SolicitacaoNaoEstaMaisDisponivelParaAlteracoes,
                    new
                    {
                        Solicitacao = solicitacao,
                        Msg = "Solicitação pode ser alterada até o estado AguardandoAssinaturaDigitalSolicitante, " +
                        "após este estado não é mais permitido!"
                    },
                    exSolicitacaoNaoDisponivelParaAlteracoes);

                    logRegistrado = true;
                    throw exSolicitacaoNaoDisponivelParaAlteracoes;
                }


                var dto = SolicitacaoExistenteDto.Create(idSolicitacao, solicitacao.IdProduto);
                if (solicitacao.IdPessoa.HasValue)
                    dto.IdPessoaSolicitante = solicitacao.IdPessoa.Value;

                dto.NomeProduto = solicitacao.IdProdutoNavigation.Titulo;

                var procuracoesPartes = await _procuracoesPartesService.BuscarPorIdSolicitacao(idSolicitacao);
                dto.CriarPartes(_mapper, procuracoesPartes);

                try
                {
                    dto.JsonProduto = (DadosMatrimonioDto)await GetProduto(idSolicitacao, solicitacao.IdProduto);
                }
                catch (Exception ex)
                {
                    await _logSistemaAppService.Add(CodLogSistema.Erro_SolicitacoesAppService_BuscarSolicitacao_GetProdutoMatrimonio,
                    new
                    {
                        IdSolicitacao = idSolicitacao,
                        IdProduto = solicitacao.IdProduto
                    },
                    ex);

                    logRegistrado = true;
                    throw;
                }

                try
                {
                    var docs = await _matrimoniosDocumentosService.BuscarPorSolicitacao(idSolicitacao);
                    if (docs != null
                        && docs.Count > 0
                        && docs.Any(x => x.IdTipoDocumento == (int)TiposDocumentosMatrimonio.Proclamas))
                        dto.JsonProduto.DadosContracaoMatrimonio.DocumentoProclamas = $"data:application/pdf;base64," +
                            $"{Convert.ToBase64String(docs.First(x => x.IdTipoDocumento == (int)TiposDocumentosMatrimonio.Proclamas).BlobConteudo)}";
                }
                catch (Exception ex)
                {
                    await _logSistemaAppService.Add(CodLogSistema.Erro_SolicitacoesAppService_BuscarSolicitacao_BuscarMatrimonioDocumentoProclamas,
                    new { IdSolicitacao = idSolicitacao }, ex);
                    logRegistrado = true;
                    throw;
                }

                if (!string.IsNullOrEmpty(solicitacao.Conteudo))
                {
                    try
                    {
                        var conteudo = JsonConvert.DeserializeObject<SolicitacaoConteudoViewModel>(solicitacao.Conteudo);
                        dto.InformacoesImportantes = conteudo != null ? conteudo.InformacoesImportantes : null;
                        dto.RepresentacaoPartes = conteudo != null ? conteudo.RepresentacaoPartes : null;
                    }
                    catch (Exception ex)
                    {
                        await _logSistemaAppService.Add(CodLogSistema.Erro_SolicitacoesAppService_BuscarSolicitacao_SolicitacaoConteudo,
                        new { IdSolicitacao = solicitacao.IdSolicitacao, SolicitacaoConteudo = solicitacao.Conteudo }, ex);
                        logRegistrado = true;
                        throw;
                    }

                }

                return dto;
            }
            catch (Exception ex)
            {
                if (!logRegistrado)
                    await _logSistemaAppService.Add(CodLogSistema.Erro_SolicitacoesAppService_BuscarSolicitacao_Erro,
                        new { IdSolicitacao = idSolicitacao }, ex);
                throw;
            }
        }

        public async Task<List<MinhasSolicitacoes>> MinhasSolicitacoes(long id)
        {
            try
            {
                return await _solicitacoesService.MinhasSolicitacoes(id);
            }
            catch (Exception)
            {
                //logs dentro do metodo acima!!
                throw;
            }
        }

        public async Task<MinhaSolicitacao> ConsultarBoleto(long id)
        {
            try
            {
                return await _solicitacoesService.ConsultarBoleto(id);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_SolicitacoesAppService_ConsultarBoleto,
                        new { IdSolicitacao = id }, ex);
                throw;
            }
        }

        public async Task<string> GerarBoleto(Solicitacoes solicitacao, BoletoDto boleto, string token)
        {
            IdentifierBoletoDto identifierBoleto = new IdentifierBoletoDto();
            if (boleto != null)
            {
                Retorno<string> retornoStrIdentifierBoleto = null;
                bool logouErroGerarBoleto = false;
                try
                {
                    boleto.Pagador.NumeroDocumento = boleto.Pagador.NumeroDocumento.PadLeft(11, '0');
                    retornoStrIdentifierBoleto = await _apiPagamento.GerarBoleto("api/v1/boleto/novoboleto", boleto, token);

                    await _logSistemaAppService.Add(retornoStrIdentifierBoleto.Sucesso ?
                        CodLogSistema.ConsumoAPI_SolicitacoesAppService_GerarBoleto_Response_Com_Sucesso :
                        CodLogSistema.ConsumoAPI_SolicitacoesAppService_GerarBoleto_Response_Sem_Sucesso,
                    new
                    {
                        Solicitacao = solicitacao,
                        BoletoDto = boleto,
                        Token = token,
                        Log = retornoStrIdentifierBoleto.Log
                    });

                    logouErroGerarBoleto = true;

                    if (!retornoStrIdentifierBoleto.Sucesso)
                        throw new Exception("Não foi possível gerar o boleto!");
                }
                catch (Exception ex)
                {
                    if (!logouErroGerarBoleto)
                        await _logSistemaAppService.Add(CodLogSistema.Erro_ConsumoAPI_SolicitacoesAppService_GerarBoleto,
                            new
                            {
                                Solicitacao = solicitacao,
                                BoletoDto = boleto,
                                Token = token
                            }, ex);

                    throw;
                }

                if (!identifierBoleto.TryConvertJsonObj(retornoStrIdentifierBoleto.ObjRetorno))
                {
                    try
                    {
                        Exception exIdentifierBoleto = new Exception("Ocorreu um erro com os dados recebidos do boleto!");
                    }
                    catch (Exception ex)
                    {
                        await _logSistemaAppService.Add(CodLogSistema.Erro_ConsumoAPI_SolicitacoesAppService_GerarBoleto_TryConvertJsonObj,
                                new
                                {
                                    Solicitacao = solicitacao,
                                    BoletoDto = boleto,
                                    Token = token,
                                    ObjRetorno = retornoStrIdentifierBoleto.ObjRetorno,
                                    RetornoServico = retornoStrIdentifierBoleto,
                                    IdentifierBoleto = identifierBoleto
                                }, ex);

                        throw;
                    }
                }

                try
                {
                    CamposPagamentoViewModel camposPagamento = new CamposPagamentoViewModel();
                    solicitacao.CamposPagamento = camposPagamento.PreencherBoletoRetornoSerializado(identifierBoleto.Identifier, boleto.Valor);

                    SolicitacaoConteudoViewModel solicitacaoConteudo = new SolicitacaoConteudoViewModel();
                    solicitacao.Conteudo = solicitacaoConteudo.PreencherComRetornoSerializado(TiposPagamento.Boleto,
                                                                                                EstadosPagamento.AguardandoPagamento);
                }
                catch (Exception ex)
                {
                    await _logSistemaAppService.Add(CodLogSistema.Erro_ConsumoAPI_SolicitacoesAppService_GerarBoleto_PreencherCamposPagamentoDaSolicitacao,
                    new
                    {
                        Solicitacao = solicitacao,
                        BoletoDto = boleto,
                        Token = token,
                        ObjRetorno = retornoStrIdentifierBoleto.ObjRetorno,
                        RetornoServico = retornoStrIdentifierBoleto,
                        IdentifierBoleto = identifierBoleto
                    }, ex);

                    throw;
                }

                // ==>>> ATENÇÃO <<<== Só mudar estado da solicitação quando boleto estiver pago..

                try
                {
                    solicitacao.IdProdutoNavigation = null;
                    solicitacao.IdCartorioNavigation = null;
                    solicitacao.IdTipoFreteNavigation = null;
                    await _solicitacoesService.Atualizar(solicitacao);
                }
                catch (Exception ex)
                {
                    await _logSistemaAppService.Add(CodLogSistema.Erro_ConsumoAPI_SolicitacoesAppService_GerarBoleto_AtualizarSolicitacao,
                    new
                    {
                        Solicitacao = solicitacao,
                        BoletoDto = boleto,
                        Token = token,
                        ObjRetorno = retornoStrIdentifierBoleto.ObjRetorno,
                        RetornoServico = retornoStrIdentifierBoleto,
                        IdentifierBoleto = identifierBoleto
                    }, ex);

                    throw;
                }

            }

            return identifierBoleto?.Identifier;
        }

        public async Task<List<StatusSolicitacao>> BuscarEstadoDoPedidoPorParticipante(long idSolicitacao)
        {
            try
            {
                var minhasParticipacoes = await _solicitacoesService.BuscarEstadoDoPedidoPorParticipante(idSolicitacao);

                return minhasParticipacoes;
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_SolicitacoesAppService_BuscarEstadoDoPedidoPorParticipante,
                    new { IdSolicitacao = idSolicitacao }, ex);

                throw;
            }
        }

        public async Task<List<ProcuracoesPartesEstadosPc>> BuscarTodasProcuracoesPartesEstados(Expression<Func<ProcuracoesPartesEstadosPc, bool>> func, int pagina = 0)
        {
            try
            {
                return await _solicitacoesService.BuscarTodasProcuracoesPartesEstados(func);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_SolicitacoesAppService_BuscarEstadoDoPedidoPorParticipante,
                    new { Func = func.ToString() }, ex);

                throw;
            }
        }


        private async Task<object> GetProduto(long idSolicitacao, int idProduto)
        {
            try
            {
                if (Enum.TryParse(idProduto.ToString(), out TipoProduto tipoProduto))
                {
                    switch ((int)tipoProduto)
                    {
                        case (int)TipoProduto.ContrairMatrimonio:
                            var matrimonio = await _matrimonioService.BuscarPorSolicitacao(idSolicitacao);
                            if (matrimonio != null)
                                return JsonConvert.DeserializeObject<DadosMatrimonioDto>(matrimonio.CamposJson);
                            break;
                    }
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void BuscarSolicitacoesProntasParaEnvioParaCartorio(PerformContext consoleHangFire)
        {
            List<Solicitacoes> solicitacoes = null;
            try
            {
                //Buscar por solicitações no estado de SolicitacaoProntaParaEnvioCartorio
                solicitacoes = _solicitacoesService.BuscarPorSolicitacoesProntasParaEnvioCartorio();
                if (solicitacoes == null || solicitacoes.Count == 0)
                {
                    try
                    {
                        consoleHangFire.CreateConsoleMessage(null, $"Não foram localizadas solicitações para enviar para o cartório!");
                    }
                    catch { }
                    
                    return;
                }

                try
                {
                    string s = string.Empty;
                    solicitacoes.Select(x => x.IdSolicitacao).ToList().ForEach((solicitacao) =>
                    {
                        s += solicitacao.ToString() + ", ";
                    });
                    consoleHangFire.CreateConsoleMessage(null, $"Solicitações localizadas para envio para o cartório: {s}");
                }
                catch { }
            }
            catch (Exception ex)
            {
                string msg = "Ocorreu um erro ao tentar buscar as solicitacoes prontas para envio para o cartório!";
                CodLogSistema log = CodLogSistema.JOB_Erro_SolicitacoesAppService_BuscarSolicitacoesProntasParaEnvioParaCartorio_BuscarSolicitacoes;
                
                consoleHangFire.CreateExceptionMessage(ex, log, msg);
                
                _logSistemaAppService.AddByJob(log, msg, ex);
                
                throw;
            }

            Dictionary<string, string> dicCartorio = new Dictionary<string, string>();
            try
            {
                dicCartorio = BuscarUltimoCartorio();
            }
            catch (Exception ex)
            {
                string msg = "Ocorreu um erro ao buscar os dados do último cartório!";
                CodLogSistema log = CodLogSistema.JOB_Erro_SolicitacoesAppService_BuscarSolicitacoesProntasParaEnvioParaCartorio_BuscarDadosUltimoCartorio;

                consoleHangFire.CreateExceptionMessage(ex, log, msg);

                _logSistemaAppService.AddByJob(log, msg, ex);

                throw;
            }

            string razaoSocialCartorio, emailCartorio;
            try
            {
                bool boolRazao = dicCartorio.TryGetValue("razao", out razaoSocialCartorio);
                bool boolEmail = dicCartorio.TryGetValue("email", out emailCartorio);

                if (!boolRazao || !boolEmail)
                {
                    string msg = "Não foi possível localizar a Razão e/ou Email do cartório!";
                    
                    try
                    {
                        consoleHangFire.CreateConsoleMessage(null, msg);
                    }
                    catch { }

                    throw new Exception(msg);
                }

                try
                {
                    consoleHangFire.CreateConsoleMessage(null, $"Razão Social do Cartório = {razaoSocialCartorio} e E-mail do Cartório = {emailCartorio}");
                }
                catch { }

            }
            catch (Exception ex)
            {
                string msg = "Ocorreu um erro ao buscar os dados do último cartório!";
                CodLogSistema log = CodLogSistema.JOB_Erro_SolicitacoesAppService_BuscarSolicitacoesProntasParaEnvioParaCartorio_DadosUltimoCartorio_SemRazaoSocialOuEmail;

                consoleHangFire.CreateExceptionMessage(ex, log, msg);

                _logSistemaAppService.AddByJob(log, msg, ex);
                throw;
            }

            try
            {
                //Carregar todos os dados da solicitação com estado de SolicitacaoProntaParaEnvioCartorio
                var solicitacoesProntaParaEnvio = CarregarTodosOsDadosDaSolicitacaoParaEnvio(consoleHangFire, solicitacoes);

                List<ValidadorEnvioEmailSolicitacaoCartorioDto> validador = GerarPDF(consoleHangFire, solicitacoesProntaParaEnvio, razaoSocialCartorio, emailCartorio);

                // Enviar Email
                EnviarEmailParaCartorio(consoleHangFire, validador);

                var solicitacoesElegiveis = solicitacoes.Where(x => validador.Any(v => v.IdSolicitacao == x.IdSolicitacao && v.Sucesso)).ToList();

                //Após enviar a solicitação alterar estado para SolicitacaoEnviadaAoCartorio
                if (solicitacoesElegiveis.Count > 0)
                {
                    try
                    {
                        string s = "";
                        solicitacoesElegiveis.ForEach((solicitacao) => s += solicitacao.IdSolicitacao.ToString() + ", ");
                        consoleHangFire.CreateConsoleMessage(null, $"Solicitações elegíveis para envio para o cartório: {s}");
                    }
                    catch { }

                    MudarEstadoSolicitacaoEnviadaAoCartorio(consoleHangFire, solicitacoesElegiveis.ToList());
                }
                else
                {
                    try
                    {
                        var solicitacoesNaoValidas = validador.Where(v => !v.Sucesso).ToList();
                        string s = "";
                        solicitacoesNaoValidas.ForEach((solicitacao) => s += $"{solicitacao.IdSolicitacao}, ");
                        consoleHangFire.CreateConsoleMessage(null, $"Não houveram solicitações elegíveis para envio ao cartório! Solicitações: {s}");
                    }
                    catch { }
                }
                    
            }
            catch (Exception)
            {
                throw;
            }

            try
            {
                _solicitacoesService.FinalizarJob();
                
                try
                {
                    consoleHangFire.CreateConsoleMessage(null, $"Job Finalizado!!!");
                }
                catch { }
            }
            catch (Exception ex)
            {
                string msg = "Ocorreu um erro ao gravar dados no banco de dados!";
                CodLogSistema log = CodLogSistema.JOB_Erro_SolicitacoesAppService_BuscarSolicitacoesProntasParaEnvioParaCartorio_GravarTodosDados;

                consoleHangFire.CreateExceptionMessage(ex, log, msg);

                _logSistemaAppService.AddByJob(log, msg, ex);

                throw;
            }
        }

        public async Task<string> ConsultarStatusSolicitacao(long idSolicitacao)
        {
            try
            {
                var solicitacao = await _solicitacoesService.BuscarId(idSolicitacao);

                if (solicitacao == null
                    || solicitacao.IdSolicitacaoEstadoNavigation == null
                    || string.IsNullOrEmpty(solicitacao.IdSolicitacaoEstadoNavigation.Descricao))
                    throw new Exception("Ocorreu um erro ao buscar o status da solicitação!");

                string descricaoSolicitacaoEstado = solicitacao.IdSolicitacaoEstadoNavigation.Descricao;
                if (BoletoValidoParaPagamento(solicitacao))
                    descricaoSolicitacaoEstado = "Aguardando Efetuar Pagamento Boleto";

                return descricaoSolicitacaoEstado;
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_SolicitacoesAppService_ConsultarStatusSolicitacao, new
                {
                    IdSolicitacao = idSolicitacao
                }, ex);

                throw;
            }
        }

        private bool BoletoValidoParaPagamento(Solicitacoes solicitacao)
        {
            try
            {
                bool boletoValido = false;

                if (solicitacao.IdSolicitacaoEstado == (int)EstadosSolicitacao.AguardandoPagamento
                    && !string.IsNullOrEmpty(solicitacao.CamposPagamento))
                {
                    var camposPagamento = JsonConvert.DeserializeObject<CamposPagamentoViewModel>(solicitacao.CamposPagamento);
                    boletoValido = camposPagamento != null
                                && camposPagamento.TipoPagamento == TiposPagamento.Boleto.ToString()
                                && DateTime.TryParse(camposPagamento.DataVencimentoBoleto, out DateTime vencimentoBoleto)
                                && vencimentoBoleto.AddDays(7) > DateTime.Now;
                }

                return boletoValido;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ProcuracoesPartesSimplificadoDto> BuscarDadosProcuracaoSolicitante(long idSolicitacao)
        {
            try
            {
                var matrimonio = await _matrimonioService.BuscarPorSolicitacao(idSolicitacao);
                if (matrimonio == null
                    || matrimonio.IdSolicitacaoNavigation == null
                    || !matrimonio.IdSolicitacaoNavigation.IdPessoa.HasValue)
                    throw new Exception("Não foi possível buscar os dados do Outorgante da Solicitação.");

                return new ProcuracoesPartesSimplificadoDto
                {
                    IdPessoa = matrimonio.IdSolicitacaoNavigation.IdPessoa.Value,
                    IdMatrimonio = matrimonio.IdMatrimonio
                };
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_SolicitacoesAppService_BuscarDadosProcuracaoSolicitante, new
                {
                    IdSolicitacao = idSolicitacao
                }, ex);

                throw;
            }
        }

        private Dictionary<string, string> BuscarUltimoCartorio()
        {
            try
            {
                var ultimoCartorioValido = _cartoriosService.BuscarUltimoCartorioValido();

                if (ultimoCartorioValido != null
                    && ultimoCartorioValido.IdPessoaNavigation != null
                    && ultimoCartorioValido.IdPessoaNavigation.PessoasContatos != null
                    && ultimoCartorioValido.IdPessoaNavigation.PessoasContatos.Count > 0
                    && ultimoCartorioValido.IdPessoaNavigation.PessoasContatos.First().IdContatoNavigation != null
                    && !string.IsNullOrEmpty(ultimoCartorioValido.IdPessoaNavigation.PessoasContatos.First().IdContatoNavigation.Conteudo)
                    && ultimoCartorioValido.IdPessoaNavigation.PessoasJuridicas != null
                    && !string.IsNullOrEmpty(ultimoCartorioValido.IdPessoaNavigation.PessoasJuridicas.RazaoSocial))
                {
                    var conteudoContatoCartorio = JsonConvert.DeserializeObject<ContatosConteudo>(ultimoCartorioValido.IdPessoaNavigation.PessoasContatos.First().IdContatoNavigation.Conteudo);
                    if (conteudoContatoCartorio == null || string.IsNullOrEmpty(conteudoContatoCartorio.Email))
                        throw new Exception("Não foi possível localizar o e-mail em ContatosConteudo do último cartório existente no banco de dados!");

                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    dic.Add("email", conteudoContatoCartorio.Email);
                    dic.Add("razao", ultimoCartorioValido.IdPessoaNavigation.PessoasJuridicas.RazaoSocial);
                    return dic;
                }
                else
                    throw new Exception("Não foi possível localizar a razão social ou email (em ContatosConteudo) do último cartório existente no banco de dados!");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private List<SolicitacaoProntaParaEnvioDto> CarregarTodosOsDadosDaSolicitacaoParaEnvio(PerformContext consoleHangFire, List<Solicitacoes> solicitacoes)
        {
            CodLogSistema log = CodLogSistema.Desconhecido;

            var solicitacoesProntaParaEnvio = new List<SolicitacaoProntaParaEnvioDto>();

            foreach (var solicitacao in solicitacoes)
            {
                var solicitacaoProntaParaEnvio = new SolicitacaoProntaParaEnvioDto();
                solicitacaoProntaParaEnvio.solicitacoes = solicitacao;

                SolicitacaoConteudoViewModel conteudoSolicitacao = null;
                try
                {
                    conteudoSolicitacao = new SolicitacaoConteudoViewModel();
                    if (!string.IsNullOrEmpty(solicitacao.Conteudo))
                        conteudoSolicitacao = JsonConvert.DeserializeObject<SolicitacaoConteudoViewModel>(solicitacao.Conteudo);

                    solicitacaoProntaParaEnvio.InformacoesImportantes = conteudoSolicitacao.InformacoesImportantes;
                }
                catch (Exception ex)
                {
                    string msg = $"IdSolicitacao = {solicitacao.IdSolicitacao}";
                    log = CodLogSistema.JOB_Erro_SolicitacoesAppService_CarregarTodosOsDadosDaSolicitacaoParaEnvio_DesserializarSolicitacaoConteudo;

                    consoleHangFire.CreateExceptionMessage(ex, log, msg);

                    _logSistemaAppService.AddByJob(log,
                    new
                    {
                        Solicitacao = solicitacao
                    }, ex);
                }

                List<ProcuracoesPartesDto> procuracoesPartesDto = null;
                IEnumerable<ProcuracoesPartes> procuracoesPartes = null;
                try
                {
                    procuracoesPartes = _procuracoesPartesService.BuscarPorIdSolicitacaoByJob(solicitacao.IdSolicitacao);
                    procuracoesPartesDto = _mapper.Map<List<ProcuracoesPartesDto>>(procuracoesPartes);

                    if (procuracoesPartes == null
                        || procuracoesPartes.Count() == 0
                        || procuracoesPartesDto == null
                        || procuracoesPartesDto.Count == 0)
                        throw new Exception("Ocorreu um erro ao buscar as procurações partes da Solicitação!");
                }
                catch (Exception ex)
                {
                    string msg = $"IdSolicitacao = {solicitacao.IdSolicitacao}";
                    log = CodLogSistema.JOB_Erro_SolicitacoesAppService_CarregarTodosOsDadosDaSolicitacaoParaEnvio_BuscarProcuracoesPartesDaSolicitacao;

                    consoleHangFire.CreateExceptionMessage(ex, log, msg);

                    _logSistemaAppService.AddByJob(log,
                    new
                    {
                        Solicitacao = solicitacao
                    }, ex);

                    continue;
                }


                try
                {
                    foreach (var procuracaoParte in procuracoesPartes)
                    {
                        try
                        {
                            if (procuracoesPartesDto.Any(x => x.IdSolicitacao == procuracaoParte.IdSolicitacao
                                                           && x.IdPessoa == procuracaoParte.IdPessoa))
                                if (procuracaoParte.PessoasNavigation != null
                                    && procuracaoParte.PessoasNavigation.PessoasContatos != null
                                    && procuracaoParte.PessoasNavigation.PessoasContatos.Count > 0)
                                {
                                    if (procuracaoParte.PessoasNavigation.PessoasContatos.First().IdContatoNavigation != null)
                                        procuracoesPartesDto.First(x => x.IdSolicitacao == procuracaoParte.IdSolicitacao
                                                                && x.IdPessoa == procuracaoParte.IdPessoa).ConteudoPessoasContatos =
                                            procuracaoParte.PessoasNavigation.PessoasContatos.First().IdContatoNavigation.Conteudo;
                                }
                        }
                        catch (Exception ex)
                        {
                            string msg = $"IdSolicitacao = {solicitacao.IdSolicitacao} e IdProcuracaoParte = {procuracaoParte.IdProcuracaoParte}";
                            log = CodLogSistema.JOB_Erro_SolicitacoesAppService_CarregarTodosOsDadosDaSolicitacaoParaEnvio_AoPreencherDadosProcuracoesPartes;

                            consoleHangFire.CreateExceptionMessage(ex, log, msg);

                            _logSistemaAppService.AddByJob(log,
                            new
                            {
                                Solicitacao = solicitacao,
                                ProcuracaoParte = procuracaoParte
                            }, ex);

                            throw;
                        }
                    }
                }
                catch
                {
                    continue;
                }



                solicitacaoProntaParaEnvio.procuracoesPartes = procuracoesPartesDto;

                Matrimonios matrimonio = null;
                try
                {
                    matrimonio = _matrimonioService.BuscarPorSolicitacaoByJob(solicitacao.IdSolicitacao);
                    if (matrimonio == null)
                        throw new Exception("Não foi possível localizar o matrimônio da Solicitação!");
                }
                catch (Exception ex)
                {
                    string msg = $"IdSolicitacao = {solicitacao.IdSolicitacao}";
                    log = CodLogSistema.JOB_Erro_SolicitacoesAppService_CarregarTodosOsDadosDaSolicitacaoParaEnvio_BuscarDadosMatrimonio;

                    consoleHangFire.CreateExceptionMessage(ex, log, msg);

                    _logSistemaAppService.AddByJob(log,
                    new
                    {
                        Solicitacao = solicitacao
                    }, ex);

                    continue;
                }


                MatrimoniosDto matrimonioDto = null;
                try
                {
                    matrimonioDto = _mapper.Map<MatrimoniosDto>(matrimonio);
                    if (matrimonioDto == null)
                        throw new Exception("Ocorreu um erro ao desserializar os dados do Matrimônio!");
                }
                catch (Exception ex)
                {
                    string msg = $"IdSolicitacao = {solicitacao.IdSolicitacao}";
                    log = CodLogSistema.JOB_Erro_SolicitacoesAppService_CarregarTodosOsDadosDaSolicitacaoParaEnvio_DesserializarMatrimonioDto;

                    consoleHangFire.CreateExceptionMessage(ex, log, msg);

                    _logSistemaAppService.AddByJob(log,
                    new
                    {
                        Solicitacao = solicitacao
                    }, ex);

                    continue;
                }


                solicitacaoProntaParaEnvio.matrimonios = matrimonioDto;

                try
                {
                    var matrimonioDocumento = _matrimoniosDocumentosService.BuscarPorMatrimonioByJob(matrimonioDto.IdMatrimonio);
                    bool proclamas = matrimonioDocumento.Any(x => x.IdTipoDocumento == (int)EMatrimoniosTiposDocumentos.Proclamas);
                    bool docAssinado = matrimonioDocumento.Any(x => x.IdTipoDocumento == (int)EMatrimoniosTiposDocumentos.CPF
                                                                 || x.IdTipoDocumento == (int)EMatrimoniosTiposDocumentos.RNE);
                    if (!proclamas || !docAssinado)
                        throw new Exception("Não foi possível localizar o documento de proclamas e/ou RG/RNE");

                    solicitacaoProntaParaEnvio.matrimoniosDocumentos = matrimonioDocumento;
                }
                catch (Exception ex)
                {
                    string msg = $"IdSolicitacao = {solicitacao.IdSolicitacao} e IdMatrimonio = {matrimonioDto.IdMatrimonio}";
                    log = CodLogSistema.JOB_Erro_SolicitacoesAppService_CarregarTodosOsDadosDaSolicitacaoParaEnvio_BuscarDocumentosMatrimonios;

                    consoleHangFire.CreateExceptionMessage(ex, log, msg);

                    _logSistemaAppService.AddByJob(log,
                    new
                    {
                        Solicitacao = solicitacao,
                        IdMatrimonio = matrimonioDto.IdMatrimonio,
                        MatrimoniosDto = matrimonioDto
                    }, ex);

                    continue;
                }


                solicitacoesProntaParaEnvio.Add(solicitacaoProntaParaEnvio);
                string msgLog = $"IdSolicitacao = {solicitacao.IdSolicitacao} " +
                            $"| solicitação pronta para envio";
                log = CodLogSistema.SolicitacoesAppService_SolicitacaoProntaParaEnvio;

                try
                {
                    consoleHangFire.CreateConsoleMessage(log, msgLog);
                }
                catch { }

                _logSistemaAppService.AddByJob(log, msgLog);
            }

            return solicitacoesProntaParaEnvio;
        }

        private void EnviarEmailParaCartorio(PerformContext consoleHangFire, List<ValidadorEnvioEmailSolicitacaoCartorioDto> validador)
        {
            CodLogSistema log = CodLogSistema.Desconhecido;

            foreach (var solicitacao in validador)
            {
                string templateEmail = "";
                try
                {
                    templateEmail = _emailAppService.GetTemplateLayoutParaPDFEnvioCartorio(
                    solicitacao.NomeUsuarioCartorio,
                    solicitacao.IdSolicitacao.ToString(),
                    solicitacao.NomeSolicitante,
                    solicitacao.EmailSolicitante
                    );
                }
                catch (Exception ex)
                {
                    string msg = $"IdSolicitacao = {solicitacao.IdSolicitacao}";
                    log = CodLogSistema.Erro_SolicitacoesAppService_AoGerarTemplateLayoutParaPDFEnvioCartorio;

                    consoleHangFire.CreateExceptionMessage(ex, log, msg);

                    _logSistemaAppService.AddByJob(log,
                    new
                    {
                        Solicitacao = solicitacao
                    }, ex);

                    continue;
                }
                try
                {
                    // Enfileirar Email
                    var retorno = _emailAppService.Send(null,
                                  templateEmail,
                                  solicitacao.NomeUsuarioCartorio,
                                  solicitacao.EmailCartorio,
                                  solicitacao.IdSolicitacao,
                                  $"Solicitação {solicitacao.IdSolicitacao} pronta para atendimento",
                                  $"Solicitacao_{solicitacao.IdSolicitacao.ToString()}",
                                  solicitacao.ZipPdfCartorio).GetAwaiter().GetResult();

                    validador.First(x => x.IdSolicitacao == solicitacao.IdSolicitacao).Sucesso = true;
                }
                catch (Exception ex)
                {
                    string msg = $"IdSolicitacao = {solicitacao.IdSolicitacao}";
                    log = CodLogSistema.Erro_SolicitacoesAppService_AoEnviarEmailParaCartorio;

                    consoleHangFire.CreateExceptionMessage(ex, log, msg);

                    _logSistemaAppService.AddByJob(log,
                    new
                    {
                        Solicitacao = solicitacao
                    }, ex);


                    continue;
                }
            }
        }

        private List<ValidadorEnvioEmailSolicitacaoCartorioDto> GerarPDF(PerformContext consoleHangFire, List<SolicitacaoProntaParaEnvioDto> solicitacoes, string razaoSocialCartorio, string emailCartorio)
        {
            CodLogSistema log = CodLogSistema.Desconhecido;
            try
            {
                List<ValidadorEnvioEmailSolicitacaoCartorioDto> validador = new List<ValidadorEnvioEmailSolicitacaoCartorioDto>();
                foreach (var solicitacao in solicitacoes)
                {
                    try
                    {
                        _pdfSolicitacaoReport.SetDadosSolicitacao(solicitacao);
                        var solicitacaoValidador = _pdfSolicitacaoReport.GerarReport(razaoSocialCartorio, emailCartorio);
                        String teste = Convert.ToBase64String(solicitacaoValidador.ZipPdfCartorio);
                        validador.Add(solicitacaoValidador);
                    }
                    catch (Exception ex)
                    {
                        string msg = $"IdSolicitacao = {solicitacao.solicitacoes.IdSolicitacao}";
                        log = CodLogSistema.Erro_SolicitacoesAppService_GerarPdfCartorio;

                        consoleHangFire.CreateExceptionMessage(ex, log, msg);

                        _logSistemaAppService.AddByJob(log,
                        new
                        {
                            Solicitacao = solicitacao
                        }, ex);
                    }
                }

                return validador;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MudarEstadoSolicitacaoEnviadaAoCartorio(PerformContext consoleHangFire, List<Solicitacoes> solicitacoes)
        {
            CodLogSistema log = CodLogSistema.Desconhecido;

            foreach (var solicitacao in solicitacoes)
            {
                try
                {
                    _solicitacoesService.SolicitacaoEnviadaAoCartorio(solicitacao);
                }
                catch (Exception ex)
                {
                    string msg = $"IdSolicitacao = {solicitacao.IdSolicitacao}";
                    log = CodLogSistema.Erro_SolicitacoesAppService_AoTentarAlterarEstadoDaSolicitacaoParaSolicitacaoEnviadaAoCartorio;

                    consoleHangFire.CreateExceptionMessage(ex, log, msg);

                    _logSistemaAppService.AddByJob(log,
                    new
                    {
                        Solicitacao = solicitacao
                    }, ex);

                    continue;
                }

                string msgLog = $"IdSolicitacao = {solicitacao.IdSolicitacao} " +
                                $"| alterado estado da solicitação para 'SolicitacaoEnviadaAoCartorio' com sucesso";

                log = CodLogSistema.SolicitacoesAppService_AlteradoEstadoDaSolicitacaoParaSolicitacaoEnviadaAoCartorio;

                try
                {
                    string msg = msgLog;
                    msg += $" | IdSolicitacaoEstado = {solicitacao.IdSolicitacaoEstado}";
                    consoleHangFire.CreateConsoleMessage(log, msg);
                }
                catch { }

                _logSistemaAppService.AddByJob(log,
                    new { msgLog, IdSolicitacaoEstado = solicitacao.IdSolicitacaoEstado, IdSolicitacao = solicitacao.IdSolicitacao });
            }
        }
    }
}
