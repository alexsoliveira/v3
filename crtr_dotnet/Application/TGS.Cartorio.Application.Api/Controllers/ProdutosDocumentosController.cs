using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Domain.Entities;


namespace TGS.Cartorio.Application.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class ProdutosDocumentosController : Controller
    {
        private readonly IProdutosDocumentosAppService _produtosDocumentosService;
        private readonly ILogger<ProdutosImagensController> _logger;

        public ProdutosDocumentosController(IProdutosDocumentosAppService produtosDocumentosService, ILogger<ProdutosImagensController> logger)
        {
            _produtosDocumentosService = produtosDocumentosService;
            _logger = logger;
        }
    }
}
