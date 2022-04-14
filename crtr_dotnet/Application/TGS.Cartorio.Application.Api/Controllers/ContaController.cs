using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Application.DTO;
using TGS.Cartorio.Application.ViewModel;
using TGS.Cartorio.Application.ViewModel.Identity;
using TGS.Cartorio.Infrastructure.Utility.ApiClient;

namespace TGS.Cartorio.Application.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContaController : ControllerBase
    {
        private readonly IContaAppService _contaAppService;
        private readonly ILogSistemaAppService _logSistemaAppService;

        public ContaController(
            IContaAppService contaAppService,
            ILogSistemaAppService logSistemaAppService)
        {
            _contaAppService = contaAppService;
            _logSistemaAppService = logSistemaAppService;
        }

        [HttpPost("Cadastrar")]
        public async Task<IActionResult> Cadastrar([FromBody] UsuarioRegistro obj)
        {
            try
            {
                var res = await _contaAppService.Cadastrar(obj);
                await _logSistemaAppService.Add(CodLogSistema.ContaController_Cadastrar,
                    new
                    {
                        Sucesso = res.Sucesso,
                        Nome = obj.Nome,
                        Email = obj.Email,
                        RG = obj.RG                        
                    });
                if (res.Sucesso)
                    return Ok(true);

                return StatusCode(500, $"Ocorreu um erro ao cadastrar usuário:\n\n{JsonConvert.SerializeObject(res)}\n\nObjeto enviado:\n\n{JsonConvert.SerializeObject(obj)}");
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_ContaController_Cadastrar,
                                    new
                                    {
                                        Sucesso = false,
                                        Nome = obj.Nome,
                                        Email = obj.Email,
                                        RG = obj.RG                                        
                                    }, ex);

                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);                
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] UsuarioLogin login)
        {
            try
            {
                var dados = await _contaAppService.Login(login);
                await _logSistemaAppService.Add(CodLogSistema.ContaController_Login,
                    new
                    {
                        Sucesso = true,                        
                        Email = login.Email,
                        AccessToken = dados.AccessToken,
                        ContaAtivada = dados.ContaAtivada,
                        ExpiresIn = dados.ExpiresIn,
                        UsuarioToken = dados.UsuarioToken,                        
                    });
                return Ok(dados);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_ContaController_Login,
                                    new
                                    {
                                        Sucesso = false,
                                        Email = login.Email,
                                    }, ex);

                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }

        [HttpPost("EnviarEmailAtivacao")]
        public async Task<IActionResult> EnviarEmailAtivacao([FromBody] UsuarioConta obj)
        {
            try
            {

                var res = await _contaAppService.EnviarEmailAtivacao(obj);
                await _logSistemaAppService.Add(CodLogSistema.ContaController_EnviarEmailAtivacao,
                    new
                    {
                        Sucesso = res.Sucesso,
                        Email = obj.Email,                                                                        
                    });
                if (res.Sucesso)
                    return Ok(res);

#if Debug
                return StatusCode(500, $"Ocorreu um erro ao enviar e-mail:\n\n{JsonConvert.SerializeObject(res)}\n\nObjeto enviado:\n\n{JsonConvert.SerializeObject(obj)}");
#else
                return StatusCode(500, $"Ocorreu um erro ao enviar e-mail!");
#endif
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_ContaController_EnviarEmailAtivacao,
                                    new
                                    {
                                        Sucesso = false,                                        
                                        Email = obj.Email,                                        
                                    }, ex);

                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }

        [HttpPost("ConfirmarEmailAtivacao")]
        public async Task<IActionResult> ConfirmarEmailAtivacao([FromBody] UsuarioConfirmaConta obj)
        {
            try
            {
                var res = await _contaAppService.ConfirmarEmailAtivacao(obj);
                await _logSistemaAppService.Add(CodLogSistema.ContaController_ConfirmarEmailAtivacao,
                    new
                    {
                        Sucesso = true,
                        UserId = obj.UserId,
                        Code = obj.Code,                        
                    });

                if (res)
                    return Ok(res);

                else
                    return StatusCode(500, "Ocorreu um erro ao tentar ativar a conta!");
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_ContaController_ConfirmarEmailAtivacao,
                                    new
                                    {
                                        Sucesso = false,
                                        UserId = obj.UserId,
                                        Code = obj.Code,
                                    }, ex);

                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }

        [HttpPost("EnviarEmailResetSenha")]
        public async Task<IActionResult> EnviarEmailResetSenha([FromBody] UsuarioConta obj)
        {
            try
            {
                var res = await _contaAppService.EnviarEmailResetSenha(obj);
                await _logSistemaAppService.Add(CodLogSistema.ContaController_EnviarEmailResetSenha,
                    new
                    {
                        Sucesso = true,
                        Email = obj.Email,                                                
                    });
                return Ok(res);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_ContaController_EnviarEmailResetSenha,
                                    new
                                    {
                                        Sucesso = false,
                                        Email = obj.Email,
                                    }, ex);

                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }

        [HttpPost("ResetarSenha")]
        public async Task<IActionResult> ResetarSenha([FromBody] UsuarioResetSenha obj)
        {
            try
            {
                var res = await _contaAppService.ResetarSenha(obj);
                await _logSistemaAppService.Add(CodLogSistema.ContaController_ResetarSenha,
                        new
                        {
                            Sucesso = true,
                            UserId = obj.UserId,
                            Code = obj.Code,                        
                    });
                return Ok(res);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_ContaController_ResetarSenha,
                                    new
                                    {
                                        Sucesso = false,
                                        UserId = obj.UserId,
                                        Code = obj.Code,
                                    }, ex);

                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }

        [Authorize]
        [HttpPost("AlterarSenha")]
        public async Task<IActionResult> AlterarSenha([FromBody] UsuarioAlterarSenha obj)
        {
            try
            {
                var res = await _contaAppService.AlterarSenha(obj);
                await _logSistemaAppService.Add(CodLogSistema.ContaController_AlterarSenha,
                        new
                        {
                            Sucesso = true,
                            UserId = obj.UserId,
                        });
                return Ok(res);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_ContaController_AlterarSenha,
                                    new
                                    {
                                        Sucesso = false,
                                        UserId = obj.UserId,                                        
                                    }, ex);

                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }

        [Authorize]
        [HttpGet("BuscarUsuarioId/{usuarioId:int}")]
        public async Task<IActionResult> BuscarUsuarioId(int usuarioId)
        {
            try
            {
                var usuario = await _contaAppService.BuscarDadosConta(usuarioId);
                await _logSistemaAppService.Add(CodLogSistema.ContaController_BuscarDadosConta,
                        new
                        {
                            Sucesso = true,
                            UsuarioId = usuarioId,                            
                        });
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_ContaController_BuscarDadosConta,
                                                    new
                                                    {
                                                        Sucesso = false,
                                                        UsuarioId = usuarioId,
                                                    }, ex);

                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }

        [Authorize]
        [HttpGet("BuscarDadosUsuario/{id:int}")]
        public async Task<IActionResult> BuscarDadosUsuario(int id)
        {
            try
            {
                var usuario = await _contaAppService.BuscarDadosUsuario(id);
                await _logSistemaAppService.Add(CodLogSistema.ContaController_BuscarDadosUsuario,
                        new
                        {
                            Sucesso = true,
                            UsuarioId = id,
                            Email = usuario.Email,
                            Nome = usuario.Nome,
                            RG = usuario.RG,
                            Documento = usuario.Documento                            
                        });
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_ContaController_BuscarDadosUsuario,
                                                              new
                                                              {
                                                                  Sucesso = false,
                                                                  UsuarioId = id,                                                                  
                                                              }, ex);

                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }

        [Authorize]
        [HttpPost("SalvarDadosPessoais")]
        public async Task<IActionResult> SalvarDadosPessoais([FromBody] UsuarioDadosPessoaisViewModel obj)
        {
            try
            {
                var res = await _contaAppService.SalvarDadosPessoais(obj);
                await _logSistemaAppService.Add(CodLogSistema.ContaController_SalvarDadosPessoais,
                        new
                        {
                            Sucesso = true,
                            IdUsuario = obj.IdUsuario,
                            IdPessoa = obj.IdPessoa,
                            NomeSocial = obj.NomeSocial,
                            Nascimento = obj.Nascimento,
                            Nacionalidade = obj.Nacionalidade,
                            Profissao = obj.Profissao,
                            EstadoCivil = obj.EstadoCivil,
                            Contato = obj.Contato,                                                        
                        });
                return Ok(res);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_ContaController_SalvarDadosPessoais,
                                                              new
                                                              {
                                                                  Sucesso = false,
                                                                  IdUsuario = obj.IdUsuario,
                                                                  IdPessoa = obj.IdPessoa,
                                                                  NomeSocial = obj.NomeSocial,
                                                                  Nascimento = obj.Nascimento,
                                                                  Nacionalidade = obj.Nacionalidade,
                                                                  Profissao = obj.Profissao,
                                                                  EstadoCivil = obj.EstadoCivil,
                                                                  Contato = obj.Contato,
                                                              }, ex);

                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }

        [Authorize]
        [HttpGet("BuscarDadosUsuarioSolicitante/{id:int}/{idTipoDocumento:int}/{documento:long}")]
        public async Task<IActionResult> BuscarDadosUsuarioSolicitante(int idTipoDocumento, long documento, int id)
        {
            try
            {
                var usuario = await _contaAppService.BuscarDadosUsuarioSolicitante(idTipoDocumento, documento, id);
                if (usuario == null)
                    return NotFound();
                await _logSistemaAppService.Add(CodLogSistema.ContaController_BuscarDadosUsuarioSolicitante,
                        new
                        {
                            Sucesso = true,
                            IdTipoDocumento = idTipoDocumento,
                            Documento = documento,
                            IdUsuario = id,
                            usuario.NomeUsuario,
                            usuario.Email,
                            usuario.IdPessoaSolicitante,
                            usuario.Rg                            
                        });

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_ContaController_BuscarDadosUsuarioSolicitante,
                                                                              new
                                                                              {
                                                                                  Sucesso = false,
                                                                                  IdTipoDocumento = idTipoDocumento,
                                                                                  Documento = documento,
                                                                                  IdUsuario = id,                                                                                  
                                                                              }, ex);

                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }

        [Authorize]
        [HttpGet("EmailPossuiConta")]
        public async Task<IActionResult> EmailPossuiConta(string email)
        {
            try
            {
                var valido = await _contaAppService.ValidarDocumentoComEmail(email);
                await _logSistemaAppService.Add(CodLogSistema.ContaController_ValidarDocumentoComEmail,
                        new
                        {
                            Sucesso = true,
                            Email = email,
                            Valido = valido,                            
                        });
                return Ok(valido);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_ContaController_ValidarDocumentoComEmail,
                                                                                              new
                                                                                              {
                                                                                                  Sucesso = false,
                                                                                                  Email = email,
                                                                                              }, ex);

                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }

        [Authorize]
        [HttpGet("EmailPertenceAoDocumento/{documento:long}")]
        public async Task<IActionResult> EmailPertenceAoDocumento(long documento, string email)
        {
            try
            {
                if (documento == 0 || string.IsNullOrEmpty(email) || !email.Contains("@"))
                    return BadRequest("É obrigatório o documento e email!");

                var valido = await _contaAppService.ValidarEmailPertenceAoDocumento(documento, email);
                await _logSistemaAppService.Add(CodLogSistema.ContaController_ValidarEmailPertenceAoDocumento,
                        new
                        {
                            Sucesso = true,
                            Documento = documento,
                            Email = email,
                            Valido = valido,                            
                        });
                return Ok(valido);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_ContaController_ValidarEmailPertenceAoDocumento,
                                                new
                                                {
                                                    Sucesso = false,
                                                    Documento = documento,
                                                    Email = email,
                                                }, ex);

                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }

        [Authorize]
        [HttpGet("VerificarDadosSolicitanteAtualizados/{idUsuario:int}")]
        public async Task<IActionResult> VerificarDadosSolicitanteAtualizados(int idUsuario)
        {
            try
            {
                var usuario = await _contaAppService.BuscarDadosUsuario(idUsuario);
                if (usuario == null)
                    return NotFound();

                await _logSistemaAppService.Add(CodLogSistema.ContaController_VerificarDadosSolicitanteAtualizados_BuscarDadosUsuario,
                        new
                        {
                            Sucesso = true,                            
                            IdUsuario = idUsuario,
                            NomeSocial = usuario.NomeSocial,
                            Nascimento = usuario.Nascimento,
                            EstadoCivil = usuario.EstadoCivil,
                            Nacionalidade = usuario.Nacionalidade,
                            Profissao = usuario.Profissao,
                            Contatos = usuario.Contatos,
                            Enderecos = usuario.Enderecos,
                        });

                bool estaAtualizado = !string.IsNullOrEmpty(usuario.NomeSocial)
                                    && usuario.Nascimento.HasValue
                                    && usuario.EstadoCivil.HasValue
                                    && !string.IsNullOrEmpty(usuario.Nacionalidade)
                                    && !string.IsNullOrEmpty(usuario.Profissao);

                bool possuiContato = usuario.Contatos != null 
                    && usuario.Contatos.Any(x => !string.IsNullOrEmpty(x.Celular));
                bool possuiEndereco = usuario.Enderecos != null 
                    && usuario.Enderecos.Any(x => x.Conteudo != null);

                return Ok(estaAtualizado && possuiContato && possuiEndereco);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_ContaController_VerificarDadosSolicitanteAtualizados_BuscarDadosUsuario,
                                                new
                                                {
                                                    Sucesso = false,
                                                    IdUsuario = idUsuario
                                                }, ex);

                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}