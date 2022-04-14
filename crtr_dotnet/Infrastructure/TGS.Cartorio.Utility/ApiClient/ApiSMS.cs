using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using TGS.Cartorio.Infrastructure.Utility.Settings;
using Microsoft.Extensions.Options;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using TGS.Cartorio.Infrastructure.Utility.Others;

namespace TGS.Cartorio.Infrastructure.Utility.ApiClient
{    
    public class ApiSMS : ApiClientBase
    {
        private readonly SettingsSms _settingsSms;
        public ApiSMS(HttpClient client, IOptions<SettingsSms> settingsSms) : base(client)
        {
            _settingsSms = settingsSms.Value;
        }

        public async Task<Retorno<string>> EnviarMensagem(List<SmsItem> items, bool ambienteHomologacao = false)
        {
            try
            {
                items.ForEach((item) => { item.Identificador = _settingsSms.Identificador; });
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(
                    ASCIIEncoding.ASCII.GetBytes($"{_settingsSms.NameHeadersKeyAccess}:{_settingsSms.ValueHeadersKeyAccess}")));
                _client.DefaultRequestHeaders.Add(HttpRequestHeader.ContentType.ToString(), "application/json");
                return await PostRetorno<string>("EnviaSMS", items, ambienteHomologacao);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
