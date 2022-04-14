using System.Collections.Generic;

namespace TGS.Cartorio.Application.DTO
{
    public class SolicitacoesOutorgadosDto
    {
        public long? IdSolicitacao { get; set; }
        public string RepresentacaoPartes { get; set; }
        public ICollection<OutorgadoDto> Outorgados { get; set; }
        public bool AlteracaoDaSolicitacao { get; set; }
    }
}
