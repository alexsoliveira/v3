using Hangfire;
using Hangfire.Console;
using Hangfire.Server;
using System;
using TGS.Cartorio.Application.Jobs.Background.Interfaces;
using TGS.Cartorio.Application.Jobs.Configurations.Interfaces;
using TGS.Cartorio.Application.Extensions;
using TGS.Cartorio.Application.DTO;

namespace TGS.Cartorio.Application.Jobs.Configurations
{
    public class JobConfiguration : IJobConfiguration
    {
        private readonly ISolicitacoesBackground _solicitacoesBackground;
        private readonly IBoletosBackground _boletoBackground;
        public static object objLockBoleto = new object();
        public static object objLockBoletoAtualizaEstado = new object();
        public static object objLockEnvioEmail = new object();
        public JobConfiguration(ISolicitacoesBackground solicitacoesBackground, IBoletosBackground boletoBackground)
        {
            _solicitacoesBackground = solicitacoesBackground;
            _boletoBackground = boletoBackground;
        }
        public void AddRecurringJobs()
        {
            try
            {
                RecurringJob.AddOrUpdate(() => _solicitacoesBackground.DispararEmailSolicitacoesProntasParaEnvio(null), Cron.Minutely);

                RecurringJob.AddOrUpdate(() => _boletoBackground.AtualizarStatusBoletos(null), Cron.Minutely);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
