namespace TGS.ConPay.API.DTO
{
    public class Client
    {
        /// <summary>
        /// Nome completo do comprador.
        /// </summary>
        public string fullName { get; set; }

        /// <summary>
        /// E-mail do comprador.
        /// </summary>
        public string email { get; set; }

        ///// <summary>
        ///// CPF ou CNPJ do comprador.
        ///// </summary>
        //public string documentNumber { get; set; }

        /// <summary>
        /// DDD telefone comprador.
        /// </summary>
        public string ddd { get; set; }

        /// <summary>
        /// Número do telefone do comprador.
        /// </summary>
        public string phoneNumber { get; set; }

        /// <summary>
        /// Endereço do comprador
        /// </summary>
        public Address address { get; set; }
    }
}
