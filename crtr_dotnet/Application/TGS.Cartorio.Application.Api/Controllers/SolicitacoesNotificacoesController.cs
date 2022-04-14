using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TGS.Cartorio.Application.AppServices.Interfaces;

namespace TGS.Cartorio.Application.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class SolicitacoesNotificacoesController : Controller
    {
        private readonly ISolicitacoesNotificacoesAppService _solicitacoesNotificacoesAppService;
        private readonly ILogger<SolicitacoesNotificacoesController> _logger;

        public SolicitacoesNotificacoesController(ILogger<SolicitacoesNotificacoesController> logger, ISolicitacoesNotificacoesAppService solicitacoesNotificacoesAppService)
        {
            _logger = logger;
            _solicitacoesNotificacoesAppService = solicitacoesNotificacoesAppService;
        }
    }
}
