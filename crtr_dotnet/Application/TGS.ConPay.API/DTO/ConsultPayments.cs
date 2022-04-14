namespace TGS.ConPay.API.DTO
{
    internal class ConsultPayments
    {
        /// <summary>
        /// Data inicial das transações.
        /// </summary>
        internal string startDate { get; set; }
        /// <summary>
        /// Data final das transação.
        /// </summary>
        internal string finalDate { get; set; }
        /// <summary>
        /// Status da transação, podendo ser PAID, CANCELED, DECLINED.
        /// </summary>
        internal string status { get; set; }
    }
}
