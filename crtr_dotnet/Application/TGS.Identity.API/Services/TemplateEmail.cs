using System;

namespace TGS.Identity.API.Services
{
    public static class TemplateEmail
    {
        public static string TemplateEmailAtivacaoConta(string linkAtivacao)
        {
            try
            {
                string body = "<h3>Olá,</h3>" +
                              "<br>" +
                              "<p><b>Mediante a sua solicitação no site tabelionet.com.br " +
                              "enviamos este e-mail para verificação de autenticidade." +
                              "<br>" +
                              "Por favor, clique no " +
                              $"<a href=\"{linkAtivacao}\">Link</a> " +
                              "ou então copie e cole no seu navegador para validar a veracidade de sua conta.</b></p>" +
                              "<br><br>" +
                              $"<p>{linkAtivacao}</p><br>";

                return CreateHtmlEmail("Ativação de Conta Tabelionet", body);
            }
            catch (Exception)
            {
                throw;
            }
        }



        public static string CreateHtmlEmail(string assunto, string body)
        {
            try
            {
                string htmlEmail = CreateHtmlHead(assunto);
                htmlEmail += body;
                htmlEmail += CreateHtmlFooter();
                return htmlEmail;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static string CreateHtmlHead(string title)
        {
            try
            {
                return $"<html><head><meta charset=\"utf-8\" /><title>{title}</title></head><body>";
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static string CreateHtmlFooter()
        {
            try
            {
                return "<br><p>" +
                   "Caso não tenha sido você, por favor, entre em contato com o suporte." +
                   "</p><p><b>Thomas Greg & Sons</b></p></body></html>";
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
