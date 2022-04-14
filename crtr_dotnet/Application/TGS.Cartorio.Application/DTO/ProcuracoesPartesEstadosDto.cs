using System;

namespace TGS.Cartorio.Application.DTO
{
    public class ProcuracoesPartesEstadosDto
    {
        public long IdProcuracaoParteEstado { get; set; }
        public long IdProcuracaoParte { get; set; }
        public long IdProcuracaoParteEstadoPc { get; set; }
        public DateTime DataOperacao { get; set; }
    }
}
