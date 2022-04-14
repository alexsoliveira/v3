using System;
using System.Collections.Generic;

namespace TGS.Cartorio.Domain.Entities
{
    public partial class SolicitacoesDocumentos
    {
        public long IdSolicitacaoDocumento { get; set; }
        public long IdSolicitacao { get; set; }
        public byte[] BlobConteudo { get; set; }
        public int IdTipoDocumento { get; set; }
        public DateTime DataOperacao { get; set; }
        public long IdUsuario { get; set; }
        public byte[] BlobAssinaturaDigital { get; set; }
        public long? IdSolicitacaoParte { get; set; }

        public virtual Solicitacoes IdSolicitacaoNavigation { get; set; }
        public virtual TiposDocumentosPc IdTipoDocumentoNavigation { get; set; }
        public virtual Usuarios IdUsuarioNavigation { get; set; }
    }
}
