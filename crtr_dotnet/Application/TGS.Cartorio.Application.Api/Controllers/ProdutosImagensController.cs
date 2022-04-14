using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Domain.Entities;


namespace TGS.Cartorio.Application.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutosImagensController : Controller
    {
        private readonly IProdutosImagensAppService _produtoImagemService;
        private readonly ILogger<ProdutosImagensController> _logger;

        public ProdutosImagensController(IProdutosImagensAppService produtoImagemService, ILogger<ProdutosImagensController> logger)
        {
            _produtoImagemService = produtoImagemService;
            _logger = logger;
        }
    }
}
