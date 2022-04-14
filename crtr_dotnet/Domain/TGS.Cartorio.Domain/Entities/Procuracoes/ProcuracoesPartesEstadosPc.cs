using System.Collections.Generic;

namespace TGS.Cartorio.Domain.Entities.Procuracoes
{
    public partial class ProcuracoesPartesEstadosPc
    {
        public ProcuracoesPartesEstadosPc()
        {
            ProcuracoesPartes = new HashSet<ProcuracoesPartes>();
            ProcuracoesPartesEstados = new HashSet<ProcuracoesPartesEstados>();
        }
        public int IdProcuracaoParteEstado { get; set; }
        public string Descricao { get; set; }
        public int NuOrdem { get; set; }
        public bool FlagAtivo { get; set; }
        public virtual ICollection<ProcuracoesPartes> ProcuracoesPartes { get; set; }
        public virtual ICollection<ProcuracoesPartesEstados> ProcuracoesPartesEstados { get; set; }
    }
}
