using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TGS.Cartorio.Application.AppServices.Interfaces;

namespace TGS.Cartorio.Application.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class TiposFretesPCController : Controller
    {
		private readonly ITiposFretesPCAppService _tiposFretesPCAppService;
        private readonly ILogger<TiposFretesPCController> _logger;

        public TiposFretesPCController(ILogger<TiposFretesPCController> logger, ITiposFretesPCAppService tiposFretesPCAppService)
        {
            _logger = logger;
            _tiposFretesPCAppService = tiposFretesPCAppService;
        }
    }
}
