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
    public class CartoriosController : Controller
    {
        private readonly ICartoriosAppService _cartorioAppService;
        private readonly ILogger<CartoriosController> _logger;

        public CartoriosController(ILogger<CartoriosController> logger, ICartoriosAppService cartorioAppService)
        {
            _logger = logger;
            _cartorioAppService = cartorioAppService;
        }

//        [HttpGet("BuscarId/{id:int}")]
//        public async Task<IActionResult> BuscarId(int id)
//        {
//            try
//            {
//                return Ok(await _cartorioAppService.BuscarId(id));
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

//        [HttpGet("BuscarTodos")]
//        public async Task<IActionResult> BuscarTodos(int pagina = 0)
//        {
//            try
//            {
//                var cartorios = await _cartorioAppService.BuscarTodos(pagina);

//                return Ok(cartorios);
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
//                var cartorios = await _cartorioAppService.BuscarTodosComNoLock(pagina);
//                return Ok(cartorios);
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


//        [HttpPost("IncluirCartorio")]
//        public async Task<IActionResult> IncluirCartorio([FromBody] Cartorios cartorio)
//        {
//            try
//            {
//                await _cartorioAppService.Incluir(cartorio);
//                return Ok();
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


//        [HttpPost("AtualizarCartorio")]
//        public async Task<IActionResult> AtualizarCartorio([FromBody] Cartorios cartorio)
//        {
//            try
//            {
//                await _cartorioAppService.Atualizar(cartorio);
//                return Ok();
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
