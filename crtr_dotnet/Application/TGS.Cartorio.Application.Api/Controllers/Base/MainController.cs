using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;

namespace TGS.Cartorio.Application.Api.Controllers.Base
{
    public abstract class MainController : ControllerBase
    {
        protected string GetToken()
        {
            try
            {
                var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
                
                if (string.IsNullOrEmpty(token))
                    throw new Exception("Não foi possível recuperar o token do Usuário Logado.");

                return token;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected string GetTextoPadraoParaErroToken()
        {
            try
            {
                return "Não foi possível recuperar os dados da sua conta. Acesse novamente o site Tabelionet e tente novamente!";
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected string GetMessageError(Exception ex)
        {
            try
            {
                string msg = "";

                if (ex != null && !string.IsNullOrEmpty(ex.Message))
                    msg += $"Exception: {ex.Message}";
                
                if (ex != null 
                    && ex.InnerException != null 
                    && !string.IsNullOrEmpty(ex.InnerException.Message))
                    msg += $"\n\nInnerException: {ex.InnerException.Message}";

                return msg;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected IActionResult InternalServerError(string msg = null, Exception ex = null)
        {
            try
            {
#if Debug
                if (string.IsNullOrEmpty(msg))
                    msg = GetMessageError(ex);
                else
                    msg += $"\n\n{GetMessageError(ex)}";

                return StatusCode(500, msg);
#else
                if (string.IsNullOrEmpty(msg))
                    return StatusCode(500, "Ocorreu um erro interno!");

                return StatusCode(500, msg);
#endif
            }
            catch
            {
                if (ex == null)
                    return StatusCode(500, "Ocorreu um erro interno");
                
                return StatusCode(500, ex);
            }
        }
    }
}
