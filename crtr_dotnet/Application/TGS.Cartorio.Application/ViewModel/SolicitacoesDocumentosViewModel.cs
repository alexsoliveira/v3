using System;
using System.Collections.Generic;
using System.Text;

namespace TGS.Cartorio.Application.ViewModel
{
    public class SolicitacoesDocumentosViewModel
    {
        public long IdSolicitacaoDocumento { get; set; }
        public long IdSolicitacao { get; set; }
        public byte[] BlobConteudo { get; set; }
        public int IdTipoDocumento { get; set; }
        public DateTime DataOperacao { get; set; }
        public long IdUsuario { get; set; }
        public byte[] BlobAssinaturaDigital { get; set; }
        public long? IdSolicitacaoParte { get; set; }
    }
}
