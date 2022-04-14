using System;
using System.Collections.Generic;

namespace TGS.Cartorio.Domain.Entities
{
    public partial class AssinaturaDigitalLog
    {
        public long IdAssinaturaDigitalLog { get; set; }
        public long IdSolicitacao { get; set; }
        public string Observacao { get; set; }
        public DateTime DataOperacao { get; set; }
        public long IdUsuario { get; set; }
        
        

        public virtual Solicitacoes IdSolicitacaoNavigation { get; set; }
        public virtual Usuarios IdUsuarioNavigation { get; set; }
    }
}
