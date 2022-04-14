using System;
using System.Collections.Generic;

namespace TGS.Cartorio.Domain.Entities
{
    public partial class CartoriosModalidadesPc
    {
        public CartoriosModalidadesPc()
        {
            Cartorios = new HashSet<Cartorios>();
        }

        public int IdCartorioModalidade { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<Cartorios> Cartorios { get; set; }
    }
}
