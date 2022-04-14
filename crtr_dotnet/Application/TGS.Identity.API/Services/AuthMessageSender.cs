using System;
using System.Threading.Tasks;
using TGS.Cartorio.Infrastructure.Utility.Others;
using TGS.Cartorio.Infrastructure.Utility.WebServerContracts.Interfaces;

namespace TGS.Identity.API.Services
{
    public class AuthMessageSender : IEmailSender
    {
        private readonly IEmailWebServer _emailWebServer;
        public AuthMessageSender(IEmailWebServer emailWebServer)
        {
            _emailWebServer = emailWebServer;
        }

        public async Task<string> SendEmailAsync(string email, string nome, string assunto, string mensagem)
        {
            try
            {
                DadosEnvioEmail dados = new DadosEnvioEmail
                {
                    Email = email,
                    Nome = nome,
                    Assunto = assunto,
                    Mensagem = mensagem
                };

                return (await _emailWebServer.EnviarMensagem(dados)).ObjRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
