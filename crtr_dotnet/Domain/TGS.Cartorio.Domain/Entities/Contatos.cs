using System;
using System.Collections.Generic;

namespace TGS.Cartorio.Domain.Entities
{
    public partial class Contatos
    {
        public Contatos()
        {
            CartoriosContatos = new HashSet<CartoriosContatos>();
            UsuariosContatos = new HashSet<UsuariosContatos>();
        }

        public int IdContato { get; set; }
        public string Conteudo { get; set; }
        public DateTime DataOperacao { get; set; }
        public long IdUsuario { get; set; }
        public bool? FlagAtivo { get; set; }

        public virtual Usuarios IdUsuarioNavigation { get; set; }
        public virtual ICollection<CartoriosContatos> CartoriosContatos { get; set; }
        public virtual ICollection<PessoasContatos> PessoasContatos { get; set; }
        public virtual ICollection<UsuariosContatos> UsuariosContatos { get; set; }
    }
}
