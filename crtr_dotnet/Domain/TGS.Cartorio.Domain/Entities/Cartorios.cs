using System;
using System.Collections.Generic;

namespace TGS.Cartorio.Domain.Entities
{
    public partial class Cartorios
    {
        public Cartorios()
        {
            CartoriosContatos = new HashSet<CartoriosContatos>();
            CartoriosEnderecos = new HashSet<CartoriosEnderecos>();
            Solicitacoes = new HashSet<Solicitacoes>();
            CartoriosTaxas = new HashSet<CartoriosTaxas>();
        }

        public int IdCartorio { get; set; }
        public int IdCartorioModalidade { get; set; }
        public int IdCartorioEstado { get; set; }
        public long IdPessoa { get; set; }
        public DateTime DataOperacao { get; set; }
        public long IdUsuario { get; set; }
        
        

        public virtual CartoriosEstadosPc IdCartorioEstadoNavigation { get; set; }
        public virtual CartoriosModalidadesPc IdCartorioModalidadeNavigation { get; set; }
        public virtual Pessoas IdPessoaNavigation { get; set; }
        public virtual Usuarios IdUsuarioNavigation { get; set; }
        public virtual ICollection<CartoriosContatos> CartoriosContatos { get; set; }
        public virtual ICollection<CartoriosEnderecos> CartoriosEnderecos { get; set; }
        public virtual ICollection<Solicitacoes> Solicitacoes { get; set; }
        public virtual ICollection<CartoriosTaxas> CartoriosTaxas { get; set; }
    }
}
