using System.Threading.Tasks;
using TGS.ConPay.API.DTO;
using TGS.Pagamento.API.DTO;
using TGS.Pagamento.API.ExternalServices.Conpay.Models;

namespace TGS.Pagamento.API.ExternalServices.Conpay.Interfaces
{
    public interface IConpayServices
    {
        Task<object> GerarBoleto(Boleto boleto);
        Task<object> ConsultarBoleto(string identifier);
        Task<object> SimularParcelas(decimal valorTotal);
        Task<ResponsePayCreditCard> PagarComCartaoCredito(CreditCard creditCard);
        Task<ResponseConsultaTransacao> ConsultarTransacaoCartaoCredito(string idTransacao);
    }
}
