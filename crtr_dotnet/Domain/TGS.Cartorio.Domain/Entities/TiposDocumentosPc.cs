using System;
using System.Collections.Generic;

namespace TGS.Cartorio.Domain.Entities
{
    public partial class TiposDocumentosPc
    {
        public TiposDocumentosPc()
        {
            Pessoas = new HashSet<Pessoas>();
            ProdutosDocumentos = new HashSet<ProdutosDocumentos>();
            SolicitacoesDocumentos = new HashSet<SolicitacoesDocumentos>();
        }

        public int IdTipoDocumento { get; set; }
        public string Descricao { get; set; }
        public string Observacao { get; set; }

        public virtual ICollection<Pessoas> Pessoas { get; set; }
        public virtual ICollection<ProdutosDocumentos> ProdutosDocumentos { get; set; }
        public virtual ICollection<SolicitacoesDocumentos> SolicitacoesDocumentos { get; set; }
    }
}
