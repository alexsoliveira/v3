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
    public class PessoasEnderecosController : ControllerBase
    {
        private readonly IPessoasEnderecosAppService _pessoaEnderecoAppService;
        private readonly ILogger<PessoasEnderecosController> _logger;

        public PessoasEnderecosController(ILogger<PessoasEnderecosController> logger,
                               IPessoasEnderecosAppService pessoaEnderecoAppService)
        {
            _logger = logger;
            _pessoaEnderecoAppService = pessoaEnderecoAppService;
        }
    }
}
