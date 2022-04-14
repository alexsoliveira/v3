namespace TGS.Cartorio.Domain.Entities
{
    public partial class SolicitacoesTaxas
    {
        public long IdSolicitacaoTaxa { get; set; }
        public long IdSolicitacao { get; set; }
        public long? IdCartorioTaxa { get; set; }
        public long? IdTaxaExtra { get; set; }
        public string CamposExtras { get; set; }

        public virtual Solicitacoes SolicitacoesNavigation { get; set; }
        public virtual TaxasExtras TaxasExtrasNavigation { get; set; }
        public virtual CartoriosTaxas CartoriosTaxasNavigation { get; set; }
    }
}
