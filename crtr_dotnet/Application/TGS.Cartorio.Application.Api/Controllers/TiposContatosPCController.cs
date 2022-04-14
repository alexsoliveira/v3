using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TGS.Cartorio.Application.AppServices.Interfaces;

namespace TGS.Cartorio.Application.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class TiposContatosPcController : Controller
    {
        private readonly ITiposContatosPcAppService _tiposContatosPcAppService;
        private readonly ILogger<TiposContatosPcController> _logger;

        public TiposContatosPcController(ILogger<TiposContatosPcController> logger, ITiposContatosPcAppService tiposContatosPcAppService)
        {
            _logger = logger;
            _tiposContatosPcAppService = tiposContatosPcAppService;
        }
    }
}
