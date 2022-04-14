using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TGS.Cartorio.Domain.Entities
{
    public partial class TiposFretesPc
    {
        public TiposFretesPc()
        {
            Solicitacoes = new HashSet<Solicitacoes>();
        }

        public int IdTipoFrete { get; set; }
        public string Descricao { get; set; }

        
        [NotMapped]
        public CustosFretes CustosFretes { get; set; }

        public virtual ICollection<Solicitacoes> Solicitacoes { get; set; }
    }
}
