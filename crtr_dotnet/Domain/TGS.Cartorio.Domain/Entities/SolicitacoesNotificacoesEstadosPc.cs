using System;
using System.Collections.Generic;
using System.Text;

namespace TGS.Cartorio.Domain.Entities
{
    public partial class SolicitacoesNotificacoesEstadosPc
    {
        public SolicitacoesNotificacoesEstadosPc()
        {
            SolicitacoesNotificacoes = new HashSet<SolicitacoesNotificacoes>();
        }

        public int IdSolicitacaoNotificacaoEstado { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<SolicitacoesNotificacoes> SolicitacoesNotificacoes { get; set; }
    }
}
