namespace TGS.ConPay.API.DTO
{
    public class ConsultPayBill
    {
        public string boletoId { get; set; }
        public string boletoDtCriacao { get; set; }
        public string nossoNumero { get; set; }
        public string status { get; set; }
        public string nsu { get; set; }
        public string dtNsu { get; set; }
        public string estacao { get; set; }
        public string situacao { get; set; }
        public string descricaoErro { get; set; }
        public string numeroConta { get; set; }
        public string agenciaConta { get; set; }
        public string numeroBanco { get; set; }
        public string digitoBanco { get; set; }
        public Convenio convenio { get; set; }
        public Pagador pagador { get; set; }
        public Titulo titulo { get; set; }
        public Split[] split { get; set; }
        public string urlNotificacao { get; set; }
        public string valorOriginalBoleto { get; set; }
        public string email { get; set; }
        public string[] emails { get; set; }
        public string nossoNumeroFormatado { get; set; }
        public bool cashIn { get; set; }
        public object statusCashIn { get; set; }


        public class Convenio
        {
            public string numeroAgencia { get; set; }
            public string numeroConta { get; set; }
            public string digitoConta { get; set; }
            public string codigoBanco { get; set; }
            public string codigoConvenio { get; set; }
        }

        public class Pagador
        {
            public string bairro { get; set; }
            public string cep { get; set; }
            public string cidade { get; set; }
            public string nome { get; set; }
            public string uf { get; set; }
            public string endereco { get; set; }
            public string numeroDocumento { get; set; }
        }

        public class Titulo
        {
            public string aceito { get; set; }
            public string nossoNumero { get; set; }
            public string seuNumero { get; set; }
            public string especie { get; set; }
            public string mensagem { get; set; }
            public string nomeAvalista { get; set; }
            public object pcIof { get; set; }
            public string agBeneficiario { get; set; }
            public string contaBeneficiario { get; set; }
            public string codigoBarra { get; set; }
            public string linhaDigitavel { get; set; }
            public string dataEmissao { get; set; }
            public object dataPagamento { get; set; }
            public string dataEntrada { get; set; }
            public string dataVencimento { get; set; }
            public string valorNominal { get; set; }
            public string quantidadeDiasBaixa { get; set; }
            public string numeroDocumentoAvalista { get; set; }
            public string imagemCodigoBarra { get; set; }
        }

        public class Split
        {
            public string cnpj { get; set; }
            public float valor { get; set; }
        }

    }
}
