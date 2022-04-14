using System;
using System.Collections.Generic;

namespace TGS.Cartorio.Domain.Entities
{
    public partial class Enderecos
    {
        public Enderecos()
        {
            CartoriosEnderecos = new HashSet<CartoriosEnderecos>();
            PessoasEnderecos = new HashSet<PessoasEnderecos>();
        }

        public int IdEndereco { get; set; }
        public string Conteudo { get; set; }
        public DateTime DataOperacao { get; set; }
        public long IdUsuario { get; set; }
        public bool? FlagAtivo { get; set; }
                
        public virtual Usuarios IdUsuarioNavigation { get; set; }
        public virtual ICollection<CartoriosEnderecos> CartoriosEnderecos { get; set; }
        public virtual ICollection<PessoasEnderecos> PessoasEnderecos { get; set; }
    }
}
