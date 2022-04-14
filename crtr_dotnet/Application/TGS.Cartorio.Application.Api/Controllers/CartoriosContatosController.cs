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
    public class CartoriosContatosController : Controller
    {
        private readonly ICartoriosContatosAppService _cartoriocontatoAppService;
        private readonly ILogger<CartoriosContatosController> _logger;

        public CartoriosContatosController(ILogger<CartoriosContatosController> logger, ICartoriosContatosAppService cartoriocontatoAppService)
        {
            _logger = logger;
            _cartoriocontatoAppService = cartoriocontatoAppService;
        }

//        [HttpGet("BuscarId/{id:int}")]
//        public async Task<IActionResult> BuscarId(int id)
//        {
//            try
//            {
//                var cartorioscontatos = await _cartoriocontatoAppService.BuscarId(id);

//                return Ok(cartorioscontatos);
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
//                var cartorioscontatos = await _cartoriocontatoAppService.BuscarTodos(pagina);

//                return Ok(cartorioscontatos);
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
//                var cartorioscontatos = await _cartoriocontatoAppService.BuscarTodosComNoLock(pagina);
//                return Ok(cartorioscontatos);
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


//        [HttpPost("IncluirCartorioContato")]
//        public async Task<IActionResult> IncluirCartorioContato([FromBody] CartoriosContatos cartoriocontatos)
//        {
//            try
//            {
//                await _cartoriocontatoAppService.Incluir(cartoriocontatos);
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

//        [HttpPost("AtualizarCartorioContato")]
//        public async Task<IActionResult> AtualizarCartorioContato([FromBody] CartoriosContatos cartoriocontatos)
//        {
//            try
//            {
//                await _cartoriocontatoAppService.Atualizar(cartoriocontatos);
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
