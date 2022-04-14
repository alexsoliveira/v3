using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TGS.Cartorio.Application.AppServices.Interfaces;

namespace TGS.Cartorio.Application.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class PessoasJuridicasController : ControllerBase
    {
        private readonly IPessoasJuridicasAppService _pessoaJuridicaAppService;
        private readonly ILogger<PessoasJuridicasController> _logger;

        public PessoasJuridicasController(ILogger<PessoasJuridicasController> logger,
                               IPessoasJuridicasAppService pessoaJuridicaAppService)
        {
            _logger = logger;
            _pessoaJuridicaAppService = pessoaJuridicaAppService;
        }
    }
}
