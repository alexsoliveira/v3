using System;
using System.Collections.Generic;

namespace TGS.Cartorio.Domain.Entities
{
    public partial class UsuariosContatos
    {
        public long IdUsuarioContato { get; set; }
        public int IdContato { get; set; }
        public DateTime DataOperacao { get; set; }
        public long IdUsuario { get; set; }

        public virtual Contatos IdContatoNavigation { get; set; }
        public virtual Usuarios IdUsuarioNavigation { get; set; }
    }
}
