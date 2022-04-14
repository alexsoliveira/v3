using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Infrastructure.Utility.ApiClient;
using TGS.ConPay.API.DTO;
using TGS.Pagamento.API.Configuration;
using TGS.Pagamento.API.DTO;
using TGS.Pagamento.API.ExternalServices.Conpay.Interfaces;
using TGS.Pagamento.API.ExternalServices.Conpay.Models;
using TGS.Pagamento.API.Interfaces;

namespace TGS.Pagamento.API.Controllers.ConPay
{
    [Route("/api/v1/[controller]")]
    [Authorize]
    public class CartaoCreditoController : MainController
    {
        private readonly IConpayServices _conpayServices;

        public CartaoCreditoController(IConPay conpay, 
                                IOptions<SettingsConPay> settingsConPay,
                                IConpayServices conpayServices)
            : base(settingsConPay, conpay)
        {
            _conpay = conpay;
            _conpayServices = conpayServices;
        }

        /// <summary>
        /// Efetua o pagamento no gateway de pagamentos via cartao de crédito
        /// </summary>
        /// <param name="cartao"></param>
        /// <returns>Retorna informações do tipo ResponsePayCreditCard em caso de sucesso</returns>     
        [HttpPost("PagamentoCartaoCredito")]
        public async Task<IActionResult> PagamentoCartaoCredito(CreditCard cartao)
        {
            try
            {
                var response = await _conpayServices.PagarComCartaoCredito(cartao);
                if (response.Success)
                    return Ok(response);

                return StatusCode(400, response);
            }
            catch (Exception ex)
            {
                return CustomExceptionResponseMessage(ex);
            }
        }

        /// <summary>
        /// Consulta trasação efetuada da compra 
        /// </summary>
        /// <param name="IdTransacao">Código da transação da compra</param>
        /// <returns>Retorna dados do tipo ConsultPaymentResponse em caso de sucesso</returns>
        [HttpPost("ConsultarTransacaoCartaoCredito")]
        public async Task<ActionResult> ConsultarTransacaoCartaoCredito(TransacaoDto transacao)
        {
            //await Autenticar();

            return Ok(await _conpayServices.ConsultarTransacaoCartaoCredito(transacao.idTransacao));
        }

        [HttpPost("CancelarTransacaoCartaoCredito")]
        public async Task<ActionResult> CancelarTransacaoCartaoCredito(string IdTransacao)
        {
            await Autenticar();

            return Ok(await _conpay.PayCancelCreditCard(IdTransacao));
        }

        [HttpPost("SimularParcelamento")]
        public async Task<IActionResult> SimularParcelamento([FromBody] SimulacaoParcelamentoDto simulacao)
        {
            object objRetorno = null;
            try
            {
                objRetorno = await _conpayServices.SimularParcelas(simulacao.ValorTotal);
                if (objRetorno == null)
                    return BadRequest("Não houve resposta do serviço!");
            }
            catch (Exception ex)
            {
                return CustomExceptionResponseMessage(ex);
            }
            
            try
            {
                if (objRetorno is RespostaSimuladorParcela)
                {
                    RespostaSimuladorParcela resposta = (RespostaSimuladorParcela)objRetorno;
                    if (resposta != null)
                    {
                        var simulador = new SimuladorParcelas(resposta);
                        return Ok(simulador);
                    }   
                }
                else if (objRetorno is ErrorMessage)
                {
                    ErrorMessage erro = (ErrorMessage)objRetorno;
                    return BadRequest(erro.Erro);
                }

                return StatusCode(500, new Retorno<string>("Ocorreu um erro ao tentar simular as parcelas!"));
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                    ex = new Exception(ex.Message, new Exception("Retorno CONPAY:  " 
                        + JsonConvert.SerializeObject(objRetorno)));
                else
                    ex = new Exception(ex.Message, new Exception("Retorno CONPAY:  " 
                        + JsonConvert.SerializeObject(objRetorno), ex.InnerException));
                
                return CustomExceptionResponseMessage(ex);
            }
        }

        private SimuladorParcelas MockParcelamento(decimal valorTotal)
        {
            return new SimuladorParcelas
            {
                ValorTotal = valorTotal,
                Parcelas = new List<Parcela>
                {
                   GetMockParcela(1, valorTotal),
                   GetMockParcela(2, valorTotal),
                   GetMockParcela(3, valorTotal),
                   GetMockParcela(4, valorTotal),
                   GetMockParcela(5, valorTotal),
                   GetMockParcela(6, valorTotal),
                   GetMockParcela(7, valorTotal),
                   GetMockParcela(8, valorTotal),
                   GetMockParcela(9, valorTotal),
                   GetMockParcela(10, valorTotal),
                   GetMockParcela(11, valorTotal),
                   GetMockParcela(12, valorTotal)
                }
            };
        }

        private Parcela GetMockParcela(int parcela, decimal valorTotal)
        {
            switch (parcela)
            {
                case 1:
                    return new Parcela(1, CalcularJuros(valorTotal, 2.35), (decimal)2.35);
                case 2:
                    return new Parcela(2, CalcularJuros(valorTotal / 2, 2.35), (decimal)2.35);
                case 3:
                    return new Parcela(3, CalcularJuros(valorTotal / 3, 2.35), (decimal)2.35);
                case 4:
                    return new Parcela(4, CalcularJuros(valorTotal / 4, 2.35), (decimal)2.35);
                case 5:
                    return new Parcela(5, CalcularJuros(valorTotal / 5, 3.09), (decimal)3.09);
                case 6:
                    return new Parcela(6, CalcularJuros(valorTotal / 6, 3.09), (decimal)3.09);
                case 7:
                    return new Parcela(7, CalcularJuros(valorTotal / 7, 3.09), (decimal)3.09);
                case 8:
                    return new Parcela(8, CalcularJuros(valorTotal / 8, 3.09), (decimal)3.09);
                case 9:
                    return new Parcela(9, CalcularJuros(valorTotal / 9, 4.38), (decimal)4.38);
                case 10:
                    return new Parcela(10, CalcularJuros(valorTotal / 10, 4.38), (decimal)4.38);
                case 11:
                    return new Parcela(11, CalcularJuros(valorTotal / 11, 4.38), (decimal)4.38);
                case 12:
                    return new Parcela(12, CalcularJuros(valorTotal / 12, 4.38), (decimal)4.38);
            }

            return null;
        }

        private decimal CalcularJuros(decimal valorTotal, double juros)
        {
            return valorTotal + (valorTotal * ((decimal)juros) / 100);
        }
    }
}
