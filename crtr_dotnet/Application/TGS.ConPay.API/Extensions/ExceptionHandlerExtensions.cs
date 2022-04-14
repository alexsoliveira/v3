using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace TGS.Pagamento.API.Extensions
{
    public static class ExceptionHandlerExtensions
    {
        public static void UseGlobalExceptionHandler(this IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();

                    object retorno = null;

                    if (exceptionHandlerFeature != null)
                    {
                        context.Response.ContentType = "application/json";

                        //Erros API Refit                        
                        if (exceptionHandlerFeature.Error.GetType().Name == nameof(Refit.ApiException))
                        {
                            var ex = (Refit.ApiException)exceptionHandlerFeature.Error;

                            retorno = new
                            {
                                ex.StatusCode,
                                ex.Message,
                                ex.Content
                            };
                        }
                        //Erro gerais
                        else
                        {
                            retorno = new
                            {
                                context.Response.StatusCode,                                
                                exceptionHandlerFeature.Error.Message
                            };
                        }

                        await context.Response.WriteAsync(JsonConvert.SerializeObject(retorno));

                        //TODO: Implementar gravação de Log. 
                    }
                });
            });
        }
    }
}
