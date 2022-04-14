using Microsoft.Extensions.DependencyInjection;
using TGS.Pagamento.API.ExternalServices.Conpay;
using TGS.Pagamento.API.ExternalServices.Conpay.Interfaces;
using TGS.Pagamento.API.ExternalServices.Conpay.Services;

namespace TGS.Pagamento.API.Configuration
{
    public static class DependencyInjection
    {
        public static void ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped(typeof(IConpayHttpClient<>), typeof(ConpayHttpClient<>));
            services.AddScoped<IConpayServices, ConpayServices>();
        }
    }
}
