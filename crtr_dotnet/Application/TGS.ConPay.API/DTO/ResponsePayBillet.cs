namespace TGS.ConPay.API.DTO
{
    public class ResponsePayBillet
    {
        public string dataCriacaoBoleto { get; set; }
        public string status { get; set; }
        public string situacao { get; set; }
        public string descricaoErro { get; set; }
        public string codigoBanco { get; set; }
        public string codigoConvenio { get; set; }
        public string numeroDocumento { get; set; }
        public string nomePagador { get; set; }
        public string enderecoPagador { get; set; }
        public string bairroPagador { get; set; }
        public string cidadePagador { get; set; }
        public string ufPagador { get; set; }
        public string cepPagador { get; set; }
        public string codigoBarra { get; set; }
        public string linhaDigitavel { get; set; }
        public string dataEntrada { get; set; }
        public string aceito { get; set; }
        public long nossoNumero { get; set; }
        public long seuNumero { get; set; }
        public string dataVencimento { get; set; }
        public string dataEmissao { get; set; }
        public string dataPagamento { get; set; }
        public string especie { get; set; }
        public string valorNominal { get; set; }
        public string valorAbatimento { get; set; }
        public string quantidadeDiasBaixa { get; set; }
        public string numeroDocumentoAvalista { get; set; }
        public string nomeAvalista { get; set; }
        public string mensagem { get; set; }
        public string agenciaBeneficiario { get; set; }
        public string contaBeneficiario { get; set; }
        public string urlNotificacao { get; set; }
        public string valorOriginalBoleto { get; set; }
        public string email { get; set; }
        public string[] emails { get; set; }
        public string identifier { get; set; }
    }
}
