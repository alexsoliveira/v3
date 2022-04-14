using System;
using System.Collections.Generic;

namespace TGS.Cartorio.Domain.Entities
{
    public partial class ProdutosModalidades
    {
        public ProdutosModalidades()
        {
            CartoriosTaxas = new HashSet<CartoriosTaxas>();
        }
        public int IdProdutoModalidade { get; set; }
        public int IdProduto { get; set; }
        public int IdModalidade { get; set; }
        public DateTime DataOperacao { get; set; }
        public long IdUsuario { get; set; }
        public string Conteudo { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

        public virtual ProdutosModalidadesPc IdModalidadeNavigation { get; set; }
        public virtual Produtos IdProdutoNavigation { get; set; }
        public virtual Usuarios IdUsuarioNavigation { get; set; }
        public virtual ICollection<CartoriosTaxas> CartoriosTaxas { get; set; }
    }
}
