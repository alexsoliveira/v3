namespace TGS.Cartorio.Application.ViewModel
{
    public class EnderecoViewModel
    {
        public int IdEndereco { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string CEP { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string Cidade { get { return Localidade; } }
        public string UF { get; set; }
        public bool? FlagAtivo { get; set; }
    }
}
