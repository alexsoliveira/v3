using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Refit;
using TGS.Correios.API.DTO;
using TGS.Correios.API.Interfaces;

namespace TGS.Correios.API.Controllers
{

    [Route("api/correios")]
    public class CorreiosController : MainController
    {
        private readonly ICorreioService _correioService;

        public CorreiosController(ICorreioService correioService)
        {
            _correioService = correioService;
        }

        [HttpGet("ConsultarEndereco/{cep}")]
        public async Task<ActionResult> ConsultarEndereco(string cep)
        {
            try
            {
                var ret = await _correioService.ConsultarEnderecoAsync(cep);

                if (ret.Cep != null)
                    return Ok(ret);
                else
                {
                    AdicionarErroProcessamento($"Endereço não encontrado para o CEP {cep}");
                    return CustomResponse();
                }
            }
            catch (Exception ex)
            {
#if Debug

                AdicionarErroProcessamento($"Cep {cep} não é válido. \n\n{GetExceptionErrors(ex)}");
#else
                AdicionarErroProcessamento($"Cep não é válido");
#endif
                return CustomResponse();
            }
        }

        [HttpGet("ConsultarCEP/{uf}/{cidade}/{logradouro}")]
        public async Task<ActionResult> ConsultarCEP(string uf, string cidade, string logradouro)
        {
            try
            {
                var ret = await _correioService.ConsultarCEPAsync(uf, cidade, logradouro);

                if (ret.Count > 0)
                    return Ok(ret);
                else
                {
                    AdicionarErroProcessamento($"CEP não encontrado para o respectivo estado:{uf}, cidade:{cidade}, logradouro:{logradouro}");
                    return CustomResponse();
                }
            }
            catch (System.Exception ex)
            {
                AdicionarErroProcessamento($"Endereço não é válido {ex.Message}");
                return CustomResponse();
            }
        }
    }
}
