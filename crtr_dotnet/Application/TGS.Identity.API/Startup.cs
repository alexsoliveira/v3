using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TGS.Cartorio.Infrastructure.Utility.Settings;
using TGS.Cartorio.Infrastructure.Utility.WebServerContracts;
using TGS.Cartorio.Infrastructure.Utility.WebServerContracts.Interfaces;
using TGS.Identity.API.Configuration;
using TGS.Identity.API.Extensions;
using TGS.Identity.API.Services;

namespace NSE.Identidade.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IHostEnvironment hostEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            if (hostEnvironment.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityConfiguration(Configuration);

            services.AddApiConfiguration();

            services.AddSwaggerConfiguration();

            services.Configure<SettingsEmail>(Configuration.GetSection("SettingsEmail"));

            services.Configure<SiteSettings>(Configuration.GetSection("Site"));

            services.AddTransient<IEmailWebServer, EmailWebServer>();
            services.AddTransient<IEmailSender, AuthMessageSender>();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwaggerConfiguration();
            
            app.UseApiConfiguration(env);
        }
    }
}
