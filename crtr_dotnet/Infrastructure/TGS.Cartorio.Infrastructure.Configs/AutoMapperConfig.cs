using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace TGS.Cartorio.Infrastructure.Configs
{
     public static class AutoMapperConfig
    {
        public static IServiceCollection AdicionarAutoMapperConfig(this IServiceCollection services)
        {

            var mappingConfig = new AutoMapper.MapperConfiguration(mc =>
            {
                mc.AddProfile(new Application.Mapper.MappingProfile());
            });

            AutoMapper.IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
        }
}
