using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TGS.Comunicacao.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SMSController : ControllerBase
    {
        private readonly ILogger<SMSController> _logger;
        public SMSController(ILogger<SMSController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return null;
        }
    }
}
