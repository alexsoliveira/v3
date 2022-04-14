using System.Collections.Generic;

namespace TGS.Cartorio.Domain.Entities
{
    public partial class CartoriosServicosPc
    {
        public CartoriosServicosPc()
        {
            //CartoriosTaxas = new HashSet<CartoriosTaxas>();
        }

        public int IdCartorioServicos { get; set; }
        public string Descricao { get; set; }
        //public virtual ICollection<CartoriosTaxas> CartoriosTaxas { get; set; }
    }
}
