using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TGS.Cartorio.Domain.Interfaces.Services;

namespace TGS.Cartorio.Application.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class FluxoController : ControllerBase
    {
        private readonly ISolicitacoesService _solicitacoesService;

        public FluxoController(ISolicitacoesService solicitacoesService)
        {
            _solicitacoesService = solicitacoesService;
        }
    }
}
