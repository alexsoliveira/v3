namespace TGS.ConPay.API.DTO
{
    public class Address
    {
        /// <summary>
        /// Nome da rua do comprador.
        /// </summary>
        public string street { get; set; }

        /// <summary>
        /// Número do endereço.
        /// </summary>
        public string number { get; set; }

        /// <summary>
        /// Complemento do endereço.
        /// </summary>
        public string complement { get; set; }

        /// <summary>
        /// CEP do endereço.
        /// </summary>
        public string postalCode { get; set; }

        /// <summary>
        /// Bairro.
        /// </summary>
        public string district { get; set; }

        /// <summary>
        /// Cidade.
        /// </summary>
        public string city { get; set; }

        /// <summary>
        /// UF.
        /// </summary>
        public string federationUnit { get; set; }
    }
}
