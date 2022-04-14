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
    public class TiposPartesPCController : Controller
    {
        private readonly ITiposPartesPCAppService _tiposPartesPCAppService;
        private readonly ILogger<TiposPartesPCController> _logger;

        public TiposPartesPCController(ILogger<TiposPartesPCController> logger, ITiposPartesPCAppService tiposPartesPCAppService)
        {
            _logger = logger;
            _tiposPartesPCAppService = tiposPartesPCAppService;
        }
    }
}
