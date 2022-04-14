using Newtonsoft.Json;
using System.Collections.Generic;

namespace TGS.Cartorio.Infrastructure.Utility.Others
{
    public static class SmsBody
    {
        public static string ConvertMessages(List<SmsItem> items, string identificador)
        {
            items.ForEach((item) => { item.Identificador = identificador; });
            if (items != null && items.Count > 0)
                return JsonConvert.SerializeObject(items);

            return null;
        }
    }
    public class SmsItem
    {
        [JsonProperty("SeuNum")]
        public string Identificador { get; set; }
        [JsonProperty("Telefone")]
        public string Celular { get; set; }
        [JsonProperty("Mensagem")]
        public string Mensagem { get; set; }

    }
}
