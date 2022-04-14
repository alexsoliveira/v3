using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Application.DTO;
using TGS.Cartorio.Infrastructure.Utility.Others;
using TGS.Cartorio.Infrastructure.Utility.WebServerContracts.Interfaces;

namespace TGS.Cartorio.Application.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailWebServer _emailContract;
        private readonly ILogSistemaAppService _logSistemaAppService;
        private readonly IWebHostEnvironment _env;

        public EmailController(IEmailWebServer emailContract, 
            IWebHostEnvironment env, 
            ILogSistemaAppService logSistemaAppService)
        {
            _emailContract = emailContract;
            _env = env;
            _logSistemaAppService = logSistemaAppService;
        }

        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendEmail(DadosEnvioEmail dadosEnvioEmail)
        {
            try
            {
                var ret = await _emailContract.EnviarMensagem(dadosEnvioEmail);

                await _logSistemaAppService.Add(CodLogSistema.EmailController_SendEmail, ret.Log);

                return Ok(ret);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_EmailController_SendEmail, dadosEnvioEmail, ex);

                var innerExceptionMessage = ex.InnerException != null ? ex.InnerException.Message : "";
                var mensagemErro = $"{ex.Message}\n\n{innerExceptionMessage}";
                return StatusCode(500, mensagemErro);
            }
        }
    }
}
