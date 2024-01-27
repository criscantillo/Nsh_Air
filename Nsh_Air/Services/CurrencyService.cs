using Nsh_Air.Api.Models;
using RestSharp;

namespace Nsh_Air.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly IRestClient _restClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<NewShFlightService> _logger;

        public CurrencyService(
            IConfiguration configuration, 
            ILogger<NewShFlightService> logger)
        {
            _restClient = new RestClient();
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<ExchangeRates> GetExchangeRates()
        {
            string freeCurrencyApi = _configuration.GetValue<string>("FreeCurrencyApi");
            RestRequest request = new RestRequest(freeCurrencyApi);

            try
            {
                ExchangeRates exchangeRates =
                    await _restClient.GetAsync<ExchangeRates>(request) ?? new ExchangeRates();

                return exchangeRates;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las tasas de cambio {freeCurrencyApi}", freeCurrencyApi);
                return new ExchangeRates();
            }
        }
    }
}
