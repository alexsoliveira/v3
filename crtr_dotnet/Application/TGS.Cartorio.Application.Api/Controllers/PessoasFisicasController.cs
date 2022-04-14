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
    public class PessoasFisicasController : ControllerBase
    {

        private readonly IPessoasFisicasAppService _pessoaFisicaAppService;
        private readonly ILogger<PessoasFisicasController> _logger;

        public PessoasFisicasController(ILogger<PessoasFisicasController> logger,
                               IPessoasFisicasAppService pessoaFisicaAppService)
        {
            _logger = logger;
            _pessoaFisicaAppService = pessoaFisicaAppService;
        }
    }
}
