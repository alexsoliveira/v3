using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TGS.Cartorio.Infrastructure.Utility.Extensions;

namespace TGS.Cartorio.Infrastructure.Utility.ApiClient
{
    public abstract class ApiClientBase
    {
        public readonly HttpClient _client;

        public ApiClientBase(HttpClient client)
        {
            _client = client;
        }

        public virtual async Task<Retorno<T>> Get<T>(string path, bool processarComObjRetorno = false, string token = null)
        {
            try
            {
                if (!string.IsNullOrEmpty(token))
                    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _client.GetAsync(path);

                var retorno = await ProcessarRetornoComRetornoObj<T>(response);

                var strResponse = await response.Content.ReadAsStringAsync();

                retorno.Log = LogServicoDto.Create($"{_client.BaseAddress.AbsoluteUri}{path}",
                    "POST",
                    string.Empty,
                    strResponse,
                    retorno == null ? "" : JsonConvert.SerializeObject(retorno),
                    response.StatusCode);

                return retorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<Retorno<T>> PostRetorno<T>(string path, object valores = null, bool ambienteHomologacao = false, string token = null)
        {
            try
            {
                string strValores = string.Empty;
                if (valores != null)
                    strValores = JsonConvert.SerializeObject(valores);

                if (!string.IsNullOrEmpty(token))
                    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpContent content = new StringContent(strValores, Encoding.UTF8, "application/json");
                var response = await _client.PostAsync(path, content);

                var retorno = await ProcessarRetornoComRetornoObj<T>(response);

                var strResponse = await response.Content.ReadAsStringAsync();
                
                retorno.Log = LogServicoDto.Create($"{_client.BaseAddress.AbsoluteUri}{path}",
                    "POST",
                    strValores,
                    strResponse,
                    retorno == null ? "" : JsonConvert.SerializeObject(retorno),
                    response.StatusCode);

                return retorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task Post(string path, object valores)
        {
            var retorno = await _client.PostAsJsonAsync(path, valores);
            ProcessarRetorno(retorno);
        }

        internal virtual Task<T> ProcessarRetorno<T>(HttpResponseMessage retorno)
        {
            if (retorno.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var sucesso = retorno.Content.ReadAsAsync<T>().Result;
                return Task.Run(() => sucesso);
            }
            else
            {
                if (retorno.Content.Headers.IsTextHtmlMediaType())
                {
                    var strerro = retorno.Content.ReadAsStringAsync().Result;
                    strerro = ObterDescricaoErroHtml(strerro);
                    throw new System.Exception(strerro);
                }
                else if (retorno.Content.Headers.IsApplicationProblemJsonMediaType())
                {
                    var erro = retorno.Content.ReadAsStringAsync().Result;                    
                    throw new System.Exception(erro);
                }               
                else
                {
                    var erro = retorno.Content.ReadAsAsync<T>().Result;
                    return Task.Run(() => erro);
                }
            }
        }

        internal virtual Task<Retorno<T>> ProcessarRetornoComRetornoObj<T>(HttpResponseMessage retorno)
        {
            if (retorno.StatusCode == HttpStatusCode.NoContent)
            {
                return Task.Run(() => new Retorno<T> { Sucesso = true });
            }
            else if (retorno.IsSuccessStatusCode)
            {
                var sucesso = retorno.Content.ReadAsAsync<T>().Result;
                return Task.Run(() => new Retorno<T> { ObjRetorno = sucesso, Sucesso = true });
            }
            else
            {
                if (retorno.Content.Headers.IsTextHtmlMediaType())
                {
                    var strerro = retorno.Content.ReadAsStringAsync().Result;
                    strerro = ObterDescricaoErroHtml(strerro);
                    throw new System.Exception(strerro);
                }
                else if (retorno.Content.Headers.IsApplicationProblemJsonMediaType())
                {
                    var erro = retorno.Content.ReadAsStringAsync().Result;
                    throw new System.Exception(erro);
                }
                else
                {
                    var erro = retorno.Content.ReadAsStringAsync().Result;
                    return Task.Run(() => new Retorno<T> { MensagemErro = erro, Sucesso = false });
                }
            }
        }

        private void ProcessarRetorno(HttpResponseMessage retorno)
        {
            if (retorno.StatusCode == System.Net.HttpStatusCode.OK) return;

            if (retorno.Content.Headers.IsTextHtmlMediaType())
            {
                var strerro = retorno.Content.ReadAsStringAsync().Result;
                strerro = ObterDescricaoErroHtml(strerro);
                throw new System.Exception(strerro);
            }
            else if (retorno.Content.Headers.IsApplicationProblemJsonMediaType())
            {
                var erro = retorno.Content.ReadAsStringAsync().Result;
                throw new System.Exception(erro);
            }
            else
            {
                var strerro = retorno.Content.ReadAsStringAsync().Result;
                throw new System.Exception(strerro);
            }
        }

        public string ObterDescricaoErroHtml(string html)
        {
            if (string.IsNullOrEmpty(html?.Trim())) return html;

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            string text = doc.DocumentNode.SelectSingleNode("//body").InnerText;

            return string.IsNullOrEmpty(text) ? html : text;

        }
    }


    public class Retorno<T>
    {
        public Retorno() { }
        public Retorno(string mensagemErro)
        {
            MensagemErro = mensagemErro;
        }
        public bool Sucesso { get; set; }
        public string MensagemErro { get; set; }
        public T ObjRetorno { get; set; }
        public LogServicoDto Log { get; set; }
    }


    public class LogServicoDto
    {
        public string Referencia { get; set; }
        public string Url { get; set; }
        public string Verbo { get; set; }
        public int? StatusCode { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public string ObjRetorno { get; set; }
        public string ExceptionMessage { get; set; }
        public string InnerExceptionMessage { get; set; }

        public string RetornarLog()
        {
            try
            {
                return JsonConvert.SerializeObject(this);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static LogServicoDto Create(string url, string verbo, string request, string response, string objRetorno, HttpStatusCode? statusCode = null, Exception ex = null)
        {
            try
            {
                int? statusCodeTratado = null;
                if (statusCode.HasValue)
                    statusCodeTratado = (int)statusCode.Value;

                var log = new LogServicoDto
                {
                    Url = url,
                    Verbo = verbo,
                    Request = request,
                    Response = response,
                    ObjRetorno = objRetorno,
                    StatusCode = statusCodeTratado
                };

                if (ex != null && !string.IsNullOrEmpty(ex.Message))
                {
                    log.ExceptionMessage = ex.Message;

                    if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                        log.InnerExceptionMessage = ex.InnerException.Message;
                }

                return log;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

}
