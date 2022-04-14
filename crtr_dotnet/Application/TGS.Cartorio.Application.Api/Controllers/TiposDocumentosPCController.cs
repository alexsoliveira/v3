using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TGS.Cartorio.Application.AppServices.Interfaces;

namespace TGS.Cartorio.Application.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class TiposDocumentosPcController : Controller
    {
        private readonly ITiposDocumentosPcAppService _tiposDocumentosPcAppService;
        private readonly ILogger<TiposDocumentosPcController> _logger;

        public TiposDocumentosPcController(ILogger<TiposDocumentosPcController> logger, ITiposDocumentosPcAppService tiposDocumentosPcAppService)
        {
            _logger = logger;
            _tiposDocumentosPcAppService = tiposDocumentosPcAppService;
        }
    }
}
