
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TGS.Cartorio.Application.AppServices.Interfaces
{
    public interface IEmailSmsAppService
    {
        Task Send(long? idPessoa, string mensagem);
    }
}
