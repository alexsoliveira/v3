using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Application.DTO;

namespace TGS.Cartorio.Application.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutosAppService _produtoAppService;
        private readonly ILogger<ProdutosController> _logger;
        private readonly IMatrimoniosAppService _matrimonioAppService;
        private readonly ILogSistemaAppService _logSistemaAppService;

        public ProdutosController(ILogger<ProdutosController> logger,
                               IProdutosAppService produtoAppService,
                               IProdutosModalidadesPcAppService produtoModalidadesPcAppService,
                               IMatrimoniosAppService matrimonioAppService, 
                               ILogSistemaAppService logSistemaAppService)
        {
            _logger = logger;
            _produtoAppService = produtoAppService;
            _matrimonioAppService = matrimonioAppService;
            _logSistemaAppService = logSistemaAppService;
        }

        [HttpGet("BuscarId/{id:int}")]
        public async Task<IActionResult> BuscarId(int id)
        {
            try
            {
                var produtos = await _produtoAppService.BuscarId(id);

                return Ok(produtos);
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

        [HttpGet("BuscarDadosVitrine")]
        public async Task<IActionResult> BuscarDadosVitrine()
        {
            try
            {
                var produtos = await _produtoAppService.BuscarDadosVitrine();
                if (produtos == null)
                    return NotFound();

                return Ok(produtos);
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

        [HttpGet("BuscarDetalhesProduto/{id:int}")]
        public async Task<IActionResult> BuscarDetalhesProduto(int id)
        {
            try
            {
                var detalhesProduto = await _produtoAppService.BuscarDetalhesProduto(id);
                if (detalhesProduto == null)
                    return NotFound();

                return Ok(detalhesProduto);
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

        [HttpGet("BuscarTodos")]
        public async Task<IActionResult> BuscarTodos(int pagina = 0)
        {
            try
            {
                var produtos = await _produtoAppService.BuscarTodos(pagina);

                return Ok(produtos);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_ProdutosController_BuscarTodos,
                    null, ex);

#if DEBUG
                return BadRequest(ex.InnerException?.Message ?? ex.Message);
#else
                return BadRequest("Não foi possível retornar os dados.");
#endif
            }
        }

        [Authorize]
        [HttpGet("BuscarDadosMatrimonio/{idSolicitacao:long}")]
        public async Task<IActionResult> BuscarDadosMatrimonio(long idSolicitacao)
        {
            try
            {
                var conteudo = await _matrimonioAppService.BuscarPorSolicitacao(idSolicitacao);
                if (conteudo == null)
                    return NotFound("Não retornou conteudo!");

                return Ok(conteudo);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}