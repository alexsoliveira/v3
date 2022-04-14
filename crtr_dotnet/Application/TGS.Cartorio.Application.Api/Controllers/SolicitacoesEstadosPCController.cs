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
    public class SolicitacoesEstadosPcController : Controller
    {
        private readonly ISolicitacoesEstadosPcAppService _solicitacoesEstadosPcAppService;
        private readonly ILogger<SolicitacoesEstadosPcController> _logger;

        public SolicitacoesEstadosPcController(ILogger<SolicitacoesEstadosPcController> logger, ISolicitacoesEstadosPcAppService solicitacoesEstadosPcAppService)
        {
            _logger = logger;
            _solicitacoesEstadosPcAppService = solicitacoesEstadosPcAppService;
        }
    }
}
