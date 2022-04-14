using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Application.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class CartoriosEstadosPcController : Controller
    {
        private readonly ICartoriosEstadosPcAppService _cartoriosEstadosPcAppService;
        private readonly ILogger<CartoriosEstadosPcController> _logger;

        public CartoriosEstadosPcController(ILogger<CartoriosEstadosPcController> logger, ICartoriosEstadosPcAppService cartoriosEstadosPcAppService)
        {
            _logger = logger;
            _cartoriosEstadosPcAppService = cartoriosEstadosPcAppService;
        }

//        [HttpGet("BuscarTodos")]
//        public async Task<IActionResult> BuscarTodos(int pagina = 0)
//        {
//            try
//            {
//                var cartoriosestadospc = await _cartoriosEstadosPcAppService.BuscarTodos(pagina);

//                return Ok(cartoriosestadospc);
//            }
//            catch (Exception ex)
//            {
//#if DEBUG
//                return BadRequest(ex.InnerException?.Message ?? ex.Message);
//#else
//                return BadRequest("Não foi possível retornar os dados.");
//#endif
//            }
//        }

//        [HttpGet("BuscarTodosComNoLock")]
//        public async Task<IActionResult> BuscarTodosComNoLock(int pagina = 0)
//        {
//            try
//            {
//                var cartoriosestadospc = await _cartoriosEstadosPcAppService.BuscarTodosComNoLock(pagina);
//                return Ok(cartoriosestadospc);
//            }
//            catch (Exception ex)
//            {
//#if DEBUG
//                return BadRequest(ex.InnerException?.Message ?? ex.Message);
//#else
//                return BadRequest("Não foi possível retornar os dados.");
//#endif
//            }
//        }

    }
}
