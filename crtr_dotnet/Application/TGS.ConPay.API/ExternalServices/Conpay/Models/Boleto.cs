using Newtonsoft.Json;
using System.Collections.Generic;

namespace TGS.Pagamento.API.ExternalServices.Conpay.Models
{
    public class Boleto
    {
        [JsonProperty("dataVencimento")]
        public string DataVencimento { get; set; }
        [JsonProperty("mensagem")]
        public string Mensagem { get; set; }
        [JsonProperty("urlNotificacao")]
        public string UrlNotificacao { get; set; }
        [JsonProperty("valor")]
        public decimal Valor { get; set; }
        [JsonProperty("pagador")]
        public Pagador Pagador { get; set; }
        [JsonProperty("emails")]
        public IList<string> Emails { get; set; }

    }
}
