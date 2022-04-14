namespace TGS.Cartorio.Application.DTO
{
    public class SolicitacoesTaxasDto
    {
        public long IdSolicitacaoTaxa { get; set; }
        public long IdSolicitacao { get; set; }
        public long? IdCartorioTaxa { get; set; }
        public long? IdTaxaExtra { get; set; }
        public string CamposExtras { get; set; }
        public decimal? ValorTaxa { get; set; }
    }
}
