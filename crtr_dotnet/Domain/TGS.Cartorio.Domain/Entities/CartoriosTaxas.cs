using System.Collections.Generic;

namespace TGS.Cartorio.Domain.Entities
{
    public partial class CartoriosTaxas
    {
        public CartoriosTaxas()
        {
            SolicitacoesTaxas = new HashSet<SolicitacoesTaxas>();
        }

        public long IdCartorioTaxa { get; set; }
        public int IdCartorio { get; set; }
        public int IdProdutoModalidade { get; set; }
        public int IdCartorioServico { get; set; }
        public decimal Valor { get; set; }

        public virtual Cartorios CartorioNavigation { get; set; }
        public virtual ProdutosModalidades ProdutoModalidadeNavigation { get; set; }
        public virtual ICollection<SolicitacoesTaxas> SolicitacoesTaxas { get; set; }
    }
}
