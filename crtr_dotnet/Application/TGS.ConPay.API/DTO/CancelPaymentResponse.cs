namespace TGS.ConPay.API.DTO
{
    class CancelPaymentResponse
    {
        /// <summary>
        ///Identificador da transação.
        /// </summary>
        public string reference { get; set; }

        /// <summary>
        /// Situação da transação.
        ///     CANCELED: Indica que a cobrança foi cancelada.
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// Mensagem com detalhamento do resultado da transação..
        /// </summary>
        public string message { get; set; }
    }
}
