using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TGS.Pagamento.API.ExternalServices.Conpay.Interfaces;
using TGS.Pagamento.API.ExternalServices.Conpay.Models;

namespace TGS.Pagamento.API.ExternalServices.Base
{
    public abstract class BaseHttpClient<TReturn>
        where TReturn : class
    {
        protected HttpClient _httpClient;

        protected abstract void ConfigureHttpClient();

        public async Task<Resposta> Get(string path)
        {
            ConfigureHttpClient();
            Resposta resposta = new Resposta();

            var token = await GetToken();
            if (!string.IsNullOrEmpty(token))
                _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"Bearer {token}");

            using (var response = await _httpClient.GetAsync(path))
            {
                resposta.Sucesso = response.IsSuccessStatusCode;
                if (response.IsSuccessStatusCode)
                {
                    var objString = await response.Content.ReadAsStringAsync();
                    resposta.obj = JsonConvert.DeserializeObject<TReturn>(objString);
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    resposta.NaoAutorizado = true;
                }
                else
                {
                    var objString = await response.Content.ReadAsStringAsync();
                    resposta.obj = JsonConvert.DeserializeObject<ErrorMessage>(objString);
                }
            }
            return resposta;
        }

        public async Task<Resposta> Post(string path, object obj)
        {
            ConfigureHttpClient();
            Resposta resposta = new Resposta();

            var token = await GetToken();
            if (!string.IsNullOrEmpty(token))
                _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"Bearer {token}");


            HttpContent content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

            using (var response = await _httpClient.PostAsync(path, content))
            {
                resposta.Sucesso = response.IsSuccessStatusCode;
                if (response.IsSuccessStatusCode)
                {
                    var objString = await response.Content.ReadAsStringAsync();
                    resposta.obj = JsonConvert.DeserializeObject<TReturn>(objString);
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    resposta.NaoAutorizado = true;
                }
                else
                {
                    var objString = await response.Content.ReadAsStringAsync();
                    resposta.obj = JsonConvert.DeserializeObject<ErrorMessage>(objString);
                }
            }
            return resposta;
        }


        protected async Task<HttpResponseMessage> Post(string path, object obj, string nulo = null)
        {
            ConfigureHttpClient();
            HttpContent content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
            return await _httpClient.PostAsync(path, content);
        }

        protected async Task<HttpResponseMessage> PostAsync(string path, object obj)
        {
            ConfigureHttpClient();

            var token = await GetToken();
            if (!string.IsNullOrEmpty(token))
                _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"Bearer {token}");

            HttpContent content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

            return await _httpClient.PostAsync(path, content);
        }

        protected async Task<HttpResponseMessage> Get(string path, object obj)
        {
            ConfigureHttpClient();
            
            var token = await GetToken();
            if (!string.IsNullOrEmpty(token))
                _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"Bearer {token}");

            HttpContent content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

            return await _httpClient.GetAsync(path);
        }

        protected abstract Task<string> GetToken();
    }
}
