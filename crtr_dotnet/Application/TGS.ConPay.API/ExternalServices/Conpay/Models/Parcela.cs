namespace TGS.Pagamento.API.ExternalServices.Conpay.Models
{
    public class Parcela
    {
        public Parcela() { }
        public Parcela(int numero, decimal valorParcela, decimal juros)
        {
            Numero = numero;
            ValorParcela = valorParcela;
            Juros = juros;
        }
        public int Numero { get; set; }
        public decimal ValorParcela { get; set; }
        public decimal Juros { get; set; }
    }
}
