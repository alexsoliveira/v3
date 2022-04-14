using System;
using System.Threading.Tasks;
using TGS.ConPay.API.DTO;
using TGS.Pagamento.API.DTO;
using TGS.Pagamento.API.ExternalServices.Conpay.Interfaces;
using TGS.Pagamento.API.ExternalServices.Conpay.Models;

namespace TGS.Pagamento.API.ExternalServices.Conpay.Services
{
    public class ConpayServices : IConpayServices
    {
        private readonly IConpayHttpClient<RespostaNovoBoleto> _novoBoletoConpayHttpClient;
        private readonly IConpayHttpClient<RespostaSimuladorParcela> _simuladorParcelasHttpClient;
        private readonly IConpayHttpClient<RespostaConsultarBoleto> _consultarBoletoHttpClient;
        public ConpayServices(IConpayHttpClient<RespostaNovoBoleto> novoBoletoConpayHttpClient, 
                              IConpayHttpClient<RespostaSimuladorParcela> simuladorParcelasHttpClient,
                              IConpayHttpClient<RespostaConsultarBoleto> consultarBoletoHttpClient)
        {
            _novoBoletoConpayHttpClient = novoBoletoConpayHttpClient;
            _simuladorParcelasHttpClient = simuladorParcelasHttpClient;
            _consultarBoletoHttpClient = consultarBoletoHttpClient;
        }


        public async Task<object> GerarBoleto(Boleto boleto)
        {
            try
            {
                Resposta resposta = await _novoBoletoConpayHttpClient.Post("registers/", boleto);
                if (resposta.Sucesso)
                    return resposta.obj;
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<object> ConsultarBoleto(string identifier)
        {
            try
            {
                Resposta resposta = await _consultarBoletoHttpClient.Get($"bills/emission?identifier={identifier}");
                if (resposta.Sucesso)
                    return resposta.obj;
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<object> SimularParcelas(decimal valorTotal)
        {
            try
            {
                Resposta resposta = await _simuladorParcelasHttpClient.Post("interest-calculator", new RequisicaoSimuladorParcela(valorTotal));
                if (resposta.Sucesso)
                    return resposta.obj;
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponsePayCreditCard> PagarComCartaoCredito(CreditCard creditCard)
        {
            try
            {
                return await _novoBoletoConpayHttpClient.PagarComCartaoCredito(creditCard);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseConsultaTransacao> ConsultarTransacaoCartaoCredito(string idTransacao)
        {
            try
            {
                return await _novoBoletoConpayHttpClient.ConsultarTransacaoCartaoCredito(idTransacao);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
