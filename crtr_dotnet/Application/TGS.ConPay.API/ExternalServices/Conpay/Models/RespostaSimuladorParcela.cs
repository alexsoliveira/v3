using Newtonsoft.Json;

namespace TGS.Pagamento.API.ExternalServices.Conpay.Models
{
    public class RespostaSimuladorParcela
    {
        [JsonProperty("amount")]
        public decimal ValorTotal { get; set; }
        [JsonProperty("installments")]
        public RespostaParcelas Parcelas { get; set; }
    }
}
