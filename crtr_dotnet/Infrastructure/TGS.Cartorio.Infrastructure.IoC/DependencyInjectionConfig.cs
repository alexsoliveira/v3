using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TGS.Cartorio.Application.AppServices;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Application.CertificadoDigital;
using TGS.Cartorio.Application.CertificadoDigital.interfaces;
using TGS.Cartorio.Application.Comunicador;
using TGS.Cartorio.Application.Comunicador.Interfaces;
using TGS.Cartorio.Application.DTO;
using TGS.Cartorio.Application.Relatorios;
using TGS.Cartorio.Application.Relatorios.Interfaces;
using TGS.Cartorio.Application.Templates;
using TGS.Cartorio.Application.Templates.Interfaces;
using TGS.Cartorio.Application.Validation;
using TGS.Cartorio.Application.Validation.Interfaces;
using TGS.Cartorio.Application.ViewModel.Identity;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer.Procuracoes;
using TGS.Cartorio.Domain.Interfaces.Services;
using TGS.Cartorio.Domain.Interfaces.Services.Strategy;
using TGS.Cartorio.Domain.Services;
using TGS.Cartorio.Domain.Services.ConcreteStrategy;
using TGS.Cartorio.Infrastructure.SqlServer.Context;
using TGS.Cartorio.Infrastructure.SqlServer.Repositories;
using TGS.Cartorio.Infrastructure.SqlServer.Repositories.Procuracoes;
using TGS.Cartorio.Infrastructure.Utility.ApiClient;
using TGS.Cartorio.Infrastructure.Utility.Settings;
using TGS.Cartorio.Infrastructure.Utility.WebServerContracts;
using TGS.Cartorio.Infrastructure.Utility.WebServerContracts.Interfaces;

namespace TGS.Cartorio.Infrastructure.IoC
{
    public static class DependencyInjectionConfig
    {

        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();

            var configuration = provider.GetService<IConfiguration>();

            #region Application Validation

            services.AddScoped<IUsuariosValidation, UsuariosValidation>();
            services.AddScoped<ISolicitacoesValidation, SolicitacoesValidation>();
            services.AddScoped<IProdutosValidation, ProdutosValidation>();
            services.AddScoped<IPessoasValidation, PessoasValidation>();
            services.AddScoped<ITiposDocumentosPcValidation, TiposDocumentosPcValidation>();
            services.AddScoped<ITiposPartesPcValidation, TiposPartesPcValidation>();
            services.AddTransient<IValidator<PessoasFisicas>, PessoasFisicasValidation>();
            services.AddTransient<IValidator<PessoasJuridicas>, PessoasJuridicasValidation>();
            services.AddTransient<IValidator<Pessoas>, PessoasValidation>();
            services.AddTransient<IValidator<Usuarios>, UsuariosValidation>();
            services.AddTransient<IValidator<Produtos>, ProdutosValidation>();
            services.AddTransient<IValidator<SolicitacoesDto>, SolicitacoesValidation>();
            services.AddTransient<IValidator<SolicitacoesNotificacoes>, SolicitacoesNotificacoesValidation>();
            services.AddTransient<IValidator<UsuarioRegistro>, ContaValidation>();
            services.AddTransient<IValidator<UsuarioLogin>, LoginValidation>();
            services.AddTransient<IValidator<EnderecoDto>, EnderecoValidation>();
            services.AddTransient<IValidator<UsuarioAlterarSenha>, AlterarSenhaValidation>();
            #endregion


            #region Application Reports
            services.AddTransient<IPdfSolicitacaoReport, PdfSolicitacaoReport>();
            #endregion


            #region Application

            services.AddScoped<IEmailAppService, EmailAppService>();
            services.AddScoped<IProcuracoesPartesAppService, ProcuracoesPartesAppService>();
            services.AddScoped<IUsuariosAppService, UsuariosAppService>();
            services.AddScoped<IProdutosAppService, ProdutosAppService>();
            services.AddScoped<IProdutosImagensAppService, ProdutosImagensAppService>();
            services.AddScoped<IProdutosModalidadesAppService, ProdutosModalidadesAppService>();
            services.AddScoped<IMatrimoniosAppService, MatrimoniosAppService>();
            services.AddScoped<ITaxasAppService, TaxasAppService>();
            
            services.AddScoped<IProcuracoesPartesEstadosAppService, ProcuracoesPartesEstadosAppService>();
            services.AddScoped<ICarrinhoAppService, CarrinhoAppService>();
            services.AddScoped<IPagamentoAppService, PagamentoAppService>();

            services.AddScoped<IPessoasAppService, PessoasAppService>();
            services.AddScoped<IContatosAppService, ContatosAppService>();
            services.AddScoped<IEnderecosAppService, EnderecosAppService>();
            services.AddScoped<IProdutosDocumentosAppService, ProdutosDocumentosAppService>();
            services.AddScoped<ICartoriosAppService, CartoriosAppService>();
            services.AddScoped<ICartoriosContatosAppService, CartoriosContatosAppService>();
            services.AddScoped<ICartoriosEnderecosAppService, CartoriosEnderecosAppService>();
            services.AddScoped<ISolicitacoesAppService, SolicitacoesAppService>();
            services.AddScoped<ISolicitacoesDocumentosAppService, SolicitacoesDocumentosAppService>();
            services.AddScoped<ISolicitacoesNotificacoesAppService, SolicitacoesNotificacoesAppService>();
            services.AddScoped<ITiposPartesPCAppService, TiposPartesPCAppService>();
            services.AddScoped<ITiposFretesPCAppService, TiposFretesPCAppService>();
            services.AddScoped<ICartoriosEstadosPcAppService, CartoriosEstadosPcAppService>();
            services.AddScoped<ICartoriosModalidadesPcAppService, CartoriosModalidadesPcAppService>();
            services.AddScoped<IGenerosPcAppService, GenerosPcAppService>();
            services.AddScoped<IProdutosModalidadesPcAppService, ProdutosModalidadesPcAppService>();
            services.AddScoped<ISolicitacoesEstadosPcAppService, SolicitacoesEstadosPcAppService>();
            services.AddScoped<ISolicitacoesEstadosAppService, SolicitacoesEstadosAppService>();
            services.AddScoped<ITiposContatosPcAppService, TiposContatosPcAppService>();
            services.AddScoped<ITiposDocumentosPcAppService, TiposDocumentosPcAppService>();
            services.AddScoped<IPessoasContatosAppService, PessoasContatosAppService>();
            services.AddScoped<IPessoasEnderecosAppService, PessoasEnderecosAppService>();
            services.AddScoped<IPessoasFisicasAppService, PessoasFisicasAppService>();
            services.AddScoped<IPessoasJuridicasAppService, PessoasJuridicasAppService>();
            services.AddScoped<IConfiguracoesAppService, ConfiguracoesAppService>();
            services.AddScoped<IContaAppService, ContaAppService>();
            services.AddScoped<IUsuariosContatosAppService, UsuariosContatosAppService>();
            //services.AddScoped<ICertificadoAppService, CertificadoAppService>();
            services.AddScoped<IAssinaturaDigitalAppService, AssinaturaDigitalAppService>();
            services.AddScoped<ISmsAppService, SmsAppService>();
            services.AddScoped<ICarrinhoAppService, CarrinhoAppService>();
            services.AddScoped<ILogSistemaAppService, LogSistemaAppService>();


            #endregion

            #region Services
            services.AddScoped<IUsuariosService, UsuariosService>();
            services.AddScoped<IProdutosService, ProdutosService>();
            services.AddScoped<IProdutosImagensService, ProdutosImagensService>();
            services.AddScoped<IProdutosModalidadesService, ProdutosModalidadesService>();

            services.AddScoped<ISolicitacoesTaxasService, SolicitacoesTaxasService>();
            services.AddScoped<ITaxasExtrasService, TaxasExtrasService>();

            services.AddScoped<IProcuracoesPartesEstadosService, ProcuracoesPartesEstadosService>();
            services.AddScoped<IMatrimoniosService, MatrimoniosService>();
            services.AddScoped<IMatrimoniosDocumentosService, MatrimoniosDocumentosService>();

            services.AddScoped<IProcuracoesPartesService, ProcuracoesPartesService>();

            services.AddScoped<ISolicitacoesService, SolicitacoesService>();
            services.AddScoped<IPessoasService, PessoasService>();
            services.AddScoped<IContatosService, ContatosService>();
            services.AddScoped<IEnderecosService, EnderecosService>();
            services.AddScoped<IProdutosDocumentosService, ProdutosDocumentosService>();
            services.AddScoped<ICartoriosService, CartoriosService>();
            services.AddScoped<ICartoriosContatosService, CartoriosContatosService>();
            services.AddScoped<ICartoriosEnderecosService, CartoriosEnderecosService>();
            services.AddScoped<ISolicitacoesService, SolicitacoesService>();
            services.AddScoped<ISolicitacoesDocumentosService, SolicitacoesDocumentosService>();
            services.AddScoped<ISolicitacoesNotificacoesService, SolicitacoesNotificacoesService>();
            services.AddScoped<ITiposPartesPCService, TiposPartesPCService>();
            services.AddScoped<ITiposFretesPCService, TiposFretesPCService>();
            services.AddScoped<ICartoriosEstadosPCService, CartoriosEstadosPCService>();
            services.AddScoped<ICartoriosModalidadesPCService, CartoriosModalidadesPCService>();
            services.AddScoped<IGenerosPCService, GenerosPCService>();
            services.AddScoped<IProdutosModalidadesPCService, ProdutosModalidadesPCService>();
            services.AddScoped<ISolicitacoesEstadosPCService, SolicitacoesEstadosPCService>();
            services.AddScoped<ISolicitacoesEstadosService, SolicitacoesEstadosService>();
            services.AddScoped<ITiposContatosPCService, TiposContatosPCService>();
            services.AddScoped<ITiposDocumentosPCService, TiposDocumentosPCService>();
            services.AddScoped<IPessoasContatosService, PessoasContatosService>();
            services.AddScoped<IPessoasEnderecosService, PessoasEnderecosService>();
            services.AddScoped<IPessoasFisicasService, PessoasFisicasService>();
            services.AddScoped<IPessoasJuridicasService, PessoasJuridicasService>();
            services.AddScoped<IConfiguracoesService, ConfiguracoesService>();
            services.AddScoped<IUsuariosContatosService, UsuariosContatosService>();

            services.AddScoped<IRegrasOutorgantesStrategy, CriarOutorganteExisteNoSistema>();
            services.AddScoped<IRegrasOutorgantesStrategy, CriarOutorganteNaoExisteNoSistema>();
            services.AddScoped<IRegrasOutorgantesStrategy, CriarOutorganteSolicitante>();

            services.AddScoped<ILogSistemaService, LogSistemaService>();
            #endregion

            #region Repositories
            // * SqlServer
            services.AddDbContext<EFContext>(options => options.UseSqlServer(configuration.GetConnectionString("DbTabelionetContext")));
            services.AddScoped<IUsuariosSqlRepository, UsuariosSqlRepository>();
            services.AddScoped<IProdutosSqlRepository, ProdutosSqlRepository>();
            services.AddScoped<ISolicitacoesTaxasSqlRepository, SolicitacoesTaxasSqlRepository>();
            services.AddScoped<ITaxasExtrasSqlRepository, TaxasExtrasSqlRepository>();
            services.AddScoped<IProdutosImagensSqlRepository, ProdutosImagensSqlRepository>();
            services.AddScoped<IProdutosModalidadesSqlRepository, ProdutosModalidadesSqlRepository>();
            services.AddScoped<ISolicitacoesSqlRepository, SolicitacoesSqlRepository>();
            services.AddScoped<IPessoasSqlRepository, PessoasSqlRepository>();
            services.AddScoped<IContatosSqlRepository, ContatosSqlRepository>();
            services.AddScoped<IEnderecosSqlRepository, EnderecosSqlRepository>();
            services.AddScoped<IProdutosDocumentosSqlRepository, ProdutosDocumentosSqlRepository>();
            services.AddScoped<ICartoriosSqlRepository, CartoriosSqlRepository>();
            services.AddScoped<ICartoriosContatosSqlRepository, CartoriosContatosSqlRepository>();
            services.AddScoped<ICartoriosEnderecosSqlRepository, CartoriosEnderecosSqlRepository>();
            services.AddScoped<ISolicitacoesSqlRepository, SolicitacoesSqlRepository>();
            services.AddScoped<ISolicitacoesDocumentosSqlRepository, SolicitacoesDocumentosSqlRepository>();
            services.AddScoped<ISolicitacoesNotificacoesSqlRepository, SolicitacoesNotificacoesSqlRepository>();
            services.AddScoped<ITiposPartesPCSqlRepository, TiposPartesPCSqlRepository>();
            services.AddScoped<ITiposFretesPCSqlRepository, TiposFretesPCSqlRepository>();
            services.AddScoped<ICartoriosEstadosPCSqlRepository, CartoriosEstadosPCSqlRepository>();
            services.AddScoped<ICartoriosModalidadesPCSqlRepository, CartoriosModalidadesPCSqlRepository>();
            services.AddScoped<IGenerosPCSqlRepository, GenerosPCSqlRepository>();
            services.AddScoped<IProdutosModalidadesPCSqlRepository, ProdutosModalidadesPCSqlRepository>();
            services.AddScoped<ISolicitacoesEstadosPCSqlRepository, SolicitacoesEstadosPCSqlRepository>();
            services.AddScoped<ISolicitacoesEstadosSqlRepository, SolicitacoesEstadosSqlRepository>();
            services.AddScoped<ITiposContatosPCSqlRepository, TiposContatosPCSqlRepository>();
            services.AddScoped<ITiposDocumentosPCSqlRepository, TiposDocumentosPCSqlRepository>();
            services.AddScoped<IPessoasContatosSqlRepository, PessoasContatosSqlRepository>();
            services.AddScoped<IPessoasEnderecosSqlRepository, PessoasEnderecosSqlRepository>();
            services.AddScoped<IPessoasFisicasSqlRepository, PessoasFisicasSqlRepository>();
            services.AddScoped<IPessoasJuridicasSqlRepository, PessoasJuridicasSqlRepository>();
            services.AddScoped<IConfiguracoesSqlRepository, ConfiguracoesSqlRepository>();
            services.AddScoped<IUsuariosContatosSqlRepository, UsuariosContatosSqlRepository>();
            services.AddScoped<IProcuracoesPartesSqlRepository, ProcuracoesPartesSqlRepository>();
            services.AddScoped<IProcuracoesPartesEstadosSqlRepository, ProcuracoesPartesEstadosSqlRepository>();
            services.AddScoped<IMatrimoniosSqlRepository, MatrimoniosSqlRepository>();
            services.AddScoped<IMatrimoniosDocumentosSqlRepository, MatrimoniosDocumentosSqlRepository>();
            services.AddScoped<ILogSistemaSqlRepository, LogSistemaSqlRepository>();
            #endregion

            #region APIs

            services.AddPolicies(configuration);

            #region API Identity
            services.Configure<SettingsIdentity>(configuration.GetSection("SettingsIdentity"));

            services.AddHttpClient<ApiIdentity, SettingsIdentity>(configuration, nameof(SettingsIdentity));
            #endregion

            #region API Correios
            services.Configure<SettingsCorreios>(configuration.GetSection("SettingsCorreios"));

            services.AddHttpClient<ApiCorreios, SettingsCorreios>(configuration, nameof(SettingsCorreios));
            #endregion

            #region API Pagamento
            services.Configure<SettingsPagamento>(configuration.GetSection("SettingsPagamento"));

            services.AddHttpClient<ApiPagamento, SettingsPagamento>(configuration, nameof(SettingsPagamento));
            #endregion

            #region API Comunicacao
            services.Configure<SettingsSms>(configuration.GetSection("SettingsSms"));
            services.AddHttpClient<ApiSMS, SettingsSms>(configuration, nameof(SettingsSms));

            services.AddTransient<IComunicadorTemplate, ComunicadorTemplate>();
            services.AddTransient<ITemplateReader, TemplateReader>();
            #endregion

            #region Certificado Digital
            services.AddTransient<IRepresentacaoVisual, RepresentacaoVisual>();
            services.AddTransient<IPadesPolicy, PadesPolicy>();
            #endregion

            #region WebServer Email Tracker
            services.Configure<SettingsEmail>(configuration.GetSection("SettingsEmail"));
            services.AddTransient<IEmailWebServer, EmailWebServer>();
            #endregion

            #endregion

            return services;
        }
    }
}