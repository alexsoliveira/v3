using System;
using System.Collections.Generic;

namespace TGS.Cartorio.Domain.Entities
{
    public partial class ProdutosDocumentos
    {
        public int IdProdutoDocumentos { get; set; }
        public int IdTipoDocumento { get; set; }
        public DateTime DataOperacao { get; set; }
        public long IdUsuario { get; set; }
        public int IdProduto { get; set; }
        public bool? FlagAssinaturaDigital { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

        public virtual Produtos IdProdutoNavigation { get; set; }
        public virtual TiposDocumentosPc IdTipoDocumentoNavigation { get; set; }
        public virtual Usuarios IdUsuarioNavigation { get; set; }
    }
}
