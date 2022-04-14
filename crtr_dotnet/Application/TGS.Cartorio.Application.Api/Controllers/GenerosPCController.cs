using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Application.DTO;

namespace TGS.Cartorio.Application.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class GenerosPcController : Controller
    {
        private readonly IGenerosPcAppService _generosPcAppService;
        private readonly ILogSistemaAppService _logSistemaAppService;

        public GenerosPcController(IGenerosPcAppService generosPcAppService, ILogSistemaAppService logSistemaAppService)
        {
            _generosPcAppService = generosPcAppService;
            _logSistemaAppService = logSistemaAppService;
        }

        [AllowAnonymous]
        [HttpGet("BuscarTodos")]
        public async Task<IActionResult> BuscarTodos()
        {
            try
            {
                var generos = await _generosPcAppService.BuscarTodos();
                if (generos == null || generos.Count == 0)
                    return NotFound();

                return Ok(generos);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_GenerosPCController_BuscarTodos,
                                    new
                                    {
                                        Sucesso = false,                                        
                                    }, ex);

                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}
