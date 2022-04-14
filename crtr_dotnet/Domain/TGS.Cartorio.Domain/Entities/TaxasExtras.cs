using System;
using System.Collections.Generic;

namespace TGS.Cartorio.Domain.Entities
{
    public partial class TaxasExtras
    {
        public TaxasExtras()
        {
            SolicitacoesTaxas = new HashSet<SolicitacoesTaxas>();
        }

        public long IdTaxaExtra { get; set; }
        public int IdTipoTaxa { get; set; }
        public decimal Valor { get; set; }
        public string CamposExtras { get; set; }
        public long IdUsuario { get; set; }
        public DateTime DataOperacao { get; set; }
        public bool FlagAtivo { get; set; }

        public virtual Usuarios UsuarioNavigation { get; set; }
        public virtual TiposTaxasPc TiposTaxasPcNavigation { get; set; }
        public virtual ICollection<SolicitacoesTaxas> SolicitacoesTaxas { get; set; }
    }
}
