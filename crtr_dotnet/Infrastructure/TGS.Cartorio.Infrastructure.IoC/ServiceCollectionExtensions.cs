using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using TGS.Cartorio.Infrastructure.Utility;
using TGS.Cartorio.Infrastructure.Utility.Settings;

namespace TGS.Cartorio.Infrastructure.IoC
{
    public static class ServiceCollectionExtensions
    {
        private const string HttpCircuitBreaker = nameof(HttpCircuitBreaker);
        private const string HttpRetry = nameof(HttpRetry);
        private const string PoliciesConfigurationSectionName = "Policies";

        public static IServiceCollection AddPolicies(
            this IServiceCollection services,
            IConfiguration configuration,
            string configurationSectionName = PoliciesConfigurationSectionName)
        {
            var section = configuration.GetSection(configurationSectionName);
            services.Configure<PolicyOptions>(configuration);
            var policyOptions = section.Get<PolicyOptions>();

            var policyRegistry = services.AddPolicyRegistry();
            policyRegistry.Add(
                HttpRetry,
                HttpPolicyExtensions
                    .HandleTransientHttpError()
                    .WaitAndRetryAsync(
                        policyOptions.HttpRetry.Count,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(policyOptions.HttpRetry.BackoffPower, retryAttempt))));
            policyRegistry.Add(
                HttpCircuitBreaker,
                HttpPolicyExtensions
                    .HandleTransientHttpError()
                    .CircuitBreakerAsync(
                        handledEventsAllowedBeforeBreaking: policyOptions.HttpCircuitBreaker.ExceptionsAllowedBeforeBreaking,
                        durationOfBreak: policyOptions.HttpCircuitBreaker.DurationOfBreak));

            return services;
        }

        public static IServiceCollection AddHttpClient<TImplementation, TClientOptions>(this IServiceCollection services, IConfiguration configuration, string configurationSectionName)
           where TImplementation : class
           where TClientOptions : Settings, new() =>
           services
               .Configure<TClientOptions>(configuration.GetSection(configurationSectionName))
               .AddTransient<UserAgentDelegatingHandler>()
               //.AddTransient<LogHttpClientHandler>()
               .AddHttpClient<TImplementation>()
               .ConfigureHttpClient((sp, options) =>
               {
                   var httpClientOptions = sp.GetRequiredService<IOptions<TClientOptions>>().Value;
                   options.BaseAddress = httpClientOptions.UrlApi;
                   options.Timeout = httpClientOptions.Timeout;
                   options.DefaultRequestHeaders.Accept.Clear();
                   options.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                   if (!(string.IsNullOrEmpty(httpClientOptions.NameHeadersKeyAccess) || string.IsNullOrEmpty(httpClientOptions.ValueHeadersKeyAccess)))
                       options.DefaultRequestHeaders.Add(httpClientOptions.NameHeadersKeyAccess, httpClientOptions.ValueHeadersKeyAccess);

               })
               .ConfigurePrimaryHttpMessageHandler(h =>
               {
                   var handler = new HttpClientHandler();
                   return handler;
               })
                .AddPolicyHandlerFromRegistry(HttpRetry)
                .AddPolicyHandlerFromRegistry(HttpCircuitBreaker)
               .AddHttpMessageHandler<UserAgentDelegatingHandler>()
               //.AddHttpMessageHandler<LogHttpClientHandler>()
               .Services;
    }
}
