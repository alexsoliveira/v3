using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TGS.ConPay.API.DTO;

namespace TGS.Pagamento.API.Interfaces
{
    public interface IConPay
    {
        /// <summary>
        /// Autenticação da API
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [Post("/v2/auth/token")]
        Task<HttpResponseMessage> Autenticar(Usuario usuario);
        
        /// <summary>
        /// Pagamento via cartão de crédito em um passo
        /// </summary>
        /// <param name="cartao"></param>
        /// <returns></returns>
        [Post("/ecommerce/charges/charge")]
        [Headers("Authorization: Bearer")]
        Task<ResponsePayCreditCard> PayCreditCard(CreditCard cartao);

        /// <summary>
        /// Buscar transação por código de referência
        /// </summary>
        /// <param name="IdTransacao"></param>
        /// <returns></returns>
        [Get("/ecommerce/charges/{IdTransacao}")]
        [Headers("Authorization: Bearer")]
        Task<ConsultPaymentResponse> GetPayCreditCard(string IdTransacao);

        /// <summary>
        /// Cancela transção de pagamento
        /// </summary>
        /// <param name="IdTransacao"></param>
        /// <returns></returns>
        [Post("/ecommerce/charges/cancel/{IdTransacao}")]
        [Headers("Authorization: Bearer")]
        Task<ConsultPaymentResponse> PayCancelCreditCard(string IdTransacao);








        /// <summary>
        /// Pagamento via boleto
        /// </summary>
        /// <param name="boleto"></param>
        /// <returns></returns>
        [Post("/registers/")]
        [Headers("Authorization: Bearer")]
        Task<object> RegisterBillet(Billet boleto);

        [Get("/bills/emission/")]
        [Headers("Authorization: Bearer")]
        Task<ConsultPayBill> GetPayBillIdentifier(string identifier);

        [Get("/bills/emission/{nossoNumero}")]
        [Headers("Authorization: Bearer")]
        Task<ConsultPayBill> GetPayBillNumber(string nossoNumero);

    }
}
