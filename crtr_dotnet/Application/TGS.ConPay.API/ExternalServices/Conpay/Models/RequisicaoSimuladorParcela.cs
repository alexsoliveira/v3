using Newtonsoft.Json;

namespace TGS.Pagamento.API.ExternalServices.Conpay.Models
{
    public class RequisicaoSimuladorParcela
    {
        public RequisicaoSimuladorParcela(decimal valorTotal)
        {
            ValorTotal = valorTotal;
        }
        [JsonProperty("amount")]
        public decimal ValorTotal { get; set; }
    }
}
