using Newtonsoft.Json;

namespace TGS.Pagamento.API.ExternalServices.Conpay.Models
{
    public class Pagador
    {
        [JsonProperty("uf")]
        public string Uf { get; set; }
        [JsonProperty("cidade")]
        public string Cidade { get; set; }
        [JsonProperty("endereco")]
        public string Endereco { get; set; }
        [JsonProperty("bairro")]
        public string Bairro { get; set; }
        [JsonProperty("nome")]
        public string Nome { get; set; }
        [JsonProperty("numeroDocumento")]
        public string NumeroDocumento { get; set; }
        [JsonProperty("cep")]
        public string Cep { get; set; }

    }
}
