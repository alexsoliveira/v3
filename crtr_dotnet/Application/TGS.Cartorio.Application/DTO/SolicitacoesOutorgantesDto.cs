using System;
using System.Collections.Generic;
using System.Text;

namespace TGS.Cartorio.Application.DTO
{
    public class SolicitacoesOutorgantesDto
    {
        public long IdSolicitacao { get; set; }
        public int IdPessoaSolicitante { get; set; }
        public ICollection<OutorgantesDto> Outogantes { get; set; }
        public bool AlteracaoDaSolicitacao { get; set; }
    }
}
