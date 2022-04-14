using System.Collections.Generic;
using TGS.Pagamento.API.DTO;

namespace TGS.ConPay.API.DTO
{
    public class CreditCard
    {
        /// <summary>
        /// Quantidade de parcelas da transação.
        /// </summary>
        public int installments { get; set; }

        /// <summary>
        /// Valor da transação.
        /// </summary>
        public float value { get; set; }

        /// <summary>
        /// Dados do cartão do comprador.
        /// </summary>
        public Card card { get; set; }

        /// <summary>
        /// Dados do comprador.
        /// </summary>
        public Client client { get; set; }
    }
}
