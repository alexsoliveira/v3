namespace TGS.Cartorio.Domain.Entities
{
    public partial class StatusSolicitacaoHeader
    {
        public long IdSolicitacao { get; set; }
        public int IdProduto { get; set; }
        public string Produto { get; set; }
        public string Solicitante { get; set; }
    }


}
