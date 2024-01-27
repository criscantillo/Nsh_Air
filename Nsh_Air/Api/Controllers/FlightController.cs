using Microsoft.AspNetCore.Mvc;
using Nsh_Air.Api.Models;
using Nsh_Air.Domain;

namespace Nsh_Air.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlightController : ControllerBase
    {
        private readonly ISearchFlight _searchFlight;

        public FlightController(ISearchFlight searchFlight)
        {
            _searchFlight = searchFlight;
        }

        [HttpGet]
        [ActionName(nameof(SearchFlights))]
        public async Task<ActionResult<IList<Flight>>> SearchFlights(
            string origin, string destination)
        {
            Journey journey = await _searchFlight.GetJourney(origin, destination);
            JourneyResponse journeyResponse = new JourneyResponse 
            {
                Journey = journey
            };

            return Ok(journeyResponse);
        }
    }
}
