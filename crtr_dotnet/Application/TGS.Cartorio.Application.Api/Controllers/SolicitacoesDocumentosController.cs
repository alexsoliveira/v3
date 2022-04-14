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
    public class SolicitacoesDocumentosController : Controller
    {
        private readonly ISolicitacoesDocumentosAppService _solicitacoesDocumentosAppService;
        private readonly ILogger<SolicitacoesDocumentosController> _logger;

        public SolicitacoesDocumentosController(ILogger<SolicitacoesDocumentosController> logger, ISolicitacoesDocumentosAppService solicitacoesDocumentosAppService)
        {
            _logger = logger;
            _solicitacoesDocumentosAppService = solicitacoesDocumentosAppService;
        }
    }
}
