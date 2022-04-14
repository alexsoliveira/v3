using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TGS.Cartorio.Application.Api.Controllers.Base;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Application.DTO;

namespace TGS.Cartorio.Application.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AssinaturaDigitalController : MainController
    {
        private readonly IAssinaturaDigitalAppService _assinaturaDigitalAppService;
        private readonly ILogSistemaAppService _logSistemaAppService;

        public AssinaturaDigitalController(IAssinaturaDigitalAppService assinaturaDigitalAppService,
            ILogSistemaAppService logSistemaAppService)
        {
            _assinaturaDigitalAppService = assinaturaDigitalAppService;
            _logSistemaAppService = logSistemaAppService;
        }

        [HttpPost("AssinarPrimeiroPasso")]
        public async Task<IActionResult> AssinarPrimeiroPasso(eCertificadoDTO obj)
        {
            try
            {
                var docToSign = await _assinaturaDigitalAppService.AssinarDocumentoPrimeiroPasso(obj.IdMatrimonio, 
                    obj.IdPessoaSolicitante,
                    obj.IdUsuario,
                    obj.CertificadoBase64,
                    obj.DocumentoPDF);


                await _logSistemaAppService.Add(CodLogSistema.AssinaturaDigital_PrimeiroPasso,
                    new
                    {
                        Sucesso = true,
                        IdMatrimonio = obj.IdMatrimonio,
                        IdPessoaSolicitante = obj.IdPessoaSolicitante,
                        DocumentoPDF = obj.DocumentoPDF,
                        IdMatrimonioDocumento = docToSign.IdMatrimonioDocumento
                    });

                return Ok(docToSign);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_AssinaturaDigital_PrimeiroPasso,
                    new
                    {
                        Sucesso = false,
                        IdMatrimonio = obj.IdMatrimonio,
                        IdPessoaSolicitante = obj.IdPessoaSolicitante,
                        DocumentoPDF = obj.DocumentoPDF
                    }, ex);

                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }            
        }

        [HttpPost("AssinarSegundoPasso")]
        public async Task<IActionResult> AssinarSegundoPasso([FromBody] DadosAssinaturaSegundoPassoDto dadosAssinatura)
        {
            try
            {   
                var padesSignature = await _assinaturaDigitalAppService.AssinarDocumentoSegundoPasso(dadosAssinatura.Signature, 
                    dadosAssinatura.TransferData,
                    dadosAssinatura.IdMatrimonioDocumento);

                await _logSistemaAppService.Add(CodLogSistema.AssinaturaDigital_SegundoPasso,
                    new
                    {
                        Sucesso = true,
                        IdMatrimonioDocumento = dadosAssinatura.IdMatrimonioDocumento
                    });

                return Ok(new { Data = true });  
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_AssinaturaDigital_SegundoPasso,
                new {
                    Sucesso = false,
                    IdMatrimonioDocumento = dadosAssinatura.IdMatrimonioDocumento
                }, ex);

                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }

        [HttpPost("ValidacaoDocumento/{idMatrimonioDocumento:long}")]
        public async Task<IActionResult> ValidacaoDocumento(long idMatrimonioDocumento)
        {
            try
            {
                var documento = await _assinaturaDigitalAppService.ValidacaoDocumento(idMatrimonioDocumento);

                await _logSistemaAppService.Add(CodLogSistema.AssinaturaDigital_ValidacaoDocumento,
                    new
                    {
                        Sucesso = true,
                        IdMatrimonioDocumento = idMatrimonioDocumento
                    });

                return Ok(new { BlobAssinaturaDigital = documento.BlobAssinaturaDigital});
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_AssinaturaDigital_ValidacaoDocumento,
                    new
                    {
                        Sucesso = false,
                        IdMatrimonioDocumento = idMatrimonioDocumento
                    });

                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}
