using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TGS.Cartorio.Application.Api.Controllers.Base;
using TGS.Cartorio.Application.AppServices;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Application.DTO;
using TGS.Cartorio.Application.ViewModel;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Services;

namespace TGS.Cartorio.Application.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class EnderecosController : MainController
    {
        private readonly IEnderecosAppService _enderecoAppService;
        private readonly IPessoasEnderecosAppService _pessoasEnderecosAppService;
        private readonly IMapper _mapper;
        private readonly ILogger<EnderecosController> _logger;
        private readonly ISmsAppService _smsAppService;
        private readonly IEmailAppService _emailAppService;
        private readonly IPessoasAppService _pessoaAppService;
        private readonly IUsuariosAppService _usuarioAppService;
        private readonly ILogSistemaAppService _logSistemaAppService;

        public EnderecosController(
            ILogger<EnderecosController> logger,
            IEnderecosAppService enderecoAppService,
            IMapper mapper,
            IPessoasEnderecosAppService pessoasEnderecosAppService,
            ISmsAppService smsAppService,
            IEmailAppService emailAppService,
            IPessoasAppService pessoaAppService,
            IUsuariosAppService usuariosAppService,
            ILogSistemaAppService logSistemaAppService)
        {
            _logger = logger;
            _enderecoAppService = enderecoAppService;
            _mapper = mapper;
            _pessoasEnderecosAppService = pessoasEnderecosAppService;
            _smsAppService = smsAppService;
            _emailAppService = emailAppService;
            _pessoaAppService = pessoaAppService;
            _usuarioAppService = usuariosAppService;
            _logSistemaAppService = logSistemaAppService;
        }

        [HttpPost("Buscar")]
        public async Task<IActionResult> Buscar(string cep)
        {
            try
            {
                var retorno = await _enderecoAppService.Buscar(cep);
                await _logSistemaAppService.Add(CodLogSistema.EnderecosController_Buscar,
                        new
                        {
                            Sucesso = true,
                            CEP = cep,                            
                        });
                if (retorno.Sucesso)
                    return Ok(retorno.ObjRetorno);
                else
                    return StatusCode(500, retorno.MensagemErro);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_EnderecosController_Buscar,
                                    new
                                    {
                                        Sucesso = false,
                                        CEP = cep,
                                    }, ex);

                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }

        [HttpPost("IncluirEndereco")]
        public async Task<IActionResult> IncluirEndereco([FromBody] EnderecosDto endereco)
        {
            try
            {
                int qtdeEnderecos = 0;
                bool naoPossuiEnderecos;
                Enderecos end = _mapper.Map<Enderecos>(endereco);
                try
                {
                    qtdeEnderecos = await _pessoasEnderecosAppService.CountByIdPessoa(endereco.IdPessoa);
                    naoPossuiEnderecos = qtdeEnderecos == 0;
                    await _logSistemaAppService.Add(CodLogSistema.EnderecosController_IncluirEndereco_CountByIdPessoa,
                           new
                           {
                               Sucesso = true,
                               Conteudo = endereco.Conteudo,
                               IdPessoa = endereco.IdPessoa,
                               IdUsuario = endereco.IdUsuario,
                               IdEndereco = endereco.IdEndereco,
                               DataOperacao = endereco.DataOperacao,
                               FlagAtivo = endereco.FlagAtivo
                           });
                    
                }
                catch (Exception ex)
                {
                    await _logSistemaAppService.Add(CodLogSistema.Erro_EnderecosController_IncluirEndereco_CountByIdPessoa,
                                    new
                                    {
                                        Sucesso = false,
                                        Conteudo = endereco.Conteudo,
                                        IdPessoa = endereco.IdPessoa,
                                        IdUsuario = endereco.IdUsuario,
                                        IdEndereco = endereco.IdEndereco,
                                        DataOperacao = endereco.DataOperacao,
                                        FlagAtivo = endereco.FlagAtivo
                                    }, ex);

                    return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
                }

                try
                {                    
                    end.Conteudo = JsonConvert.SerializeObject(endereco.Conteudo);
                    end.FlagAtivo = endereco.FlagAtivo = naoPossuiEnderecos;                    
                    await _enderecoAppService.Incluir(end);
                    await _logSistemaAppService.Add(CodLogSistema.EnderecosController_IncluirEndereco_enderecoAppService_Incluir,
                           new
                           {
                               Sucesso = true,
                               Conteudo = end.Conteudo,
                               FlagAtivo = end.FlagAtivo
                           });
                    naoPossuiEnderecos = qtdeEnderecos == 0;                    
                }
                catch (Exception ex)
                {
                    await _logSistemaAppService.Add(CodLogSistema.Erro_EnderecosController_IncluirEndereco_enderecoAppService_Incluir,
                                                        new
                                                        {
                                                            Sucesso = false,
                                                            Conteudo = end.Conteudo,
                                                            FlagAtivo = end.FlagAtivo
                                                        }, ex);

                    return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
                }

                try
                {
                    PessoasEnderecos pessoaEndereco = new PessoasEnderecos
                    {
                        IdPessoa = endereco.IdPessoa,
                        IdEndereco = end.IdEndereco,
                        DataOperacao = DateTime.Now,
                        FlagAtivo = naoPossuiEnderecos,
                        IdUsuario = endereco.IdUsuario
                    };
                    await _pessoasEnderecosAppService.Incluir(pessoaEndereco);
                    await _logSistemaAppService.Add(CodLogSistema.EnderecosController_IncluirEndereco_pessoasEnderecosAppService_Incluir,
                           new
                           {
                               Sucesso = true,
                               IdPessoa = pessoaEndereco.IdPessoa,
                               IdEndereco = pessoaEndereco.IdEndereco,
                               DataOperacao = pessoaEndereco.DataOperacao,
                               FlagAtivo = pessoaEndereco.FlagAtivo,
                               IdUsuario = pessoaEndereco.IdUsuario
                           });
                }
                catch (Exception ex)
                {
                    await _logSistemaAppService.Add(CodLogSistema.Erro_EnderecosController_IncluirEndereco_pessoasEnderecosAppService_Incluir,
                           new
                           {
                               Sucesso = false,
                               IdPessoa = endereco.IdPessoa,
                               IdEndereco = end.IdEndereco,
                               DataOperacao = DateTime.Now,
                               FlagAtivo = naoPossuiEnderecos,
                               IdUsuario = endereco.IdUsuario
                           });
                    return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
                }

                try
                {
                    endereco.IdEndereco = end.IdEndereco;
                    EnderecoConteudoDto enderecoEmailSms = JsonConvert.DeserializeObject<EnderecoConteudoDto>(end.Conteudo);

                    var usuario = await _usuarioAppService.BuscarId(endereco.IdUsuario);
                    await _logSistemaAppService.Add(CodLogSistema.EnderecosController_IncluirEndereco_usuarioAppService_BuscarId,
                               new
                               {
                                   Sucesso = true,
                                   IdEndereco = end.IdEndereco,
                                   Conteudo = end.Conteudo
                               });
                    try
                    {
                        // Enviar SMS
                        var templateSms = await _smsAppService.GetTemplateCriarEndereco(usuario.NomeUsuario, endereco.IdPessoa.ToString());
                        await _logSistemaAppService.Add(CodLogSistema.EnderecosController_IncluirEndereco_smsAppService_GetTemplateCriarEndereco,
                               new
                               {
                                   Sucesso = true,
                                   NomeUsuario = usuario.NomeUsuario,
                                   IdPessoa = endereco.IdPessoa.ToString()
                               });

                        try
                        {
                            await _smsAppService.Send(usuario.IdPessoa, templateSms);
                            await _logSistemaAppService.Add(CodLogSistema.EnderecosController_IncluirEndereco_smsAppService_Send,
                                                       new
                                                       {
                                                           Sucesso = true,
                                                           IdPessoa = usuario.IdPessoa,
                                                       });
                        }
                        catch (Exception ex)
                        {
                            await _logSistemaAppService.Add(CodLogSistema.Erro_EnderecosController_IncluirEndereco_smsAppService_Send,
                                                       new
                                                       {
                                                           Sucesso = true,
                                                           IdPessoa = usuario.IdPessoa,
                                                       });
                            return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        await _logSistemaAppService.Add(CodLogSistema.Erro_EnderecosController_IncluirEndereco_smsAppService_GetTemplateCriarEndereco,
                               new
                               {
                                   Sucesso = false,
                                   NomeUsuario = usuario.NomeUsuario,
                                   IdPessoa = endereco.IdPessoa.ToString()
                               });
                        return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
                    }

                    try
                    {
                        // Enviar Email
                        var templateEmail = await _emailAppService.GetTemplateCriarEndereco(usuario.NomeUsuario, enderecoEmailSms);
                        await _logSistemaAppService.Add(CodLogSistema.EnderecosController_IncluirEndereco_emailAppService_GetTemplateCriarEndereco,
                                                       new
                                                       {
                                                           Sucesso = true,
                                                           NomeUsuario = usuario.NomeUsuario,
                                                           Logradouro = enderecoEmailSms.Logradouro,
                                                           Numero = enderecoEmailSms.Numero,
                                                           Complemento = enderecoEmailSms.Complemento,
                                                           Localidade = enderecoEmailSms.Localidade,
                                                           Bairro = enderecoEmailSms.Bairro,
                                                           Cep = enderecoEmailSms.Cep,
                                                           Uf = enderecoEmailSms.Uf
                                                       });

                        try
                        {
                            await _emailAppService.Send(usuario.IdPessoa, templateEmail, assunto: "Inclusão de Endereço");
                            await _logSistemaAppService.Add(CodLogSistema.EnderecosController_IncluirEndereco_emailAppService_Send,
                                                       new
                                                       {
                                                           Sucesso = true,
                                                           IdPessoa = usuario.IdPessoa,                                                           
                                                       });
                            return Ok(endereco);
                        }
                        catch (Exception ex)
                        {
                            await _logSistemaAppService.Add(CodLogSistema.Erro_EnderecosController_IncluirEndereco_emailAppService_Send,
                                                       new
                                                       {
                                                           Sucesso = true,
                                                           IdPessoa = usuario.IdPessoa,
                                                       });
                            return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        await _logSistemaAppService.Add(CodLogSistema.Erro_EnderecosController_IncluirEndereco_emailAppService_GetTemplateCriarEndereco,
                                                       new
                                                       {
                                                           Sucesso = false,
                                                           NomeUsuario = usuario.NomeUsuario,
                                                           Logradouro = enderecoEmailSms.Logradouro,
                                                           Numero = enderecoEmailSms.Numero,
                                                           Complemento = enderecoEmailSms.Complemento,
                                                           Localidade = enderecoEmailSms.Localidade,
                                                           Bairro = enderecoEmailSms.Bairro,
                                                           Cep = enderecoEmailSms.Cep,
                                                           Uf = enderecoEmailSms.Uf
                                                       });
                        return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
                    }                    
                }
                catch (Exception ex)
                {
                    await _logSistemaAppService.Add(CodLogSistema.Erro_EnderecosController_IncluirEndereco_usuarioAppService_BuscarId,
                               new
                               {
                                   Sucesso = true,
                                   IdEndereco = end.IdEndereco,
                                   Conteudo = end.Conteudo
                               });
                    return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
                }                                

                
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
                //return BadRequest("Não foi possível retornar os dados.");
            }
        }

        [HttpPut("AtualizarEndereco")]
        public async Task<IActionResult> AtualizarEndereco([FromBody] Enderecos endereco)
        {
            EnderecosDto enderecoDto = null;
            try
            {
                try
                {
                    //Enderecos end = _mapper.Map<Enderecos>(endereco);
                    enderecoDto = JsonConvert.DeserializeObject<EnderecosDto>(endereco.Conteudo);
                    endereco.Conteudo = JsonConvert.SerializeObject(enderecoDto.Conteudo);
                    //endereco.FlagAtivo = false;

                    await _enderecoAppService.Atualizar(endereco);
                    await _logSistemaAppService.Add(CodLogSistema.EnderecosController_AtualizarEndereco_Atualizar,
                                                           new
                                                           {
                                                               Sucesso = true,
                                                               IdEndereco = endereco.IdEndereco,
                                                               IdUsuario = endereco.IdUsuario,
                                                               EnderecosDto = endereco,
                                                               DataOperacao = endereco.DataOperacao,
                                                               FlagAtivo = endereco.FlagAtivo
                                                           });
                }
                catch (Exception ex)
                {
                    await _logSistemaAppService.Add(CodLogSistema.Erro_EnderecosController_AtualizarEndereco_Atualizar,
                                                           new
                                                           {
                                                               Sucesso = false,
                                                               IdEndereco = endereco.IdEndereco,
                                                               IdUsuario = endereco.IdUsuario,
                                                               EnderecosDto = endereco,
                                                               DataOperacao = endereco.DataOperacao,
                                                               FlagAtivo = endereco.FlagAtivo
                                                           });
                    return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
                }

                try
                {
                    var usuario = await _usuarioAppService.BuscarId(endereco.IdUsuario);
                    await _logSistemaAppService.Add(CodLogSistema.EnderecosController_AtualizarEndereco_BuscarId,
                                                               new
                                                               {
                                                                   Sucesso = true,
                                                                   IdUsuario = usuario.IdUsuario,
                                                                   IdPessoa = usuario.IdPessoa,
                                                                   NomeUsuario = usuario.NomeUsuario,
                                                                   Enderecos = usuario.Enderecos
                                                               });
                    try
                    {
                        // Enviar SMS
                        var templateSms = await _smsAppService.GetTemplateAlterarEndereco(usuario.NomeUsuario, endereco.IdUsuario.ToString());
                        await _logSistemaAppService.Add(CodLogSistema.EnderecosController_AtualizarEndereco_smsAppService_GetTemplateAlterarEndereco,
                                                       new
                                                       {
                                                           Sucesso = true,
                                                           NomeUsuario = usuario.NomeUsuario,
                                                           IdUsuario = endereco.IdUsuario,
                                                           IdEndereco = endereco.IdEndereco,                                                           
                                                       });
                        try
                        {
                            await _smsAppService.Send(usuario.IdPessoa, templateSms);
                            await _logSistemaAppService.Add(CodLogSistema.EnderecosController_AtualizarEndereco_smsAppService_Send,
                                                                                  new
                                                                                  {
                                                                                      Sucesso = true,
                                                                                      IdPessoa = usuario.IdPessoa,
                                                                                      TemplateSms = templateSms.ToString(),
                                                                                      IdUsuario = usuario.IdUsuario,                                                                                      
                                                                                  });
                        }
                        catch (Exception ex)
                        {
                            await _logSistemaAppService.Add(CodLogSistema.Erro_EnderecosController_AtualizarEndereco_smsAppService_Send,
                                                                                  new
                                                                                  {
                                                                                      Sucesso = false,
                                                                                      IdPessoa = usuario.IdPessoa,
                                                                                      TemplateSms = templateSms.ToString(),
                                                                                      IdUsuario = usuario.IdUsuario,
                                                                                  });
                            return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        await _logSistemaAppService.Add(CodLogSistema.Erro_EnderecosController_AtualizarEndereco_smsAppService_GetTemplateAlterarEndereco,
                                                       new
                                                       {
                                                           Sucesso = false,
                                                           NomeUsuario = usuario.NomeUsuario,
                                                           IdUsuario = endereco.IdUsuario,
                                                           IdEndereco = endereco.IdEndereco,
                                                       });
                        return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
                    }

                    try
                    {
                        // Enviar Email
                        var templateEmail = await _emailAppService.GetTemplateAlterarEndereco(usuario.NomeUsuario);
                        await _logSistemaAppService.Add(CodLogSistema.EnderecosController_AtualizarEndereco_emailAppService_GetTemplateAlterarEndereco,
                                                      new
                                                      {
                                                          Sucesso = true,
                                                          NomeUsuario = usuario.NomeUsuario,
                                                          IdUsuario = endereco.IdUsuario,
                                                          IdEndereco = endereco.IdEndereco,
                                                      });
                        try
                        {
                            await _emailAppService.Send(usuario.IdPessoa, templateEmail, assunto: "Atualização de Endereço");
                            await _logSistemaAppService.Add(CodLogSistema.EnderecosController_AtualizarEndereco_emailAppService_Send,
                                                                                 new
                                                                                 {
                                                                                     Sucesso = true,
                                                                                     IdPessoa = usuario.IdPessoa,
                                                                                     TemplateSms = templateEmail.ToString(),
                                                                                     IdUsuario = usuario.IdUsuario,
                                                                                 });

                            enderecoDto.IdEndereco = endereco.IdEndereco;
                            enderecoDto.FlagAtivo = false;
                            return Ok(enderecoDto);
                        }
                        catch (Exception ex)
                        {
                            await _logSistemaAppService.Add(CodLogSistema.Erro_EnderecosController_AtualizarEndereco_emailAppService_Send,
                                                                                 new
                                                                                 {
                                                                                     Sucesso = false,
                                                                                     IdPessoa = usuario.IdPessoa,
                                                                                     TemplateSms = templateEmail.ToString(),
                                                                                     IdUsuario = usuario.IdUsuario,
                                                                                 });
                            return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        await _logSistemaAppService.Add(CodLogSistema.EnderecosController_AtualizarEndereco_emailAppService_GetTemplateAlterarEndereco,
                                                      new
                                                      {
                                                          Sucesso = false,
                                                          NomeUsuario = usuario.NomeUsuario,
                                                          IdUsuario = endereco.IdUsuario,
                                                          IdEndereco = endereco.IdEndereco,
                                                      });
                        return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
                    }
                    
                }
                catch (Exception ex)
                {
                    await _logSistemaAppService.Add(CodLogSistema.Erro_EnderecosController_AtualizarEndereco_BuscarId,
                                                               new
                                                               {
                                                                   Sucesso = false,
                                                                   IdUsuario = endereco.IdUsuario,                                                                   
                                                               });
                    return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
                }
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);                
            }
        }

        [HttpDelete("ApagarEndereco/{idEndereco}")]
        public async Task<IActionResult> ApagarEndereco(int idEndereco)
        {
            PessoasEnderecos pessoaEndereco = null;
            long idPessoa = 0;
            bool enderecoPrincipal = false;
            try
            {
                pessoaEndereco = await _pessoasEnderecosAppService.BuscarPorEndereco(idEndereco);
                if (pessoaEndereco != null)
                {
                    idPessoa = pessoaEndereco.IdPessoa;
                    enderecoPrincipal = pessoaEndereco.IdEnderecoNavigation != null 
                        && pessoaEndereco.IdEnderecoNavigation.FlagAtivo.HasValue ? pessoaEndereco.IdEnderecoNavigation.FlagAtivo.Value : false;
                }
                    
            }
            catch (Exception)
            {
                throw;
            }

            try
            {
                await _pessoasEnderecosAppService.RemoverPorIdEndereco(idEndereco);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_EnderecosController_ApagarEndereco_RemoverPorIdEndereco,
                new
                {
                    Sucesso = false,
                    IdEndereco = idEndereco,
                }, ex);

                return InternalServerError("Ocorreu um erro ao tentar remover endereço.", ex);
            }

            try
            {
                await _enderecoAppService.Apagar(idEndereco);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_EnderecosController_ApagarEndereco_Apagar,
                new
                {
                    Sucesso = false,
                    IdEndereco = idEndereco,
                }, ex);
                return InternalServerError("Ocorreu um erro ao tentar remover o endereço.", ex);
            }

            try
            {
                if (idPessoa > 0)
                {
                    var enderecosPessoas = await _pessoasEnderecosAppService.BuscarPorPessoa(idPessoa);
                    if (enderecosPessoas != null
                        && enderecosPessoas.Count(x => x.IdEnderecoNavigation != null) > 0
                        && enderecoPrincipal)
                    {
                        var enderecoPessoa = enderecosPessoas.First(x => x.IdEnderecoNavigation != null);
                        await _pessoasEnderecosAppService.AtualizarEnderecoPrincipal(enderecoPessoa.IdEndereco);
                    }
                }
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_EnderecosController_ApagarEndereco_AoAtualizarUmNovoEnderecoPrincipal,
                new
                {
                    Sucesso = false,
                    IdEnderecoApagado = idEndereco,
                    IdPessoa = idPessoa
                }, ex);
                return InternalServerError("Ocorreu um erro ao atualizar um novo endereço como principal.", ex);
            }

            return NoContent();
        }

        [HttpPost("AtualizarEnderecoPrincipal/{idEndereco}")]
        public async Task<IActionResult> AtualizarEnderecoPrincipal(int idEndereco)
        {
            try
            {
                await _pessoasEnderecosAppService.AtualizarEnderecoPrincipal(idEndereco);
                await _logSistemaAppService.Add(CodLogSistema.EnderecosController_AtualizarEnderecoPrincipal_AtualizarEnderecoPrincipal,
                                                                                 new
                                                                                 {
                                                                                     Sucesso = true,
                                                                                     IdEndereco = idEndereco,
                                                                                 });
                return Ok();

            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_EnderecosController_AtualizarEnderecoPrincipal_AtualizarEnderecoPrincipal,
                                                               new
                                                               {
                                                                   Sucesso = false,
                                                                   IdEndereco = idEndereco,
                                                               });
                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }               
    }
}
