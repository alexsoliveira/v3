﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TGS.Correios.API.Controllers
{
    [ApiController]
    public abstract class MainController : Controller
    {
        protected ICollection<string> Erros = new List<string>();

        protected ActionResult CustomResponse(object result = null)
        {
            if (OperacaoValida())
            {
                return Ok(result);
            }

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Mensagens", Erros.ToArray() }
            }));
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                AdicionarErroProcessamento(erro.ErrorMessage);
            }

            return CustomResponse();
        }

        protected bool OperacaoValida()
        {
            return !Erros.Any();
        }

        protected void AdicionarErroProcessamento(string erro)
        {
            Erros.Add(erro);
        }

        protected void LimparErrosProcessamento()
        {
            Erros.Clear();
        }

        protected string GetExceptionErrors(Exception ex)
        {
            string erro = "";

            if (ex != null && !string.IsNullOrEmpty(ex.Message))
            {
                erro = ex.Message;

                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                    erro += $"\n\nInnerException: {ex.Message}";
            }

            return erro;
        }
    }
}