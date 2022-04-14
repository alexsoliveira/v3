using System.Collections.Generic;

namespace TGS.Cartorio.Domain.Entities.Procuracoes
{
    public class SolicitacoesOutorgantes
    {
        public long IdSolicitacao { get; set; }
        public int IdPessoaSolicitante { get; set; }
        public ICollection<Outorgante> Outogantes { get; set; }
    }
}
