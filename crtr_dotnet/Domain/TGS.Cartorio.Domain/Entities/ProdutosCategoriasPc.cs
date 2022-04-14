using System.Collections.Generic;

namespace TGS.Cartorio.Domain.Entities
{
    public partial class ProdutosCategoriasPc
    {
        public ProdutosCategoriasPc()
        {
            Produtos = new HashSet<Produtos>();
        }
        public int IdProdutoCategoria { get; set; }
        public string Descricao { get; set; }
        public string Titulo { get; set; }
        public virtual ICollection<Produtos> Produtos { get; set; }
        //public virtual ICollection<ProdutosModalidadesPc> Modalidades { get; set; }
    }
}
