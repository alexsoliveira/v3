using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TGS.Comunicacao.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly ILogger<SMSController> _logger;
        public EmailController(ILogger<SMSController> logger)
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
