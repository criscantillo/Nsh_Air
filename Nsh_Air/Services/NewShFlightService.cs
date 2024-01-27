using Nsh_Air.Domain;
using RestSharp;

namespace Nsh_Air.Services
{
    public class NewShFlightService : IFlightService
    {
        private readonly IRestClient _restClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<NewShFlightService> _logger;

        public NewShFlightService(
            IConfiguration configuration, 
            ILogger<NewShFlightService> logger)
        {
            _restClient = new RestClient();
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<IList<FlightDetail>> GetFlights()
        {
            string flyRouteApi = _configuration.GetValue<string>("FlyApi");
            RestRequest request = new RestRequest(flyRouteApi);

            try
            {
                IList<FlightDetail> flights = 
                    await _restClient.GetAsync<IList<FlightDetail>>(request) ?? new List<FlightDetail>();

                return flights;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los vuelos de la Api {flyRouteApi}", flyRouteApi);
                return new List<FlightDetail>();
            }
        }
    }
}
