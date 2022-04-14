using System;
using System.Collections.Generic;
using System.IO;
using TGS.Cartorio.Application.Comunicador.Interfaces;
using TGS.Cartorio.Application.Enumerables;

namespace TGS.Cartorio.Application.Comunicador
{
    public class ComunicadorTemplate : IComunicadorTemplate
    {
        public string GetTemplate(TipoComunicador tipoComunicador, TipoMensagem tipoMensagem, Dictionary<string, string> dados)
        {
            try
            {
                string template = null;
                switch (tipoComunicador)
                {
                    case TipoComunicador.Email:
                        template = GetTemplateEmail(tipoMensagem);
                        break;
                    case TipoComunicador.SMS:
                        template = GetTemplateSMS(tipoMensagem);
                        break;
                }
                
                if (string.IsNullOrEmpty(template))
                    throw new Exception($"Template de {tipoComunicador} de {tipoMensagem} não foi localizado!");

                if (dados == null || dados.Count == 0)
                    return template;

                foreach (var dado in dados)
                    template = template.Replace(dado.Key, dado.Value);

                return template;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetTemplateSMS(TipoMensagem tipoMensagem, Dictionary<string, string> dados)
        {
            try
            {
                string template = GetTemplateSMS(tipoMensagem);
                if (string.IsNullOrEmpty(template))
                    throw new Exception($"Template de SMS {tipoMensagem.ToString()} não foi localizado!");

                if (dados == null || dados.Count == 0)
                    return template;

                foreach (var dado in dados)
                    template = template.Replace(dado.Key, dado.Value);

                return template;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GetTemplateEmail(TipoMensagem tipoMensagem)
        {
            try
            {
                string htmlFile = null;
                switch (tipoMensagem)
                {
                    case TipoMensagem.Cadastro:
                        htmlFile = @"~\Comunicador\TemplateEmail\cadastro.html";
                        break;
                    case TipoMensagem.AlterarSenha:
                        htmlFile = @"~\Comunicador\TemplateEmail\alterar_senha.html";
                        break;
                    case TipoMensagem.EsqueciMinhaSenha:
                        htmlFile = @"~\Comunicador\TemplateEmail\esqueci_minha_senha.html";
                        break;
                    default:
                        break;
                }

                if (!string.IsNullOrEmpty(htmlFile) && File.Exists(htmlFile))
                    return File.ReadAllText(htmlFile);
                
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private static string GetTemplateSMS(TipoMensagem tipoMensagem)
        {
            try
            {
                string htmlFile = null;
                switch (tipoMensagem)
                {
                    case TipoMensagem.Cadastro:
                        htmlFile = @"~\Comunicador\TemplateSMS\cadastro.txt";
                        break;
                    case TipoMensagem.AlterarSenha:
                        htmlFile = @"~\Comunicador\TemplateSMS\alterar_senha.txt";
                        break;
                    case TipoMensagem.EsqueciMinhaSenha:
                        htmlFile = @"~\Comunicador\TemplateSMS\esqueci_minha_senha.txt";
                        break;
                    default:
                        break;
                }

                if (!string.IsNullOrEmpty(htmlFile) && File.Exists(htmlFile))
                    return File.ReadAllText(htmlFile);

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
