using System.Collections.Generic;

namespace TGS.Cartorio.Domain.Entities.Procuracoes
{
    public class SolicitacoesOutorgados
    {
        public long IdSolicitacao { get; set; }
        public ICollection<Outorgados> Outorgados { get; set; }
    }
}
