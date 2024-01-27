using Nsh_Air.Domain;

namespace Nsh_Air.Services
{
    public interface IFlightService
    {
        Task<IList<FlightDetail>> GetFlights();
    }
}
