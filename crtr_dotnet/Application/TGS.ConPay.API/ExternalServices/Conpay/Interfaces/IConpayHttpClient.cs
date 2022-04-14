using System.Threading.Tasks;
using TGS.ConPay.API.DTO;
using TGS.Pagamento.API.DTO;
using TGS.Pagamento.API.ExternalServices.Conpay.Models;

namespace TGS.Pagamento.API.ExternalServices.Conpay.Interfaces
{
    public interface IConpayHttpClient<TReturn>
    {
        Task<Resposta> Get(string path);
        Task<Resposta> Post(string path, object obj);
        Task<ResponsePayCreditCard> PagarComCartaoCredito(CreditCard creditCard);
        Task<ResponseConsultaTransacao> ConsultarTransacaoCartaoCredito(string idTransacao);
    }
}
