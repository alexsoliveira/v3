using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using TGS.Cartorio.Infrastructure.Utility.ApiClient;
using TGS.ConPay.API.DTO;
using TGS.Pagamento.API.Configuration;
using TGS.Pagamento.API.ExternalServices.Conpay.Interfaces;
using TGS.Pagamento.API.ExternalServices.Conpay.Models;
using TGS.Pagamento.API.Interfaces;

namespace TGS.Pagamento.API.Controllers.ConPay
{
    [Route("/api/v1/[controller]")]
    public class BoletoController : MainController
    {
        private readonly IConpayServices _conpayServices;

        public BoletoController(IConPay conpay, 
                                IOptions<SettingsConPay> settingsConPay,
                                IConpayServices conpayServices)
            : base(settingsConPay, conpay)
        {
            _conpayServices = conpayServices;
        }

        ///// <summary>
        ///// Efetua pagamento por boleto
        ///// </summary>
        ///// <param name="boleto"></param>
        ///// <returns></returns>
        //[HttpPost("PagamentoBoleto")]
        //public async Task<ActionResult> PagamentoBoleto(Billet boleto)
        //{
        //    await Autenticar();

        //    var guid = Guid.NewGuid();                 

        //    return Ok(await _conpay.RegisterBillet(boleto));

        //}

        /// <summary>
        /// Consulta informações do boleto
        /// </summary>
        /// <param name="identificadorBoleto">Número indentificador do boleto</param>
        /// <returns>
        /// RETORNO DO SERVIÇO DA CONPAY
        ///Status Descriação
        ///A	Boleto criado mas não registrado
        ///R	Boleto registrado
        ///P	Boleto pago
        ///V	Boleto vencido
        ///B	Boleto baixado
        /// </returns>
        [AllowAnonymous]
        [HttpPost("ConsultaBoletoIdentificador")]
        public async Task<IActionResult> ConsultaBoletoIdentificador(string identificadorBoleto)
        {
            try
            {
                var ret = await _conpayServices.ConsultarBoleto(identificadorBoleto);

                if (ret == null)
                    return NotFound("Identificador do boleto não foi localizado!");

                return Ok(ret);
            }
            catch (Exception ex)
            {
                return CustomExceptionResponseMessage(ex);
            }
        }

        /// <summary>
        /// Consulta informações do boleto
        /// </summary>
        /// <param name="nossoNumero">Número indentificador do boleto</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("ConsultaBoletoNossoNumero")]
        public async Task<ActionResult> ConsultaBoletoNossoNumero(string nossoNumero)
        {
            await Autenticar();

            return Ok(await _conpay.GetPayBillNumber(nossoNumero));
        }

        [Authorize]
        [HttpPost("NovoBoleto")]
        public async Task<IActionResult> NovoBoleto([FromBody] Boleto boleto)
        {
            try
            {
                var objRetorno = await _conpayServices.GerarBoleto(boleto);

                if (objRetorno is RespostaNovoBoleto)
                {
                    RespostaNovoBoleto resposta = (RespostaNovoBoleto)objRetorno;
                    if (resposta != null)
                        return Ok(new { Identifier = resposta.identifier });
                }
                else if (objRetorno is ErrorMessage)
                {
                    ErrorMessage erro = (ErrorMessage)objRetorno;
                    return BadRequest(erro.Erro);
                }

                return BadRequest("Ocorreu um erro ao tentar gerar o boleto!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
