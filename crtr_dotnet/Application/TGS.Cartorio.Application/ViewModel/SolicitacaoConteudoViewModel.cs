using Newtonsoft.Json;
using System;
using TGS.Cartorio.Application.Enumerables;

namespace TGS.Cartorio.Application.ViewModel
{
    public class SolicitacaoConteudoViewModel
    {
        public string TipoPagamentoAtual { get; set; }
        public string EstadoPagamento { get; set; }
        public string InformacoesImportantes { get; set; }
        public string RepresentacaoPartes { get; set; }
        public DateTime? DataTermoDeAceite { get; set; }
        public bool AceitouTermoDeAceite { get; set; }

        public string PreencherComRetornoSerializado(TiposPagamento tipoPagamento, EstadosPagamento estado)
        {
            TipoPagamentoAtual = tipoPagamento.ToString();
            EstadoPagamento = estado.ToString();
            return JsonConvert.SerializeObject(this);
        }

        public EstadosPagamento GetEstadoPagamentoCartaoCredito(string status)
        {
            try
            {
                switch (status)
                {
                    case "PAID":
                        return EstadosPagamento.Aprovado;
                    case "AUTHORIZED":
                        return EstadosPagamento.PreAprovado;
                    case "DECLINED":
                        return EstadosPagamento.Recusado;
                    case "CANCELED":
                        return EstadosPagamento.Cancelado;
                    default:
                        return EstadosPagamento.StatusNaoReconhecido;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
