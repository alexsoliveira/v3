using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TGS.Identity.API.Services
{
    public interface IEmailSender
    {
        Task<string> SendEmailAsync(string email, string nome, string assunto, string mensagem);
    }
}
