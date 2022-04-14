using Newtonsoft.Json;
using System;
using TGS.Cartorio.Application.DTO.Pagamento;
using TGS.Cartorio.Application.Enumerables;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Application.ViewModel
{
    public class CamposPagamentoViewModel
    {
        public string Identifier { get; set; }
        public string TipoPagamento { get; set; }
        public string Status { get; set; }
        public string DataOperacao { get; set; }
        public string DataVencimentoBoleto { get; set; }
        public string DataConfirmacaoPagamentoBoleto { get; set; }
        public decimal Valor { get; set; }
        public int NumeroParcelas { get; set; }
        public string DadosDonoCartao { get; set; }
        public bool Ativo { get; set; }

        public string PreencherBoletoRetornoSerializado(string identifier, decimal valor)
        {
            var dataOperacao = DateTime.Now.ToString("yyyy-MM-dd");
            TipoPagamento = TiposPagamento.Boleto.ToString();
            Identifier = identifier;
            DataOperacao = dataOperacao;
            DataVencimentoBoleto = dataOperacao;
            Valor = valor;
            Ativo = true;
            return JsonConvert.SerializeObject(this);
        }

        public string PreencherCartaoCreditoSerializado(RespostaPagamentoCartaoCreditoDto dadosPagamento, CartaoCreditoDto cartao)
        {
            var dataOperacao = DateTime.Now.ToString("yyyy-MM-dd");
            TipoPagamento = TiposPagamento.CartaoCredito.ToString();
            NumeroParcelas = cartao.NumeroParcelas;
            DadosDonoCartao = JsonConvert.SerializeObject(cartao.Cliente);
            Identifier = dadosPagamento.Referencia;
            Status = dadosPagamento.Status;
            DataOperacao = dataOperacao;
            Valor = cartao.ValorTotal;
            Ativo = true;
            return JsonConvert.SerializeObject(this);
        }

        public bool IsBoletoDeHoje()
        {
            return DateTime.TryParse(DataVencimentoBoleto, out DateTime dataVencimentoBoleto)
                    && !string.IsNullOrEmpty(Identifier)
                    && dataVencimentoBoleto.ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd");
        }
    }
}
