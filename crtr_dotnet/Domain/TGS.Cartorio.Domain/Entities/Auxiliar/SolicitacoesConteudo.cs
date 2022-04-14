using System;

namespace TGS.Cartorio.Domain.Entities.Auxiliar
{
    public class SolicitacoesConteudo
    {
        public string TipoPagamentoAtual { get; set; }
        public string EstadoPagamento { get; set; }
        public string InformacoesImportantes { get; set; }
        public string RepresentacaoPartes { get; set; }
        public DateTime? DataTermoDeAceite { get; set; }
        public bool AceitouTermoDeAceite { get; set; }

        
    }
}
