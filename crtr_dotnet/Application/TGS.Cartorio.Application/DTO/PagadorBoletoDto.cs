using Newtonsoft.Json;

namespace TGS.Cartorio.Application.DTO
{
    public class PagadorBoletoDto
    {
        public string Uf { get; set; }
        public string Cidade { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Nome { get; set; }
        public string NumeroDocumento { get; set; }
        public string Cep { get; set; }
    }
}
