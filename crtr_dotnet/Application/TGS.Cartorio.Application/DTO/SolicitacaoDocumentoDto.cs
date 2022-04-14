using System;
using System.Collections.Generic;
using System.Text;

namespace TGS.Cartorio.Application.DTO
{
    public class SolicitacaoDocumentoDto
    {
        public byte[] BlobConteudo { get; set; }
        public int IdTipoDocumento { get; set; }        
        public byte[] BlobAssinaturaDigital { get; set; }
    }
}
