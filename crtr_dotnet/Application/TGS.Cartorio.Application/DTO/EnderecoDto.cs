using Newtonsoft.Json;
using System;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Application.DTO
{
    public class EnderecoDto
    {
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string Uf { get; set; }
        public string Ibge { get; set; }
        public string Gia { get; set; }
    }

    public class EnderecosDto
    {
        public int IdEndereco { get; set; }
        public EnderecoConteudoDto Conteudo { get; set; }
        public DateTime DataOperacao { get; set; }
        public long IdUsuario { get; set; }
        public int IdPessoa { get; set; }
        public bool? FlagAtivo { get; set; }

        public void SetDomainData(Enderecos endereco)
        {
            var enderecoConteudo = JsonConvert.DeserializeObject<EnderecoConteudoDto>(endereco.Conteudo);
            Conteudo = enderecoConteudo;
            IdEndereco = endereco.IdEndereco;
            FlagAtivo = endereco.FlagAtivo;
        }
    }

    public class EnderecoConteudoDto
    {
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string Uf { get; set; }
    }
}
