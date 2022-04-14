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
    public class CartoriosModalidadesPcController : Controller
    {
        private readonly ICartoriosModalidadesPcAppService _cartoriosModalidadesPcAppService;
        private readonly ILogger<CartoriosModalidadesPcController> _logger;

        public CartoriosModalidadesPcController(ILogger<CartoriosModalidadesPcController> logger, ICartoriosModalidadesPcAppService cartoriosModalidadesPcAppService)
        {
            _logger = logger;
            _cartoriosModalidadesPcAppService = cartoriosModalidadesPcAppService;
        }

//        [HttpGet("BuscarTodos")]
//        public async Task<IActionResult> BuscarTodos(int pagina = 0)
//        {
//            try
//            {
//                var CartoriosModalidadesPc = await _cartoriosModalidadesPcAppService.BuscarTodos(pagina);

//                return Ok(CartoriosModalidadesPc);
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
//                var CartoriosModalidadesPc = await _cartoriosModalidadesPcAppService.BuscarTodosComNoLock(pagina);
//                return Ok(CartoriosModalidadesPc);
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
