using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TGS.Cartorio.Infrastructure.Utility.ApiClient
{
    public class ApiPagamento : ApiClientBase
    {
        public ApiPagamento(HttpClient client) 
            : base(client)
        {
            
        }


        public async Task<Retorno<string>> GerarBoleto<TBoleto>(string path, TBoleto boleto, string token)
        {
            HttpRequestMessage request = null;
            try
            {
                _client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "text/plain");
                _client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "*/*");
                _client.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                Uri uri = new Uri(Path.Combine(_client.BaseAddress.AbsoluteUri, path));
                request = new HttpRequestMessage(HttpMethod.Post, uri);
                request.Content = new StringContent(JsonConvert.SerializeObject(boleto), Encoding.UTF8, "application/json");
            }
            catch (Exception)
            {
                throw;
            }

            HttpResponseMessage response = null;
            try
            {
                response = await _client.SendAsync(request);
            }
            catch (Exception ex)
            {
                Exception exSend = new Exception("Ocorreu um erro ao requisitar GerarBoleto!", ex);
                throw exSend;
            }

            try
            {
                var url = $"{_client.BaseAddress.AbsoluteUri}{path}";
                var objRetorno = await response.Content.ReadAsStringAsync();
                return new Retorno<string>
                {
                    Sucesso = response.IsSuccessStatusCode,
                    ObjRetorno = objRetorno,
                    Log = LogServicoDto.Create(
                        url,
                        "POST",
                        JsonConvert.SerializeObject(request),
                        JsonConvert.SerializeObject(response),
                        objRetorno,
                        response.StatusCode)
                };
            }
            catch (Exception ex)
            {
                Exception exDesserializacaoRetorno = new Exception("Ocorreu um erro ao tentar desserializar retorno da requisição GerarBoleto!", ex);
                throw exDesserializacaoRetorno;
            }
        }

    }
}
