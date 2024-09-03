using CP4.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CP4.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
        public class ExchangeController : ControllerBase, IExchangeController
        {
            private readonly IExchangeRateService _service;

            public ExchangeController(IExchangeRateService service)
            {
                _service = service;
            }

            [HttpGet("usd-to-brl")]
            public async Task<IActionResult> GetUsdToBrl()
            {
                var rate = await _service.GetUsdToBrlRateAsync();
                return Ok(new { USD_To_BRL = rate });
            }
        }
    }
