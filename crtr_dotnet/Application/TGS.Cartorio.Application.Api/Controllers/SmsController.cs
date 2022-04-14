using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using TGS.Cartorio.Infrastructure.Utility.ApiClient;

namespace TGS.Cartorio.Application.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class SmsController : ControllerBase
    {
        private readonly ApiSMS _apiComunicacao;
        private readonly IWebHostEnvironment _env;

        public SmsController(ApiSMS apiComunicacao, IWebHostEnvironment env)
        {
            _apiComunicacao = apiComunicacao;
            _env = env;
        }

        //[HttpPost("SendMessage")]
        //public async Task<IActionResult> SendMessage(List<SmsItem> items)
        //{
        //    try
        //    {
        //        var ret = await _apiComunicacao.EnviarMensagem(items, _env.EnvironmentName == "Development");
        //        return Ok(ret);
        //    }
        //    catch (Exception ex)
        //    {
        //        var innerExceptionMessage = ex.InnerException != null ? ex.InnerException.Message : "";
        //        var mensagemErro = $"{ex.Message}\n\n{innerExceptionMessage}";
        //        return StatusCode(500, mensagemErro);
        //    }
        //}
    }
}
