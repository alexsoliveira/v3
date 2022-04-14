using System.Threading.Tasks;
using TGS.Cartorio.Infrastructure.Utility.ApiClient;
using TGS.Cartorio.Infrastructure.Utility.Others;

namespace TGS.Cartorio.Infrastructure.Utility.WebServerContracts.Interfaces
{
    public interface IEmailWebServer
    {
        Task<Retorno<string>> EnviarMensagem(DadosEnvioEmail dadosEnvioEmail);
    }
}
