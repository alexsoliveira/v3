using Hangfire;
using Hangfire.Console;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace TGS.Cartorio.Application.Jobs.Configurations
{
    public static class HangFireConfiguration
    {
        
        public static void SetHangFireConfigurations(this IServiceCollection services, IConfiguration config)
        {
            try
            {
                services.AddHangfire(configuration => {
                    configuration.UseSqlServerStorage(config.GetConnectionString("DbTabelionetContext"));
                    configuration.UseConsole();
                });

                services.AddHangfireServer();
                services.AddMvc();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
