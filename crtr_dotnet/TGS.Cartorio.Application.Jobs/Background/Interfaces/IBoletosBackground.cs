using Hangfire.Server;

namespace TGS.Cartorio.Application.Jobs.Background.Interfaces
{
    public interface IBoletosBackground
    {
        void AtualizarStatusBoletos(PerformContext consoleHangFire);
    }
}
