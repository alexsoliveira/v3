using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TGS.Cartorio.Application.Api.Controllers.Base;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Application.DTO;

namespace TGS.Cartorio.Application.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutosModalidadesPcController : MainController
    {
        private readonly IProdutosModalidadesPcAppService _produtosModalidadesPcAppService;
        private readonly ILogger<ProdutosModalidadesPcController> _logger;
        private readonly ILogSistemaAppService _logSistemaAppService;

        public ProdutosModalidadesPcController(ILogger<ProdutosModalidadesPcController> logger, IProdutosModalidadesPcAppService produtosModalidadesPcAppService, ILogSistemaAppService logSistemaAppService)
        {
            _logger = logger;
            _produtosModalidadesPcAppService = produtosModalidadesPcAppService;
            _logSistemaAppService = logSistemaAppService;
        }

        [HttpGet("BuscarTodos")]
        public async Task<IActionResult> BuscarTodos()
        {
            try
            {
                var ProdutosModalidadesPc = await _produtosModalidadesPcAppService.BuscarTodos();

                return Ok(ProdutosModalidadesPc);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_ProdutosModalidadesPcController_BuscarTodos,
                    "Ocorreu um erro ao buscar ProdutosModalidadesPC!", ex);

                return StatusCode(500, GetMessageError(ex));
            }
        }
    }
}
