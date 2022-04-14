using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TGS.ConPay.API.DTO;
using TGS.Pagamento.API.Configuration;
using TGS.Pagamento.API.DTO;
using TGS.Pagamento.API.ExternalServices.Base;
using TGS.Pagamento.API.ExternalServices.Conpay.Interfaces;

namespace TGS.Pagamento.API.ExternalServices.Conpay
{
    public class ConpayHttpClient<TReturn> : BaseHttpClient<TReturn>, IConpayHttpClient<TReturn>
        where TReturn : class
    {
        private SettingsConPay _settingsConpay;
        public ConpayHttpClient(IOptions<SettingsConPay> settingsConpay)
        {
            _settingsConpay = settingsConpay.Value;
        }

        protected override void ConfigureHttpClient()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = _settingsConpay.UrlApi;
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "*/*");
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Connection", "keep-alive");
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0");
        }

        protected override async Task<string> GetToken()
        {
            try
            {
                HttpResponseMessage response = await Post("v2/auth/token", new { accessKeyId = _settingsConpay.accessKeyId, secretKey = _settingsConpay.secretKey }, null);
                if (response.IsSuccessStatusCode)
                {
                    var token = response.Headers.GetValues("ACCESS_TOKEN").FirstOrDefault();
                    if (token != null && !string.IsNullOrEmpty(token))
                        return token;
                    else
                        return null;
                }
                    
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponsePayCreditCard> PagarComCartaoCredito(CreditCard creditCard)
        {
            try
            {
                HttpResponseMessage response = await PostAsync("ecommerce/charges/charge", creditCard);
                var responsePayCreditCard = new ResponsePayCreditCard();
                if (response.IsSuccessStatusCode)
                {
                    var ret = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(ret))
                        responsePayCreditCard = JsonConvert.DeserializeObject<ResponsePayCreditCard>(ret);
                }

                responsePayCreditCard.Success = response.IsSuccessStatusCode;
                responsePayCreditCard.StatusCodeServico = (int)response.StatusCode;
                return responsePayCreditCard;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseConsultaTransacao> ConsultarTransacaoCartaoCredito(string idTransacao)
        {
            try
            {
                var responseConsultaTransacao = new ResponseConsultaTransacao();
                var path = $"ecommerce/charges/{idTransacao}";
                var response = await Get(path, null);
                if (response.IsSuccessStatusCode)
                {
                    var ret = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(ret))
                        responseConsultaTransacao = JsonConvert.DeserializeObject<ResponseConsultaTransacao>(ret);
                    //responsePayCreditCard.Success = true;
                    return responseConsultaTransacao;
                }

                return responseConsultaTransacao;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
