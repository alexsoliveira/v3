using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGS.Cartorio.Application.Jobs.Background.Interfaces;
using TGS.Cartorio.Application.Jobs.Configurations;
using TGS.Cartorio.Application.Jobs.Configurations.Interfaces;
using TGS.Cartorio.Application.Jobs.Filter;
using TGS.Cartorio.Infrastructure.Configs;
using TGS.Cartorio.Infrastructure.IoC;

namespace TGS.Cartorio.Application.Jobs
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ResolveDependencies();
            services.ResolveInternDependencies(Configuration);
            services.AdicionarAutoMapperConfig();
            services.SetHangFireConfigurations(Configuration);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TGS.Cartorio.Application.Jobs", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IJobConfiguration jobs)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TGS.Cartorio.Application.Jobs v1"));
            }

            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new [] { new AcessoLiberadoFilter() }
            });
            jobs.AddRecurringJobs();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
