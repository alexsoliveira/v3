using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Application.DTO;
using TGS.Cartorio.Application.DTO.Products.Matrimonio;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Services;


namespace TGS.Cartorio.Application.AppServices
{
    public class TaxasAppService : ITaxasAppService
    {
        private readonly ISolicitacoesTaxasService _solicitacoesTaxasService;
        private readonly ISolicitacoesService _solicitacoesService;
        private readonly IConfiguracoesAppService _configuracoesAppService;
        private readonly ILogSistemaAppService _logSistemaAppService;
        private readonly IMapper _mapper;

        public TaxasAppService(ISolicitacoesTaxasService solicitacoesTaxasService,
            IMapper mapper,
            ISolicitacoesService solicitacoesService,
            ILogSistemaAppService logSistemaAppService, IConfiguracoesAppService configuracoesAppService)
        {
            _solicitacoesTaxasService = solicitacoesTaxasService;
            _mapper = mapper;
            _solicitacoesService = solicitacoesService;
            _logSistemaAppService = logSistemaAppService;
            _configuracoesAppService = configuracoesAppService;
        }

        public async Task<ICollection<SolicitacoesTaxasDto>> BuscarTaxasPorSolicitacao(long idSolicitacao)
        {
            try
            {
                ICollection<SolicitacoesTaxasDto> colecaoTaxas = new List<SolicitacoesTaxasDto>();
                var solicitacoesTaxas = await _solicitacoesTaxasService.BuscarPorSolicitacao(idSolicitacao);

                foreach (var taxa in solicitacoesTaxas)
                {

                    var taxaDto = _mapper.Map<SolicitacoesTaxasDto>(taxa);
                    if (taxa.IdCartorioTaxa.HasValue)
                        taxaDto.ValorTaxa = taxa.CartoriosTaxasNavigation?.Valor;
                    else if (taxa.IdTaxaExtra.HasValue)
                        taxaDto.ValorTaxa = taxa.TaxasExtrasNavigation?.Valor;
                    colecaoTaxas.Add(taxaDto);
                }

                return colecaoTaxas;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<ComposicaoProdutoValorTotalDto> BuscarComposicaoPrecoProdutoTotal(long idSolicitacao)
        {
            var composicaoProduto = new ComposicaoProdutoValorTotalDto();
            Solicitacoes solicitacao = null;
            try
            {

                solicitacao = await _solicitacoesService.BuscarId(idSolicitacao);
                if (solicitacao == null
                    || solicitacao.IdProdutoNavigation == null
                    || string.IsNullOrEmpty(solicitacao.IdProdutoNavigation.Campos))
                    return null;

                composicaoProduto.TituloProduto = solicitacao.IdProdutoNavigation.Titulo;
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_TaxasAppService_BuscarComposicaoPrecoProdutoTotal_BuscarSolicitacao,
                    new { IdSolicitacao = idSolicitacao }, ex);
                throw;
            }

            ComposicaoPrecoDTO camposProduto = null;
            try
            {
                camposProduto = JsonConvert.DeserializeObject<ComposicaoPrecoDTO>(solicitacao.IdProdutoNavigation.Campos);
                if (camposProduto != null)
                    composicaoProduto.ValorTotal = Convert.ToDecimal(camposProduto.Custo);

            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_TaxasAppService_BuscarComposicaoPrecoProdutoTotal_DesserializeProduto,
                    new 
                    { 
                        IdSolicitacao = idSolicitacao, 
                        Solicitacao = solicitacao 
                    }, ex);

                throw;
            }

            ICollection<SolicitacoesTaxasDto> taxas = null;
            try
            {
                taxas = await BuscarTaxasPorSolicitacao(idSolicitacao);
                if (taxas != null)
                    foreach (var taxa in taxas)
                        if (taxa.ValorTaxa.HasValue)
                            composicaoProduto.ValorTotal += taxa.ValorTaxa.Value;
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_TaxasAppService_BuscarComposicaoPrecoProdutoTotal_BuscarTaxasPorSolicitacao,
                    new 
                    { 
                        IdSolicitacao = idSolicitacao, 
                        Solicitacao = solicitacao,
                        ComposicaoProduto = composicaoProduto,
                        Taxas = taxas
                    }, ex);
                throw;
            }

            return composicaoProduto;
        }

        public async Task<decimal> BuscarTaxaPorBoleto()
        {
            string descricaoConfiguracao = "TaxaTabelionetPorBoleto";
            Configuracoes config = null;
            bool logGravado = false;
            try
            {
                config = await _configuracoesAppService.BuscarPorDescricao(x => x.Descricao == descricaoConfiguracao);
                if (config == null || string.IsNullOrEmpty(config.Conteudo))
                {
                    await _logSistemaAppService.Add(CodLogSistema.Erro_TaxasAppService_BuscarTaxaPorBoleto_ConfiguracaoTaxaTabelionetPorBoletoNaoLocalizada,
                        new
                        {
                            DescricaoConfiguracao = descricaoConfiguracao,
                            Configuracao = config
                        });
                    logGravado = true;
                    throw new Exception("Não foi possível localizar a taxa do boleto!");
                }


                if (decimal.TryParse(config.Conteudo, out decimal valorTaxaBoleto))
                    return valorTaxaBoleto;

                await _logSistemaAppService.Add(CodLogSistema.Erro_TaxasAppService_BuscarTaxaPorBoleto_ErroAoConverterTaxa,
                        new
                        {
                            DescricaoConfiguracao = descricaoConfiguracao,
                            Configuracao = config
                        });
                logGravado = true;
                throw new Exception("Não foi possível converter a taxa do boleto!");
            }
            catch (Exception ex)
            {
                if (!logGravado)
                    await _logSistemaAppService.Add(CodLogSistema.Erro_TaxasAppService_BuscarTaxaPorBoleto_ErroGeral,
                        new
                        {
                            DescricaoConfiguracao = descricaoConfiguracao,
                            Configuracao = config
                        }, ex);

                throw;
            }
        }
    }
}