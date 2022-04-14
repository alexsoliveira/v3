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
    public class ContatosController : ControllerBase
    {
        private readonly IContatosAppService _contatoAppService;
        private readonly ILogger<ContatosController> _logger;

        public ContatosController(ILogger<ContatosController> logger, IContatosAppService contatoAppService)
        {
            _logger = logger;
            _contatoAppService = contatoAppService;
        }
    }
}
