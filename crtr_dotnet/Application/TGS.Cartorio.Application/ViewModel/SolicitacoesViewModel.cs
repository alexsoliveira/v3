using System;
using System.Collections.Generic;
using System.Text;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Application.ViewModel
{
    public class SolicitacoesViewModel
    {
        public long IdSolicitacao { get; set; }
        public int IdProduto { get; set; }
        public int IdSolicitacaoEstado { get; set; }
        public DateTime DataOperacao { get; set; }
        public long IdUsuario { get; set; }
        public int? IdCartorio { get; set; }
        public string CamposPagamento { get; set; }
        public decimal? ValorFrete { get; set; }
        public int? IdTipoFrete { get; set; }
        public virtual ICollection<SolicitacoesEstados> SolicitacoesEstados { get; set; }

    }
}
