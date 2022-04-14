using System;

namespace TGS.Cartorio.Domain.Entities.Procuracoes
{
    public partial class MatrimoniosDocumentos
    {
        public MatrimoniosDocumentos() { }
        public MatrimoniosDocumentos(long idMatrimonio, long idProcuracaoParte, long idUsuario, int idTipoDocumento, byte[] blobConteudo) 
        {
            IdMatrimonio = idMatrimonio;
            IdProcuracaoParte = idProcuracaoParte;
            IdUsuario = idUsuario;
            IdTipoDocumento = idTipoDocumento;
            BlobConteudo = blobConteudo;
            DataOperacao = DateTime.Now;
        }
        public long IdMatrimonioDocumento { get; set; }
        public long IdMatrimonio { get; set; }
        public long? IdProcuracaoParte { get; set; }
        public int IdTipoDocumento { get; set; }
        public byte[] BlobConteudo { get; set; }
        public byte[] BlobAssinaturaDigital { get; set; }
        public DateTime DataOperacao { get; set; }
        public long IdUsuario { get; set; }
        public virtual Matrimonios IdMatrimonioNavigation { get; set; }
        public virtual MatrimonioTiposDocumentosPc IdTipoDocumentoNavigation { get; set; }
        public virtual ProcuracoesPartes IdProcuracaoParteNavigation { get; set; }
    }
}
