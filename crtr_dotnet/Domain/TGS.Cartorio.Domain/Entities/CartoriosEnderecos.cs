using System;
using System.Collections.Generic;

namespace TGS.Cartorio.Domain.Entities
{
    public partial class CartoriosEnderecos
    {
        public int IdCartorioEndereco { get; set; }
        public int IdCartorio { get; set; }
        public int IdEndereco { get; set; }
        public long IdUsuario { get; set; }
        public DateTime DataOperacao { get; set; }

        public virtual Cartorios IdCartorioNavigation { get; set; }
        public virtual Enderecos IdEnderecoNavigation { get; set; }
        public virtual Usuarios IdUsuarioNavigation { get; set; }
    }
}
