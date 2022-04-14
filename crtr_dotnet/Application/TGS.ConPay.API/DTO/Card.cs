namespace TGS.ConPay.API.DTO
{
    public class Card
    {
        /// <summary>
        /// Número do cartão.
        /// </summary>
        public string number { get; set; }

        /// <summary>
        /// Mês de expiração do cartão.
        /// </summary>
        public string exp_month { get; set; }

        /// <summary>
        /// Ano de expiração do cartão.
        /// </summary>
        public string exp_year { get; set; }

        /// <summary>
        /// Código de segurança do cartão.
        /// </summary>
        public string security_code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Holder holder { get; set; }
    }
}
