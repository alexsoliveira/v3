using TGS.Cartorio.Domain.Entities;
namespace TGS.Cartorio.Application.DTO
{
    public class SolicitacoesDto
    {
        public long? IdSolicitacao { get; set; }
        public int IdSolicitacaoEstado { get; set; }
        public int IdProduto { get; set; }
        public long IdUsuario { get; set; }
        public long NumeroDocumento { get; set; }
        public int IdTipoDocumento { get; set; }
        public int IdGenero { get; set; }
        public int IdTipoParte { get; set; }
        public int IdPessoa { get; set; }
        public int IdCartorio { get; set; }
        public int? IdTipoFrete { get; set; }
        public decimal? ValorFrete { get; set; }
        public string Email { get; set; }
        public string NomePessoa { get; set; }
        public string NomeSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string RazaoSocial { get; set; }
        public string Conteudo { get; set; }
        public string CamposPagamento { get; set; }
        public SolicitacoesDocumentos SolicitacoesDocumentos { get; set; }
    }
}
