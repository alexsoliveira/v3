using System;
using System.Collections.Generic;
using System.Text;

namespace TGS.Cartorio.Domain.Entities
{

    public partial class CartoriosEstadosPc
    {

        public CartoriosEstadosPc()
        {
            Cartorios = new HashSet<Cartorios>();
        }

        public int IdCartorioEstado { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<Cartorios> Cartorios { get; set; }
    }
}
