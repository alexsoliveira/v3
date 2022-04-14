using Newtonsoft.Json;

namespace TGS.Pagamento.API.ExternalServices.Conpay.Models
{
    public class ParcelaDados
    {
        public ParcelaDados() { }
        public ParcelaDados(decimal valorParcela, decimal juros) 
        {
            ValorParcela = valorParcela;
            Juros = juros;
        }

        [JsonProperty("calculatedAmount")]
        public decimal ValorParcela { get; set; }
        [JsonProperty("fee")]
        public decimal Juros { get; set; }
    }
}
