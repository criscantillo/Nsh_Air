using Microsoft.AspNetCore.Mvc;
using Nsh_Air.Api.Models;
using Nsh_Air.Services;

namespace Nsh_Air.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;

        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpGet]
        [ActionName(nameof(GetCurrency))]
        public async Task<ActionResult<ExchangeRates>> GetCurrency()
        {
            ExchangeRates exchangeRates = await _currencyService.GetExchangeRates();
            return Ok(exchangeRates);
        }
    }
}
