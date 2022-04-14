using Hangfire.Server;
using System;
using System.Threading.Tasks;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Application.Jobs.Background.Interfaces;
using TGS.Cartorio.Application.Jobs.Configurations;

namespace TGS.Cartorio.Application.Jobs.Background
{
    public class SolicitacoesBackground : ISolicitacoesBackground
    {
        private readonly ISolicitacoesAppService _solicitacoesAppService;
        public SolicitacoesBackground(ISolicitacoesAppService solicitacoesAppService)
        {
            _solicitacoesAppService = solicitacoesAppService;
        }
        public void DispararEmailSolicitacoesProntasParaEnvio(PerformContext context)
        {
            lock (JobConfiguration.objLockEnvioEmail)
            {
                try
                {
                    _solicitacoesAppService.BuscarSolicitacoesProntasParaEnvioParaCartorio(context);
                }
                catch { }
            }
        }
    }
}
