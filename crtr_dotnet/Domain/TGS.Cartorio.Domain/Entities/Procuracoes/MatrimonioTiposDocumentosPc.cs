using System.Collections.Generic;

namespace TGS.Cartorio.Domain.Entities.Procuracoes
{
    public partial class MatrimonioTiposDocumentosPc
    {
        public MatrimonioTiposDocumentosPc()
        {
            MatrimoniosDocumentos = new HashSet<MatrimoniosDocumentos>();
        }

        public int IdTipoDocumento { get; set; }
        public string Descricao { get; set; }
        public virtual ICollection<MatrimoniosDocumentos> MatrimoniosDocumentos { get; set; }
    }
}
