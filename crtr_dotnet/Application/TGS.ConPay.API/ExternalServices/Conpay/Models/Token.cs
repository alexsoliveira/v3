namespace TGS.Pagamento.API.ExternalServices.Conpay.Models
{
    public class Token
    {
        public bool NaoAutorizado { get; set; }
        public bool Sucesso { get; set; }
        public object obj { get; set; }
    }
}
