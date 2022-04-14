namespace TGS.Cartorio.Application.DTO
{
    public class StatusSolicitacaoHeaderDto
    {
        public int? IdSolicitacao { get; set; }
        public int IdProduto { get; set; }
        public string Produto { get; set; }
        public string Solicitante { get; set; }
    }
}
