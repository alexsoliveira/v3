using System;
using System.Collections.Generic;

namespace TGS.Cartorio.Domain.Entities
{
    public partial class CartoriosContatos
    {
        public int IdCartorioContato { get; set; }
        public int IdCartorio { get; set; }
        public int IdContato { get; set; }
        public long IdUsuario { get; set; }
        public DateTime? DataOperacao { get; set; }

        public virtual Cartorios IdCartorioNavigation { get; set; }
        public virtual Contatos IdContatoNavigation { get; set; }
        public virtual Usuarios IdUsuarioNavigation { get; set; }
    }
}
