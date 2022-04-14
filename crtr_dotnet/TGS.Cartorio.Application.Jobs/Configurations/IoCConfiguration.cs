using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using TGS.Cartorio.Application.Jobs.Background;
using TGS.Cartorio.Application.Jobs.Background.Interfaces;
using TGS.Cartorio.Application.Jobs.Configurations.Interfaces;

namespace TGS.Cartorio.Application.Jobs.Configurations
{
    public static class IoCConfiguration
    {
        public static void ResolveInternDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            try
            {
                services.AddTransient<IJobConfiguration, JobConfiguration>();
                services.AddTransient<ISolicitacoesBackground, SolicitacoesBackground>();
                services.AddTransient<IBoletosBackground, BoletosBackground>();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
