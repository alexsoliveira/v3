using System;
using System.Collections.Generic;
using System.Text;

namespace TGS.Cartorio.Application.DTO
{
    public class MinhasSolicitacoesDto
    {
        public long IdSolicitacao { get; set; }
        public string Participacao { get; set; }
        public string Produto { get; set; }
        public string TipoPagamento { get; set; }
        public string EstadoPagamento { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public string Estado { get; set; }
        public DateTime? UltimaInteracao { get; set; }
    }
}
