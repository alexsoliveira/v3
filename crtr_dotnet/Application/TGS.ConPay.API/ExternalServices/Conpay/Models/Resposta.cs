namespace TGS.Pagamento.API.ExternalServices.Conpay.Models
{
    public class Resposta
    {
        public bool NaoAutorizado { get; set; }
        public bool Sucesso { get; set; }
        public object obj { get; set; }
    }
}
