using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Application.DTO;

namespace TGS.Cartorio.Application.Api.Controllers
{
    [Authorize]
    [ApiController]    
    [Route("[controller]")]
    public class CarrinhoController : Controller
    {
        private readonly ICarrinhoAppService _carrinhoAppService;
        private readonly ILogSistemaAppService _logSistemaAppService;

        public CarrinhoController(
            ICarrinhoAppService carrinhoAppService
            , ILogSistemaAppService logSistemaAppService)
        {
            _carrinhoAppService = carrinhoAppService;
            _logSistemaAppService = logSistemaAppService;
        }

        [HttpGet("BuscarSolicitante/{id:long}")]
        public async Task<IActionResult> BuscarSolicitante(long id)
        {
            try
            {
                var usuario = await _carrinhoAppService.BuscarSolicitante(id);
                await _logSistemaAppService.Add(CodLogSistema.CarrinhoController_BuscarSolicitante,
                    new
                    {
                        Sucesso = true,
                        IdUsuario = usuario.IdUsuario,
                        IdPessoa = usuario.IdPessoa
                    });
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_CarrinhoController_BuscarSolicitante,
                                    new
                                    {
                                        Sucesso = false,
                                        IdSolicitacao = id,                                        
                                    }, ex);

                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }

        [HttpGet("ObterParticipantes/{idSolicitacao:long}")]
        public async Task<IActionResult> ObterParticipantes(long idSolicitacao)
        {
            try
            {
                var participantes = await _carrinhoAppService.ObterParticipantes(idSolicitacao);
                if (participantes == null || participantes.Count() == 0)
                    return NotFound();

                await _logSistemaAppService.Add(CodLogSistema.CarrinhoController_ObterParticipantes,
                   new
                   {
                       Sucesso = true,
                       IdSolicitacao = idSolicitacao
                   });

                return Ok(participantes);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_CarrinhoController_ObterParticipantes,
                                                    new
                                                    {
                                                        Sucesso = false,
                                                        IdSolicitacao = idSolicitacao,
                                                    }, ex);

                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }

        [HttpGet("ObterProduto/{id:long}")]
        public async Task<IActionResult> ObterProduto(long id)
        {
            try
            {
                var produto = await _carrinhoAppService.ObterProduto(id);
                if (produto == null)
                    return NotFound();
                await _logSistemaAppService.Add(CodLogSistema.CarrinhoController_ObterProduto,
                   new
                   {
                       Sucesso = true,
                       IdSolicitacao = id
                   });
                return Ok(produto);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_CarrinhoController_ObterProduto,
                                                                    new
                                                                    {
                                                                        Sucesso = false,
                                                                        IdSolicitacao = id,
                                                                    }, ex);

                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }

        [HttpGet("ObterComposicaoPrecos/{id:long}")]
        public async Task<IActionResult> ObterComposicaoPrecos(long id) 
        {
            try
            {
                var composicaoPrecos = await _carrinhoAppService.ObterComposicaoPrecos(id);
                if (composicaoPrecos == null)
                    return NotFound();
                await _logSistemaAppService.Add(CodLogSistema.CarrinhoController_ObterComposicaoPrecos,
                   new
                   {
                       Sucesso = true,
                       IdSolicitacao = id
                   });
                return Ok(composicaoPrecos);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_CarrinhoController_ObterComposicaoPrecos,
                                                                    new
                                                                    {
                                                                        Sucesso = false,
                                                                        IdSolicitacao = id,
                                                                    }, ex);

                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("ObterTermoConcordancia")]
        public async Task<IActionResult> ObterTermoConcordancia(string descricao)
        {
            try
            {                
                var termo = await _carrinhoAppService.ObterTermoConcordancia(descricao);
                if (termo == null)
                    return NotFound();
                await _logSistemaAppService.Add(CodLogSistema.CarrinhoController_ObterTermoConcordancia,
                   new
                   {
                       Sucesso = true,
                       Descricao = termo.Descricao
                   });
                return Ok(termo);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_CarrinhoController_ObterTermoConcordancia,
                                                                                    new
                                                                                    {
                                                                                        Sucesso = false,
                                                                                        Descricao = descricao,
                                                                                    }, ex);

                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }

        [HttpPost("AceiteTermoConcordancia")]
        public async Task<IActionResult> AceiteTermoConcordancia(long id, bool isTermoAceito)
        {
            try
            {
                await _carrinhoAppService.AceiteTermoConcordancia(id, isTermoAceito);
                await _logSistemaAppService.Add(CodLogSistema.CarrinhoController_AceiteTermoConcordancia,
                                                                                                    new
                                                                                                    {
                                                                                                        Sucesso = true,
                                                                                                        IdSolicitacao = id,
                                                                                                        IsTermoAceito = isTermoAceito,
                                                                                                    });
                return Ok();
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_CarrinhoController_AceiteTermoConcordancia,
                                                                                                    new
                                                                                                    {
                                                                                                        Sucesso = false,
                                                                                                        IdSolicitacao = id,
                                                                                                        IsTermoAceito = isTermoAceito,
                                                                                                    }, ex);

                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}
