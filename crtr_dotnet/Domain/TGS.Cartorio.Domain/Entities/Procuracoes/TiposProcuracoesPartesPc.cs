using System.Collections.Generic;

namespace TGS.Cartorio.Domain.Entities.Procuracoes
{
    public class TiposProcuracoesPartesPc
    {
        public TiposProcuracoesPartesPc()
        {
            ProcuracoesPartes = new HashSet<ProcuracoesPartes>();
        }
        public int IdTipoProcuracaoParte { get; set; }
        public string Descricao { get; set; }
        public virtual ICollection<ProcuracoesPartes> ProcuracoesPartes { get; set; }
}
}
