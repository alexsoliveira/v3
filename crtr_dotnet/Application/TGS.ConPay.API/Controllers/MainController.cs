using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Refit;
using TGS.Cartorio.Infrastructure.Utility.ApiClient;
using TGS.Pagamento.API.Configuration;
using TGS.Pagamento.API.Interfaces;

namespace TGS.Pagamento.API.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private readonly SettingsConPay _settingsConPay;
        protected IConPay _conpay;
        protected ICollection<string> Erros = new List<string>();
        public MainController(IOptions<SettingsConPay> settingsConPay, IConPay conpay)
        {
            _settingsConPay = settingsConPay.Value;
            _conpay = conpay;
        }

        protected ActionResult CustomResponse(object result = null)
        {
            if (OperacaoValida())
            {
                return Ok(result);
            }

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Mensagens", Erros.ToArray() }
            }));
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                AdicionarErroProcessamento(erro.ErrorMessage);
            }

            return CustomResponse();
        }

        protected bool OperacaoValida()
        {
            return !Erros.Any();
        }

        protected void AdicionarErroProcessamento(string erro)
        {
            Erros.Add(erro);
        }

        protected void LimparErrosProcessamento()
        {
            Erros.Clear();
        }

        protected IActionResult CustomExceptionResponseMessage(Exception ex)
        {
            try
            {
                string erro = $"Message Error: {ex.Message}";
                
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                    erro += $"\n\nInnerException Error: {ex.InnerException.Message}";
                
                if (ex.InnerException != null && ex.InnerException.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.InnerException.Message))
                    erro += $"\n\nNext InnerException Error: {ex.InnerException.InnerException.Message}";

                return StatusCode(500, new Retorno<string>(erro));
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu um erro ao tentar processar recurso desejado!");
            }
        }

        protected async Task Autenticar()
        {
            try
            {
                var usuario = new Usuario
                {
                    accessKeyId = _settingsConPay.accessKeyId,
                    secretKey = _settingsConPay.secretKey
                };

                HttpResponseMessage res = null;
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = _settingsConPay.UrlApi;

                    client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Connection", "keep-alive");
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "*/*");
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Cache-Control", "no-cache");
                    client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0");

                    var content = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");
                    res = await client.PostAsync("v2/auth/token", content);

                    if (!res.IsSuccessStatusCode)
                    {
                        var dadosAutenticacao = new
                        {
                            MessageError = await res.Content.ReadAsStringAsync(),
                            StatusCode = res.StatusCode,
                            Headers = res.Headers,
                            RequestMessage = res.RequestMessage
                        };

                        var exInner = new Exception($"Erro ao buscar token: {JsonConvert.SerializeObject(dadosAutenticacao)}");
                        throw new Exception("Não foi possível buscar o Token da Conpay",
                            exInner);
                    }


                    var token = res.Headers.GetValues("ACCESS_TOKEN").FirstOrDefault();

                    _conpay = RestService.For<IConPay>(_settingsConPay.UrlApi.ToString(), new RefitSettings()
                    {
                        AuthorizationHeaderValueGetter = () => Task.FromResult(token),

                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}