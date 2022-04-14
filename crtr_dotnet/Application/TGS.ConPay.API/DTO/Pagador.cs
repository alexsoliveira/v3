namespace TGS.ConPay.API.DTO
{
    public class Pagador
    {
        public string numeroDocumento { get; set; }
        public string nome { get; set; }
        public string endereco { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }
        public string uf { get; set; }
        public string cep { get; set; }
    }
}
