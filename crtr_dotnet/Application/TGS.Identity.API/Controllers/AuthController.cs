using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TGS.Identity.API.Extensions;
using TGS.Identity.API.Models;
using TGS.Identity.API.Services;

namespace TGS.Identity.API.Controllers
{
    [Route("api/identidade")]
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppSettings _appSettings;
        private readonly SiteSettings _siteSettings;
        private readonly IEmailSender _emailSender;

        public AuthController(SignInManager<IdentityUser> signInManager,
                              UserManager<IdentityUser> userManager,
                              RoleManager<IdentityRole> roleManager,
                              IOptions<AppSettings> appSettings,
                              IOptions<SiteSettings> siteSettings,
                              IEmailSender emailSender)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _appSettings = appSettings.Value;
            _siteSettings = siteSettings.Value;
            _emailSender = emailSender;
        }

        [HttpPost("Cadastrar")]
        public async Task<ActionResult> Cadastrar([FromBody] UsuarioRegistro usuarioRegistro)
        {
            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);

                var user = new IdentityUser
                {
                    UserName = usuarioRegistro.Email,
                    Email = usuarioRegistro.Email,
                    EmailConfirmed = true
                };

                //lixo - Quando tiver um e-mail do site definido, remover estas linhas
                //user.EmailConfirmed = true;
                //lixo

                var result = await _userManager.CreateAsync(user, usuarioRegistro.Senha);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        AdicionarErroProcessamento(error.Description);
                    }

                    return CustomResponse();
                }

                var identityUser = await _userManager.FindByNameAsync(user.UserName);
                await _userManager.AddToRoleAsync(identityUser, "Cliente");

                return CustomResponse();
            }
            catch (Exception ex)
            {
                var msgErro = $"{ex.Message} {(ex.InnerException != null ? "\n\n\n InnerException Message: " + ex.InnerException.Message : "")}";
                return StatusCode(500, msgErro);
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(UsuarioLogin usuarioLogin)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = await _userManager.FindByEmailAsync(usuarioLogin.Email);

            if (user == null)
            {
                AdicionarErroProcessamento("Usuário e/ou Senha incorretos");
                return CustomResponse();
            }

            var confirm = await _userManager.IsEmailConfirmedAsync(user);

            if (!confirm)
            {                
                return CustomResponse(new UsuarioRespostaLogin { ContaAtivada = confirm });
            }

            var result = await _signInManager.PasswordSignInAsync(usuarioLogin.Email, usuarioLogin.Senha, false, true);

            if (result.Succeeded)
            {
                return CustomResponse(await GerarJwt(usuarioLogin.Email));
            }

            if (result.IsLockedOut)
            {
                AdicionarErroProcessamento("Usuário temporariamente bloqueado por tentativas inválidas. Tente novamente mais tarde!");
                return CustomResponse();
            }

            AdicionarErroProcessamento("Usuário e/ou Senha incorretos");
            return CustomResponse();
        }

        [HttpPost("EnviarEmailAtivacao")]
        public async Task<ActionResult> EnviarEmailAtivacao(UsuarioConta dados)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByEmailAsync(dados.Email);

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    var callbackUrl = Url.Content($"{_siteSettings.Host}/{_siteSettings.ActionConfirmarEmail}?userId={user.Id}&code={HttpUtility.UrlEncode(code)}");

                    var htmlEmail = TemplateEmail.TemplateEmailAtivacaoConta(callbackUrl);

                    var resposta = await _emailSender.SendEmailAsync(user.Email, 
                                                                     user.UserName, 
                                                                     $"{_siteSettings.Sistema} | Ativação de conta", 
                                                                     htmlEmail);

                    if (!string.IsNullOrEmpty(resposta))
                        return StatusCode(500, resposta);

                    return CustomResponse();
                }

                return CustomResponse(ModelState);
            }
            catch (Exception ex)
            {
                AdicionarErroProcessamento($"Falha ao enviar e-mail. Contate o administrador do sistema! {GetAllErrorsExceptions(ex)}");
                return CustomResponse();
            }
        }

        [HttpPost("ConfirmarEmailAtivacao")]
        public async Task<ActionResult> ConfirmarEmailAtivacao(UsuarioConfirmaConta dados)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(dados.UserId);

                string code = HttpUtility.UrlDecode(dados.Code);
                var result = await _userManager.ConfirmEmailAsync(user, code);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        AdicionarErroProcessamento(error.Description);
                    }
                    return CustomResponse();
                }
                return CustomResponse(user);
            }
            catch (Exception ex)
            {
                AdicionarErroProcessamento("Erro ao ativar a conta. Contate o administrador do sistema");
                return CustomResponse();
            }
        }

        [HttpPost("EnviarEmailResetSenha")]
        public async Task<ActionResult> EnviarEmailResetSenha(UsuarioConta dados)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(dados.Email);

                if (user != null)
                {
                    var code = await _userManager.GeneratePasswordResetTokenAsync(user);

                    var callbackUrl = Url.Content($"{_siteSettings.Host}/{_siteSettings.ActionResetarSenha}?userId={user.Id}&code={WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code))}");

                    await _emailSender.SendEmailAsync(user.Email, user.UserName, $"{_siteSettings.Sistema} | Resetar senha",
                        $"Clique neste link para resetar sua senha: <a href='{callbackUrl}'>link</a>");

                    return CustomResponse(true);
                }
                else
                {
                    AdicionarErroProcessamento("E-mail não existe cadastrado no sistema!");
                    return CustomResponse();
                }
            }
            catch
            {
                AdicionarErroProcessamento("Falha ao enviar e-mail. Contate o administrador do sistema!");
                return CustomResponse();
            }
        }

        [HttpPost("ResetarSenha")]
        public async Task<ActionResult> ResetarSenha(UsuarioResetSenha obj)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(obj.UserId);

                if (user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(obj.Code)), obj.Senha);

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
                AdicionarErroProcessamento("Erro resetar senha. Contate o administrador do sistema");
                return CustomResponse();
            }
        }

        private async Task<UsuarioRespostaLogin> GerarJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);            

            var identityClaims = await ObterClaimsUsuario(claims, user);
            var encodedToken = CodificarToken(identityClaims);

            return ObterRespostaToken(encodedToken, user, claims);
        }

        private async Task<ClaimsIdentity> ObterClaimsUsuario(ICollection<Claim> claims, IdentityUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));
           
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            return identityClaims;
        }

        private string CodificarToken(ClaimsIdentity identityClaims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Emissor,
                Audience = _appSettings.ValidoEm,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),             
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            return tokenHandler.WriteToken(token);
        }

        private UsuarioRespostaLogin ObterRespostaToken(string encodedToken, IdentityUser user, IEnumerable<Claim> claims)
        {
            return new UsuarioRespostaLogin
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_appSettings.ExpiracaoHoras).TotalSeconds,
                ContaAtivada = user.EmailConfirmed,
                UsuarioToken = new UsuarioToken
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Claims = claims.Select(c => new UsuarioClaim { Type = c.Type, Value = c.Value })
                }
            };
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

    }
}