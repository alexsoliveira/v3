using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TGS.Identity.API.Models;

namespace TGS.Identity.API.Controllers
{
    [Route("api/identidade")]
    public class UserController : MainController
    {        
        private readonly UserManager<IdentityUser> _userManager;
        //private readonly AppSettings _appSettings;
        //private readonly SiteSettings _siteSettings;
        //private readonly IEmailSender _emailSender;

        public UserController(UserManager<IdentityUser> userManager)
        {            
            _userManager = userManager;
        }

        [HttpPost("AlterarSenha")]
        public async Task<ActionResult> AlterarSenha(UsuarioAlterarSenha obj)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(obj.UserId);

                if (user != null)
                {
                    var result = await _userManager.ChangePasswordAsync(user, obj.SenhaAtual, obj.NovaSenha);

                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            AdicionarErroProcessamento(error.Description);
                        }
                        return CustomResponse();
                    }
                    return CustomResponse(result.Succeeded);
                }
                else
                {
                    AdicionarErroProcessamento("Usuário não encontrado!");
                    return CustomResponse();
                }
            }
            catch
            {
                AdicionarErroProcessamento("Erro ao alterar senha. Contate o administrador do sistema");
                return CustomResponse();
            }
        }

        [HttpPost("ObterEmail")]
        public async Task<ActionResult> ObterEmail([FromBody] UsuarioAlterarSenha obj)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(obj.UserId);

                if (user != null)
                {                                                                                
                    return CustomResponse(user.Email);
                }

                AdicionarErroProcessamento("Usuário não encontrado!");
                return CustomResponse();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        //[HttpPost("ObterEmail")]
        //public async Task<ActionResult> ObterEmail(UsuarioResetSenha obj)
        //{
        //    try
        //    {
        //        var user = await _userManager.FindByIdAsync(obj.UserId);

        //        if (user != null)
        //        {
        //            return CustomResponse(user.Email);
        //        }

        //        AdicionarErroProcessamento("Usuário não encontrado!");
        //        return CustomResponse();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
