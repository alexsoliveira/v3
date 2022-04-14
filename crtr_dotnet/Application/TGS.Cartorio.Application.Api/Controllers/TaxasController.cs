using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using TGS.Cartorio.Application.Api.Controllers.Base;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Application.DTO;

namespace TGS.Cartorio.Application.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class TaxasController : MainController
    {
        private readonly ITaxasAppService _taxasAppService;
        private readonly ILogSistemaAppService _logSistemaAppService;
        public TaxasController(ITaxasAppService taxasAppService, 
            ILogSistemaAppService logSistemaAppService)
        {
            _taxasAppService = taxasAppService;
            _logSistemaAppService = logSistemaAppService;
        }

        [HttpGet("BuscarTaxasPorSolicitacao/{idSolicitacao:long}")]
        public async Task<IActionResult> BuscarTaxasPorSolicitacao(long idSolicitacao)
        {
            try
            {
                var taxas = await _taxasAppService.BuscarTaxasPorSolicitacao(idSolicitacao);
                if (taxas == null || !taxas.Any())
                    return NotFound("Nenhuma taxa foi localizada!");

                return Ok(taxas);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_TaxasController_BuscarTaxasPorSolicitacao,
                new
                {
                    IdSolicitacao = idSolicitacao
                }, ex);

                return InternalServerError($"Ocorreu um erro ao buscar as taxas da Solicitação {idSolicitacao}.", ex);
            }
        }


        [HttpGet("BuscarComposicaoPrecoProdutoTotal/{idSolicitacao:long}")]
        public async Task<IActionResult> BuscarComposicaoPrecoProdutoTotal(long idSolicitacao)
        {
            try
            {
                var composicao = await _taxasAppService.BuscarComposicaoPrecoProdutoTotal(idSolicitacao);
                if (composicao != null 
                    && composicao.ValorTotal.HasValue
                    && !string.IsNullOrEmpty(composicao.TituloProduto))
                    return Ok(composicao);
                else
                    return NotFound($"Não foi possível localizar os dados da Solicitação {idSolicitacao}.");
            }
            catch (Exception ex)
            {
                return InternalServerError($"Ocorreu um erro ao buscar os dados da Solicitação {idSolicitacao}.", ex);
            }
        }
    }
}
