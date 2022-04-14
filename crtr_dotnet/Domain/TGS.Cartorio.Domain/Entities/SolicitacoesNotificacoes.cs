using System;
using System.Collections.Generic;

namespace TGS.Cartorio.Domain.Entities
{
    public partial class SolicitacoesNotificacoes
    {
        public long IdSolicitacaoNotificacao { get; set; }
        public long IdSolicitacao { get; set; }
        public string Titulo { get; set; }
        public string Conteudo { get; set; }
        public DateTime DataOperacao { get; set; }
        public long IdUsuario { get; set; }
        public int IdSolicitacaoNotificacaoEstado { get; set; }

        public virtual Solicitacoes IdSolicitacaoNavigation { get; set; }
        public virtual SolicitacoesNotificacoesEstadosPc IdSolicitacaoNotificacaoEstadoNavigation { get; set; }
        public virtual Usuarios IdUsuarioNavigation { get; set; }
    }
}
