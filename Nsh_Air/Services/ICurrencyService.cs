using Nsh_Air.Api.Models;

namespace Nsh_Air.Services
{
    public interface ICurrencyService
    {
        Task<ExchangeRates> GetExchangeRates();
    }
}
