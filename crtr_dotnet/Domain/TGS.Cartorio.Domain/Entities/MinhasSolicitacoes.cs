using System;
using System.Collections.Generic;
using System.Text;

namespace TGS.Cartorio.Domain.Entities
{
    public class MinhasSolicitacoes
    {
        public long IdSolicitacao { get; set; }
        public long? IdPessoaSolicitante { get; set; }
        public string Participacao { get; set; }
        public string Produto { get; set; }
        public string CamposPagamento { get; set; }
        public string Conteudo { get; set; }
        public DateTime? DataSolicitacao { get; set; }
        public string Estado { get; set; }
        public DateTime? UltimaInteracao { get; set; }
    }
}
