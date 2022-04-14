using Newtonsoft.Json;
using System;
using TGS.Cartorio.Application.DTO.Pagamento;
using TGS.Cartorio.Infrastructure.Utility.ApiClient;

namespace TGS.Cartorio.Application.DTO
{
    public class LogSistemaDto
    {
        public CodLogSistema CodLogSistema { get; set; }
        public string JsonConteudo { get; set; }

        public static string CreateConteudo(object obj)
        {
            try
            {
                return JsonConvert.SerializeObject(obj);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static LogSistemaDto Create(CodLogSistema codLogSistema, string jsonConteudo)
        {
            try
            {
                return new LogSistemaDto
                {
                    CodLogSistema = codLogSistema,
                    JsonConteudo = jsonConteudo
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void RemoverDadosRequest(LogServicoDto log)
        {
            try
            {
                if (string.IsNullOrEmpty(log.Request))
                    return;


                var objRequest = JsonConvert.DeserializeObject(log.Request);

                if (objRequest is CartaoCreditoDto)
                {
                    ((CartaoCreditoDto)objRequest).Cartao = null;
                    log.Request = JsonConvert.SerializeObject(((CartaoCreditoDto)objRequest));
                }
            }
            catch { }
        }
    }


    public enum CodLogSistema
    {
        Job_AtualizacaoSolicitacaoBoletoPagoComSucesso,
        Job_Erro_AtualizarStatusDaSolicitacaoPorBoletoPago,

        //enviado e-mail para cartório com sucesso
        Erro_SolicitacoesAppService_AoEnviarEmailParaCartorio,
        Erro_SolicitacoesAppService_AoGerarTemplateLayoutParaPDFEnvioCartorio,
        Job_EnviadoEmailParaCartorio,
        Job_ErroAEnviarEmailParaCartorio,

        //alterado estado da solicitação para 'SolicitacaoEnviadaAoCartorio' com sucesso
        SolicitacoesAppService_AlteradoEstadoDaSolicitacaoParaSolicitacaoEnviadaAoCartorio,
        Erro_SolicitacoesAppService_AoTentarAlterarEstadoDaSolicitacaoParaSolicitacaoEnviadaAoCartorio,
        Job_AlteradoEstadoDaSolicitacaoParaSolicitacaoEnviadaAoCartorio,
        Job_ErroAoAlterarEstadoDaSolicitacaoParaSolicitacaoEnviadaAoCartorio,

        Erro_PessoasAppService_BuscarPorIdCompleto,
        Erro_PessoasAppService_BuscarPorIdCompleto_SolicitanteNaoLocalizado,
        Erro_PessoasAppService_BuscarPorIdCompleto_SolicitanteSemDadosContato,
        Erro_PessoasAppService_BuscarPorIdCompleto_ErroAoDesserializarDadosContatoDoSolicitante,

        //solicitação pronta para envio com sucesso
        Job_SolicitacaoProntaParaEnvio,
        Job_ErroNaCargaDeEnvioDaSolicitacao,

        Erro_UsuariosAppService_BuscarPorIdPessoa,
        Erro_PagamentoController_PagarComCartaoCredito_BuscarPorIdPessoa_NaoFoiLocalizado,
        Erro_PagamentoController_PagarComCartaoCredito_ErroAoMontarDadosPagamento,

        Erro_TaxasAppService_BuscarTaxaPorBoleto_ConfiguracaoTaxaTabelionetPorBoletoNaoLocalizada,
        Erro_TaxasAppService_BuscarTaxaPorBoleto_ErroAoConverterTaxa,
        Erro_TaxasAppService_BuscarTaxaPorBoleto_ErroGeral,


        ContaAppService_Cadastrar_ApiIdentity_CadastroContaUsuario,
        Erro_ContaAppService_Cadastrar_ApiIdentity_CadastroContaUsuario,
        ContaAppService_Cadastrar_InclusaoUsuario,
        Erro_ContaAppService_Cadastrar_InclusaoUsuario,
        ContaAppService_Cadastrar_InclusaoPessoa,
        Erro_ContaAppService_Cadastrar_InclusaoPessoa,
        ContaAppService_Cadastrar_InclusaoContato,
        Erro_ContaAppService_Cadastrar_InclusaoContato,
        ContaAppService_Cadastrar_InclusaoPessoasContato,
        Erro_ContaAppService_Cadastrar_InclusaoPessoasContato,
        ContaAppService_Cadastrar_InclusaoPessoaFisica,
        Erro_ContaAppService_Cadastrar_InclusaoPessoaFisica,
        ContaAppService_Cadastrar_AtualizarUsuarioComIdPessoa,
        Erro_ContaAppService_Cadastrar_AtualizarUsuarioComIdPessoa,
        Erro_ContaAppService_Cadastrar_Geral,

        ApiIdentity_ContaAppService_EfetuarLogin,
        Erro_ApiIdentity_ContaAppService_AoLogar,
        Erro_ApiIdentity_ContaAppService_AoBuscarUsuarioLogin,
        ApiIdentity_ContaAppService_EnviarEmailAtivacao,
        Erro_ApiIdentity_ContaAppService_EnviarEmailAtivacao,
        ApiIdentity_ContaAppService_ConfirmarEmailAtivacao,
        Erro_ApiIdentity_ContaAppService_ConfirmarEmailAtivacao,
        ContaAppService_ConfirmarEmailAtivacao_AtualizacaoUsuario,
        Erro_ContaAppService_ConfirmarEmailAtivacao_AtualizacaoUsuario,

        ApiIdentity_ContaAppService_EnviarEmailResetSenha,
        Erro_ApiIdentity_ContaAppService_EnviarEmailResetSenha,
        ApiIdentity_ContaAppService_ResetarSenha,
        Erro_ApiIdentity_ContaAppService_ResetarSenha,
        ApiIdentity_ContaAppService_AlterarSenha,
        Erro_ApiIdentity_ContaAppService_AlterarSenha,
        ApiIdentity_ContaAppService_ObterEmail,
        Erro_ApiIdentity_ContaAppService_ObterEmail,
        Erro_ApiIdentity_ContaAppService_BuscaUsuarioParaEnvioEmailSMS,
        Erro_ApiIdentity_ContaAppService_EnvioSMS,
        Erro_ApiIdentity_ContaAppService_EnvioEmail,

        Erro_ApiIdentity_ContaAppService_BuscarDadosConta_BuscarUsuario,
        Erro_ApiIdentity_ContaAppService_BuscarDadosConta_BuscarPessoa,
        Erro_ApiIdentity_ContaAppService_BuscarDadosConta_BuscarListaContatos,


        Erro_SolicitacoesAppService_GerarPdfCartorio,

        Erro_SolicitacoesAppService_ConsultarStatusSolicitacao,

        Erro_SolicitacoesAppService_BuscarDadosProcuracaoSolicitante,

        Send_SMS,
        Erro_Send_SMS,
        Erro_Send_SMS_AoGerarTemplateCriarConta,
        Erro_Send_SMS_AoGerarTemplateAlterarSenha,
        Erro_Send_SMS_AoGerarTemplateCriarSolicitacao,
        Erro_Send_SMS_AoGerarTemplateCriarEndereco,
        Erro_Send_SMS_AoGerarTemplateAlterarEndereco,
        Erro_Send_SMS_AoGerarTemplateSalvarDadosPessoais,

        Send_Email,
        Erro_Send_Email,


        EmailController_SendEmail,
        Erro_EmailController_SendEmail,
        AssinaturaDigital_PrimeiroPasso,
        AssinaturaDigital_SegundoPasso,
        Erro_AssinaturaDigital_PrimeiroPasso,
        Erro_AssinaturaDigital_SegundoPasso,
        AssinaturaDigital_ValidacaoDocumento,
        Erro_AssinaturaDigital_ValidacaoDocumento,

        Erro_PessoasEnderecosAppService_BuscarPorPessoa,
        Erro_PessoasEnderecosAppService_BuscarPorEndereco,
        Erro_PagamentoController_ErroAoDesserializarEnderecoPessoaSolicitante,
        Erro_PessoasEnderecosAppService_,

        SolicitacoesAppService_SolicitacaoProntaParaEnvio,
        SolicitacoesAppService_ErroNaCargaDeEnvioDaSolicitacao,
        //CarrinhoController
        CarrinhoController_BuscarSolicitante,
        Erro_CarrinhoController_BuscarSolicitante,
        CarrinhoController_ObterParticipantes,
        Erro_CarrinhoController_ObterParticipantes,
        CarrinhoController_ObterProduto,
        Erro_CarrinhoController_ObterProduto,
        CarrinhoController_ObterComposicaoPrecos,
        Erro_CarrinhoController_ObterComposicaoPrecos,
        CarrinhoController_ObterTermoConcordancia,
        Erro_CarrinhoController_ObterTermoConcordancia,
        CarrinhoController_AceiteTermoConcordancia,
        Erro_CarrinhoController_AceiteTermoConcordancia,
        //ContaController
        ContaController_Cadastrar,
        Erro_ContaController_Cadastrar,
        ContaController_Login,
        Erro_ContaController_Login,
        ContaController_EnviarEmailAtivacao,
        Erro_ContaController_EnviarEmailAtivacao,
        ContaController_ConfirmarEmailAtivacao,
        Erro_ContaController_ConfirmarEmailAtivacao,
        ContaController_EnviarEmailResetSenha,
        Erro_ContaController_EnviarEmailResetSenha,
        ContaController_ResetarSenha,
        Erro_ContaController_ResetarSenha,
        ContaController_AlterarSenha,
        Erro_ContaController_AlterarSenha,
        ContaController_BuscarDadosConta,
        Erro_ContaController_BuscarDadosConta,
        ContaController_BuscarDadosUsuario,
        Erro_ContaController_BuscarDadosUsuario,
        ContaController_SalvarTelefone,
        Erro_ContaController_SalvarTelefone,
        ContaController_SalvarDadosPessoais,
        Erro_ContaController_SalvarDadosPessoais,
        ContaController_BuscarDadosUsuarioSolicitante,
        Erro_ContaController_BuscarDadosUsuarioSolicitante,
        ContaController_ValidarDocumentoComEmail,
        Erro_ContaController_ValidarDocumentoComEmail,
        ContaController_ValidarEmailPertenceAoDocumento,
        Erro_ContaController_ValidarEmailPertenceAoDocumento,
        ContaController_VerificarDadosSolicitanteAtualizados_BuscarDadosUsuario,
        Erro_ContaController_VerificarDadosSolicitanteAtualizados_BuscarDadosUsuario,
        //EnderecosController
        EnderecosController_Buscar,
        Erro_EnderecosController_Buscar,
        EnderecosController_IncluirEndereco_CountByIdPessoa,
        Erro_EnderecosController_IncluirEndereco_CountByIdPessoa,
        EnderecosController_IncluirEndereco_enderecoAppService_Incluir,
        Erro_EnderecosController_IncluirEndereco_enderecoAppService_Incluir,
        EnderecosController_IncluirEndereco_pessoasEnderecosAppService_Incluir,
        Erro_EnderecosController_IncluirEndereco_pessoasEnderecosAppService_Incluir,
        EnderecosController_IncluirEndereco_usuarioAppService_BuscarId,
        Erro_EnderecosController_IncluirEndereco_usuarioAppService_BuscarId,
        EnderecosController_IncluirEndereco_smsAppService_GetTemplateCriarEndereco,
        Erro_EnderecosController_IncluirEndereco_smsAppService_GetTemplateCriarEndereco,
        EnderecosController_IncluirEndereco_emailAppService_GetTemplateCriarEndereco,
        Erro_EnderecosController_IncluirEndereco_emailAppService_GetTemplateCriarEndereco,
        EnderecosController_IncluirEndereco_smsAppService_Send,
        Erro_EnderecosController_IncluirEndereco_smsAppService_Send,
        EnderecosController_IncluirEndereco_emailAppService_Send,
        Erro_EnderecosController_IncluirEndereco_emailAppService_Send,
        EnderecosController_AtualizarEndereco_Atualizar,
        Erro_EnderecosController_AtualizarEndereco_Atualizar,
        EnderecosController_AtualizarEndereco_BuscarId,
        Erro_EnderecosController_AtualizarEndereco_BuscarId,
        EnderecosController_AtualizarEndereco_smsAppService_GetTemplateAlterarEndereco,
        Erro_EnderecosController_AtualizarEndereco_smsAppService_GetTemplateAlterarEndereco,
        EnderecosController_AtualizarEndereco_smsAppService_Send,
        Erro_EnderecosController_AtualizarEndereco_smsAppService_Send,
        EnderecosController_AtualizarEndereco_emailAppService_GetTemplateAlterarEndereco,
        Erro_EnderecosController_AtualizarEndereco_emailAppService_GetTemplateAlterarEndereco,
        EnderecosController_AtualizarEndereco_emailAppService_Send,
        Erro_EnderecosController_AtualizarEndereco_emailAppService_Send,
        EnderecosController_ApagarEndereco_RemoverPorIdEndereco,
        Erro_EnderecosController_ApagarEndereco_RemoverPorIdEndereco,
        EnderecosController_ApagarEndereco_Apagar,
        Erro_EnderecosController_ApagarEndereco_Apagar,
        Erro_EnderecosController_ApagarEndereco_AoAtualizarUmNovoEnderecoPrincipal,
        EnderecosController_AtualizarEnderecoPrincipal_AtualizarEnderecoPrincipal,
        Erro_EnderecosController_AtualizarEnderecoPrincipal_AtualizarEnderecoPrincipal,
        EnderecosController_AtualizarEnderecoPrincipal_BuscarId,
        Erro_EnderecosController_AtualizarEnderecoPrincipal_BuscarIdNaoLocalizado,
        Erro_EnderecosController_AtualizarEnderecoPrincipal_BuscarId,
        //GenerosPCController
        GenerosPCController_BuscarTodos,
        Erro_GenerosPCController_BuscarTodos,
        //PagamentoController        
        PagamentoAppService_SimularParcelamento,
        PagamentoAppService_PagarComCartaoCredito,
        Erro_PagamentoAppService_PagarComCartaoCredito,

        PagamentoController_PagarViaBoleto_SolicitacaoBuscarId,
        PagamentoController_PagarViaBoleto_SolicitacaoSemIdPessoaSolicitante,
        PagamentoController_PagarViaBoleto_SolicitanteNaoLocalizado,
        PagamentoController_PagarViaBoleto_SolicitanteEstaSemEndereco,
        Erro_PagamentoController_PagarViaBoleto_AoDesserializarEnderecoSolicitante,
        Erro_PagamentoController_PagarViaBoleto_TaxasAppService_BuscarComposicaoPrecoProdutoTotal,
        Erro_PagamentoController_PagarViaBoleto_TaxasAppService_BuscarTaxaPorBoleto,
        Erro_PagamentoController_PagarViaBoleto_UsuarioNaoLocalizado,
        Erro_PagamentoController_PagarViaBoleto_GetToken,


        Erro_SolicitacoesController_ConsultarBoleto_GetToken,

        

        Erro_PagamentoController_ConsultarPagamentoBoleto_GetToken,
        Erro_PagamentoAppService_ConsultarPagamentoBoleto_BuscarSolicitacao,
        Erro_PagamentoAppService_ConsultarPagamentoBoleto_AoDesserializarSolicitacaoConteudo,
        Erro_PagamentoAppService_ConsultarPagamentoBoleto_AoDesserializarCamposPagamento,
        PagamentoAppService_ConsultarPagamentoBoleto_ConsultaBoletoIdentificador_Response,
        Erro_PagamentoAppService_ConsultarPagamentoBoleto_ConsultaBoletoIdentificador_Response,





        PagamentoAppService_ConsultarTransacaoCartaoCreditoPorIdentifier,
        PagamentoAppService_ConsultarTransacaoCartaoCreditoPorSolicitacao_BuscarId,
        Erro_PagamentoAppService_ConsultarTransacaoCartaoCreditoPorSolicitacao_BuscarId,
        Erro_PagamentoAppService_ConsultarTransacaoCartaoCreditoPorSolicitacao_CamposPagamento,
        PagamentoAppService_ConsultarTransacaoCartaoCreditoPorSolicitacao_CamposPagamento,
        PagamentoAppService_ConsultarTransacaoCartaoCreditoPorSolicitacao_Response,
        Erro_PagamentoAppService_ConsultarTransacaoCartaoCreditoPorSolicitacao_Response,

        Erro_PagamentoAppService_ConsultarTransacaoCartaoCreditoPorIdentifier,
        Erro_PagamentoAppService_SimularParcelamento,
        Erro_PagamentoAppService_SimularParcelamento_GetToken,
        Erro_PagamentoController_PagarComCartaoCredito_ErroAoDesserializarCamposPagamentoDaSolicitacao,
        Erro_PagamentoController_PagarComCartaoCredito_ErroAoDesserializarCampoConteudoDaSolicitacao,
        Erro_PagamentoController_PagarComCartaoCredito_SolicitacaoEstaSemIdPessoa_Solicitante,
        Erro_PagamentoController_PagarComCartaoCredito_GetToken,
        Erro_PagamentoController_PagarComCartaoCredito_SolicitacaoComEstadoDiferenteDoMomentoDePagamento,
        Erro_PagamentoController_PagarComCartaoCredito_BuscarId,
        PagamentoController_PagarComCartaoCredito_ConsultarTransacaoCartaoCreditoPorIdentifier,
        Erro_PagamentoController_PagarComCartaoCredito_ConsultarTransacaoCartaoCreditoPorIdentifier,
        PagamentoController_PagarComCartaoCredito_pessoasAppService_BuscarPorIdCompleto,
        Erro_PagamentoController_PagarComCartaoCredito_pessoasAppService_BuscarPorIdCompleto,
        PagamentoController_PagarComCartaoCredito_pessoasEnderecosAppService_BuscarPorPessoa,
        Erro_PagamentoController_PagarComCartaoCredito_pessoasEnderecosAppService_BuscarPorPessoa,
        PagamentoController_PagarComCartaoCredito_usuarioAppService_BuscarPorIdPessoa,
        Erro_PagamentoController_PagarComCartaoCredito_usuarioAppService_BuscarPorIdPessoa,
        PagamentoController_PagarComCartaoCredito_PagarComCartaoCredito,
        Erro_PagamentoController_PagarComCartaoCredito,
        Erro_PagamentoController_PagarComCartaoCredito_AoPrepararSolicitacaoParaAtualizarEstado,
        Erro_PagamentoController_PagarComCartaoCredito_DesserializarSolicitacaoEmSolicitacaoDto,
        Erro_PagamentoController_PagarComCartaoCredito_AoAtualizarSolicitacao,

        Erro_PagamentoController_ConsultarTransacaoCartaoCredito_GetToken,
        Erro_PagamentoController_PagarComCartaoCredito_PagarComCartaoCredito,        

        Erro_TaxasController_BuscarTaxasPorSolicitacao,
        Erro_TaxasAppService_BuscarComposicaoPrecoProdutoTotal_BuscarSolicitacao,
        Erro_TaxasAppService_BuscarComposicaoPrecoProdutoTotal_DesserializeProduto,
        Erro_TaxasAppService_BuscarComposicaoPrecoProdutoTotal_BuscarTaxasPorSolicitacao,

        Erro_SolicitacoesAppService_Incluir_Mapeamento_SolicitacoesSimplificadoDto_Solicitacoes,
        Erro_SolicitacoesAppService_Incluir_BuscarCartorioValido,
        Erro_SolicitacoesAppService_Incluir_Solicitacao,
        
        Erro_SolicitacoesAppService_IncluirTaxaEmolumento_BuscarTodosCartorios,
        Erro_SolicitacoesAppService_IncluirTaxaEmolumento_BuscarTaxaEmolumentoPorEstado,
        Erro_SolicitacoesAppService_IncluirTaxaEmolumento_IncluirSolicitacoesTaxas,
        Erro_SolicitacoesAppService_IncluirTaxaEmolumento_AoBuscarUltimoCartorioValido,
        Erro_SolicitacoesAppService_IncluirTaxaEmolumento_CartorioSelecionadoNaoPossuiDadosDeEndereco,
        Erro_SolicitacoesAppService_IncluirTaxaEmolumento_TaxasExtras,
        Erro_SolicitacoesAppService_Incluir_BuscarUsuario,
        Erro_SolicitacoesAppService_Incluir_BuscarPessoa,

        Erro_SolicitacoesAppService_BuscarTodosComNoLock,
        
        Erro_SolicitacoesAppService_BuscarDadosStatusSolicitacao,

        Erro_SolicitacoesAppService_Atualizar,
        
        Erro_SolicitacoesAppService_BuscarId,

        Erro_SolicitacoesAppService_AtualizarSolicitacaoParaCarrinho_BuscarSolicitacao,
        Erro_SolicitacoesAppService_AtualizarSolicitacaoParaCarrinho_AtualizarPropriedadeEstadoDominio,
        Erro_SolicitacoesAppService_AtualizarSolicitacaoParaCarrinho_AtualizarSolicitacao,

        Erro_SolicitacoesAppService_AtualizarSolicitacaoParaAguardandoPagamento_BuscarSolicitacao,
        Erro_SolicitacoesAppService_AtualizarSolicitacaoParaAguardandoPagamento_AtualizarPropriedadeEstadoDominio,
        Erro_SolicitacoesAppService_AtualizarSolicitacaoParaAguardandoPagamento_AtualizarSolicitacao,

        Erro_SolicitacoesAppService_AtualizarSolicitacaoParaProntaParaEnvioCartorio_BuscarSolicitacao,
        Erro_SolicitacoesAppService_AtualizarSolicitacaoParaProntaParaEnvioCartorio_AtualizarPropriedadeEstadoDominio,
        Erro_SolicitacoesAppService_AtualizarSolicitacaoParaProntaParaEnvioCartorio_AtualizarSolicitacao,

        Erro_SolicitacoesAppService_AtualizarSolicitacaoParaAguardandoAssinaturaDigitalSolicitante_BuscarSolicitacao,
        Erro_SolicitacoesAppService_AtualizarSolicitacaoParaAguardandoAssinaturaDigitalSolicitante_AtualizarPropriedadeEstadoDominio,
        Erro_SolicitacoesAppService_AtualizarSolicitacaoParaAguardandoAssinaturaDigitalSolicitante_AtualizarSolicitacao,

        Erro_SolicitacoesAppService_BuscarSolicitacao_BuscarPorId,
        Erro_SolicitacoesAppService_BuscarSolicitacao_SolicitacaoNaoEstaMaisDisponivelParaAlteracoes,
        Erro_SolicitacoesAppService_BuscarSolicitacao_GetProdutoMatrimonio,
        Erro_SolicitacoesAppService_BuscarSolicitacao_BuscarMatrimonioDocumentoProclamas,
        Erro_SolicitacoesAppService_BuscarSolicitacao_SolicitacaoConteudo,
        Erro_SolicitacoesAppService_BuscarSolicitacao_Erro,

        Erro_SolicitacoesAppService_ConsultarBoleto,

        Erro_ConsumoAPI_SolicitacoesAppService_GerarBoleto,
        ConsumoAPI_SolicitacoesAppService_GerarBoleto_Response_Com_Sucesso,
        ConsumoAPI_SolicitacoesAppService_GerarBoleto_Response_Sem_Sucesso,
        Erro_ConsumoAPI_SolicitacoesAppService_GerarBoleto_BuscarSolicitacao,
        Erro_ConsumoAPI_SolicitacoesAppService_GerarBoleto_TryConvertJsonObj,
        Erro_ConsumoAPI_SolicitacoesAppService_GerarBoleto_PreencherCamposPagamentoDaSolicitacao,
        Erro_ConsumoAPI_SolicitacoesAppService_GerarBoleto_AtualizarSolicitacao,

        Erro_SolicitacoesAppService_BuscarEstadoDoPedidoPorParticipante,

        JOB_Erro_SolicitacoesAppService_BuscarSolicitacoesProntasParaEnvioParaCartorio_BuscarSolicitacoes,
        JOB_Erro_SolicitacoesAppService_BuscarSolicitacoesProntasParaEnvioParaCartorio_BuscarDadosUltimoCartorio,
        JOB_Erro_SolicitacoesAppService_BuscarSolicitacoesProntasParaEnvioParaCartorio_DadosUltimoCartorio_SemRazaoSocialOuEmail,

        JOB_Erro_SolicitacoesAppService_BuscarSolicitacoesProntasParaEnvioParaCartorio_GravarTodosDados,

        JOB_Erro_SolicitacoesAppService_CarregarTodosOsDadosDaSolicitacaoParaEnvio_DesserializarSolicitacaoConteudo,
        JOB_Erro_SolicitacoesAppService_CarregarTodosOsDadosDaSolicitacaoParaEnvio_BuscarProcuracoesPartesDaSolicitacao,
        JOB_Erro_SolicitacoesAppService_CarregarTodosOsDadosDaSolicitacaoParaEnvio_AoPreencherDadosProcuracoesPartes,
        JOB_Erro_SolicitacoesAppService_CarregarTodosOsDadosDaSolicitacaoParaEnvio_BuscarDadosMatrimonio,
        JOB_Erro_SolicitacoesAppService_CarregarTodosOsDadosDaSolicitacaoParaEnvio_DesserializarMatrimonioDto,
        JOB_Erro_SolicitacoesAppService_CarregarTodosOsDadosDaSolicitacaoParaEnvio_BuscarDocumentosMatrimonios,

        Erro_ProdutosModalidadesPcController_BuscarTodos,

        Erro_ProdutosAppService_BuscarId,
        Erro_ProdutosAppService_BuscarId_DesserializarParaDto,

        Erro_ProdutosAppService_BuscarDadosVitrine,
        Erro_ProdutosAppService_BuscarDadosVitrine_ListaProdutosVitrine,
        Erro_ProdutosAppService_BuscarDadosVitrine_ProdutosCategoriasPcNaoLocalizados,

        Erro_ProdutosAppService_BuscarDetalhesProduto,
        Erro_ProdutosAppService_BuscarDetalhesProduto_DesserializarProdutosImagens,
        Erro_ProdutosAppService_BuscarDetalhesProduto_AddProdutosModalidades,

        Erro_ProdutosController_BuscarTodos,

        Erro_MatrimoniosAppService_BuscarPorSolicitacao,
        Erro_MatrimoniosAppService_DesserializarDadosMatrimonios,
        Erro_MatrimoniosAppService_BuscarPorSolicitacao_BuscarMatrimoniosDocumentos,

        Erro_PessoasController_PessoaExiste,



        Desconhecido

    }
}
