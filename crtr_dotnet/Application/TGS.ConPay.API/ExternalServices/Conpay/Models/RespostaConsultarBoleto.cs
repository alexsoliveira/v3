using Newtonsoft.Json;

namespace TGS.Pagamento.API.ExternalServices.Conpay.Models
{
    public class RespostaConsultarBoleto
    {
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
