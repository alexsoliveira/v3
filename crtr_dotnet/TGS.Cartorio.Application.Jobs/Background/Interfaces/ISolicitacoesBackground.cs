using Hangfire.Server;
using System.Threading.Tasks;

namespace TGS.Cartorio.Application.Jobs.Background.Interfaces
{
    public interface ISolicitacoesBackground
    {
        void DispararEmailSolicitacoesProntasParaEnvio(PerformContext context);
    }
}
