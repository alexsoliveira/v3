using System;
using System.Collections.Generic;

namespace TGS.Cartorio.Domain.Entities
{
    public partial class SolicitacoesEstadosPc
    {
        public SolicitacoesEstadosPc()
        {
            Solicitacoes = new HashSet<Solicitacoes>();
        }

        public int IdSolicitacaoEstado { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<Solicitacoes> Solicitacoes { get; set; }
        public virtual ICollection<SolicitacoesEstados> SolicitacoesEstados { get; set; }
    }
}
