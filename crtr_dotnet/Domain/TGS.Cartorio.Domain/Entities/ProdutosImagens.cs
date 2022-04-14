using System;
using System.Collections.Generic;
using System.Text;

namespace TGS.Cartorio.Domain.Entities
{
    public partial class ProdutosImagens
    {
        public int IdProdutoImagem { get; set; }
        public int IdProduto { get; set; }
        public byte[] BlobConteudo { get; set; }
        public DateTime DataOperacao { get; set; }
        public long IdUsuario { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

        public string StrBlobConteudo
        {
            get
            {
                return Encoding.UTF8.GetString(this.BlobConteudo, 0, this.BlobConteudo.Length);
            }
        }


        public virtual Produtos IdProdutoNavigation { get; set; }
        public virtual Usuarios IdUsuarioNavigation { get; set; }
    }
}
