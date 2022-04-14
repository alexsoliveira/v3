using System.Collections.Generic;

namespace TGS.Cartorio.Domain.Entities
{
    public partial class TiposTaxasPc
    {
        public TiposTaxasPc()
        {
            TaxasExtras = new HashSet<TaxasExtras>();
        }
        public int IdTipoTaxa { get; set; }
        public string Descricao { get; set; }
        public virtual ICollection<TaxasExtras> TaxasExtras { get; set; }
    }
}
