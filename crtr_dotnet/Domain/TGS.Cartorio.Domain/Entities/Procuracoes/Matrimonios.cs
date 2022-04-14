using System;
using System.Collections.Generic;

namespace TGS.Cartorio.Domain.Entities.Procuracoes
{
    public partial class Matrimonios
    {
        public Matrimonios()
        {
            MatrimoniosDocumentos = new HashSet<MatrimoniosDocumentos>();
        }
        public long IdMatrimonio { get; set; }
        public long IdSolicitacao { get; set; }
        public string CamposJson { get; set; }
        public DateTime DataOperacao { get; set; }
        public long IdUsuario { get; set; }
        public virtual Solicitacoes IdSolicitacaoNavigation { get; set; }
        public virtual ICollection<MatrimoniosDocumentos> MatrimoniosDocumentos { get; set; }
    }
}
