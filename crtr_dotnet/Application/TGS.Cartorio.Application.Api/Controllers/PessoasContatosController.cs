using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TGS.Cartorio.Application.AppServices.Interfaces;
namespace TGS.Cartorio.Application.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class PessoasContatosController : ControllerBase
    {

        private readonly IPessoasContatosAppService _pessoacontatoAppService;
        private readonly ILogger<PessoasContatosController> _logger;

        public PessoasContatosController(ILogger<PessoasContatosController> logger,
                               IPessoasContatosAppService pessoacontatoAppService)
        {
            _logger = logger;
            _pessoacontatoAppService = pessoacontatoAppService;
        }
    }
}
