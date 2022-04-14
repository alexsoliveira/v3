using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Application.DTO;

namespace TGS.Cartorio.Application.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class CertificadoController : ControllerBase
    {
        private readonly ICertificadoAppService _certificadoAppService;

        public CertificadoController(ICertificadoAppService certificadoAppService)
        {
            _certificadoAppService = certificadoAppService;
        }
    }
}
