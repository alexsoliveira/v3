using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TGS.Cartorio.Application.Api.Controllers.Base;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Application.DTO;

namespace TGS.Cartorio.Application.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class PessoasController : MainController
    {
        private readonly IPessoasAppService _pessoaAppService;
        private readonly ILogSistemaAppService _logSistemaAppService;

        public PessoasController(ILogger<PessoasController> logger,
                               IPessoasAppService pessoaAppService, 
                               ILogSistemaAppService logSistemaAppService)
        {
            _pessoaAppService = pessoaAppService;
            _logSistemaAppService = logSistemaAppService;
        }

        [HttpGet("PessoaExiste/{idTipoDocumento:int}/{documento:long}")]
        public async Task<IActionResult> PessoaExiste(int idTipoDocumento, long documento)
        {
            try
            {
                long? idPessoa = await _pessoaAppService.PessoaExiste(idTipoDocumento, documento);

                if (idPessoa == null)
                    return NotFound();

                return Ok(idPessoa);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_PessoasController_PessoaExiste, new
                {
                    IdTipoDocumento = idTipoDocumento,
                    Documento = documento
                }, ex);
                return InternalServerError("Ocorreu um erro ao buscar o status da solicitação.", ex);
            }
        }
    }
}