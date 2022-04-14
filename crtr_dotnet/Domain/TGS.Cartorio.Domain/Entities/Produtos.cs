using System;
using System.Collections.Generic;

namespace TGS.Cartorio.Domain.Entities
{
    public partial class Produtos
    {
        public Produtos()
        {
            ProdutosDocumentos = new HashSet<ProdutosDocumentos>();
            ProdutosImagens = new HashSet<ProdutosImagens>();
            ProdutosModalidades = new HashSet<ProdutosModalidades>();
            Solicitacoes = new HashSet<Solicitacoes>();
        }

        public int IdProduto { get; set; }
        public int IdProdutoCategoria { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataOperacao { get; set; }
        public long IdUsuario { get; set; }
        public string Campos { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public bool? FlagAtivo { get; set; }
        public string SubTitulo { get; set; }

        public virtual Usuarios IdUsuarioNavigation { get; set; }
        public virtual ProdutosCategoriasPc IdProdutoCategoriaNavigation { get; set; }
        public virtual ICollection<ProdutosDocumentos> ProdutosDocumentos { get; set; }
        public virtual ICollection<ProdutosImagens> ProdutosImagens { get; set; }
        public virtual ICollection<ProdutosModalidades> ProdutosModalidades { get; set; }
        public virtual ICollection<Solicitacoes> Solicitacoes { get; set; }
    }
}
