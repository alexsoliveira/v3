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
    public class CartoriosEnderecosController : Controller
    {
        private readonly ICartoriosEnderecosAppService _cartorioenderecoAppService;
        private readonly ILogger<CartoriosEnderecosController> _logger;

        public CartoriosEnderecosController(ILogger<CartoriosEnderecosController> logger, ICartoriosEnderecosAppService cartorioenderecoAppService)
        {
            _logger = logger;
            _cartorioenderecoAppService = cartorioenderecoAppService;
        }

//        [HttpGet("BuscarId/{id:int}")]
//        public async Task<IActionResult> BuscarId(int id)
//        {
//            try
//            {
//                var cartoriosenderecos = await _cartorioenderecoAppService.BuscarId(id);

//                return Ok(cartoriosenderecos);
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
//                var cartoriosenderecos = await _cartorioenderecoAppService.BuscarTodos(pagina);

//                return Ok(cartoriosenderecos);
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
//                var cartoriosenderecos = await _cartorioenderecoAppService.BuscarTodosComNoLock(pagina);
//                return Ok(cartoriosenderecos);
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

//        [HttpPost("AtualizarCartorioEndereco")]
//        public async Task<IActionResult> AtualizarCartorioEndereco([FromBody] CartoriosEnderecos cartorioendereco)
//        {
//            try
//            {
//                await _cartorioenderecoAppService.Atualizar(cartorioendereco);
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






//        [HttpPost("IncluirCartorioEndereco")]
//        public async Task<IActionResult> IncluirCartorioEndereco([FromBody] CartoriosEnderecos cartorioendereco)
//        {
//            try
//            {
//                await _cartorioenderecoAppService.Incluir(cartorioendereco);
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
