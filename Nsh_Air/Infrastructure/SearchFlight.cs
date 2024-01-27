using AutoMapper;
using Nsh_Air.Domain;
using Nsh_Air.Services;

namespace Nsh_Air.Infrastructure
{
    public class SearchFlight : ISearchFlight
    {
        private readonly IFlightService _flightService;
        private readonly IMapper _mapper;

        public SearchFlight(
            IFlightService flightService, 
            IMapper mapper)
        {
            _flightService = flightService;
            _mapper = mapper;
        }

        public async Task<IList<FlightDetail>> Search(string origin, string destination)
        {
            IList<FlightDetail> flightDetails = await _flightService.GetFlights();

            IList<FlightDetail> flightSimple = 
                GetDirectFlight(flightDetails, origin, destination);

            if(flightSimple.Count == 0)
            {
                IList<FlightDetail> flightStops = 
                    GetStopsFlights(flightDetails, origin, destination);

                return flightStops;
            }

            return flightSimple;
        }

        public IList<FlightDetail> GetDirectFlight(IList<FlightDetail> flightDetails, 
            string origin, string destination)
        {
            var flights = from flightDetail in flightDetails
                             where flightDetail.DepartureStation == origin 
                                    && flightDetail.ArrivalStation == destination
                             select flightDetail;

            return flights.ToList();
        }

        public IList<FlightDetail> GetStopsFlights(IList<FlightDetail> flightDetails,
            string origin, string destination)
        {
            IList<FlightDetail> stopFlights = new List<FlightDetail>();

            var flightsOrigin =  from f in flightDetails
                                where f.DepartureStation == origin
                                select f;

            var arrival = from f in flightDetails
                          where f.ArrivalStation == destination
                          select f;

            foreach (var a in arrival)
            {
                if(stopFlights.Count == 0)
                {
                    FlightDetail? stop =
                    flightsOrigin.Where(x => x.ArrivalStation == a.DepartureStation).FirstOrDefault();

                    if (stop is null)
                    {
                        foreach (var f in flightsOrigin)
                        {
                            IList<FlightDetail> thirdFlight = GetDirectFlight(flightDetails, f.ArrivalStation, a.DepartureStation);

                            if (thirdFlight.Count > 0)
                            {
                                stopFlights.Add(f);
                                stopFlights = stopFlights.Concat(thirdFlight).ToList();
                                stopFlights.Add(a);

                                break;
                            }
                        }
                    }
                    else
                    {
                        stopFlights.Add(stop);
                        stopFlights.Add(a);

                        break;
                    }
                }
            }

            return stopFlights;
        }

        public async Task<Journey> GetJourney(string origin, string destination)
        {
            IList<FlightDetail> flightDetails = await Search(origin, destination);
            IList<Flight> flights = _mapper.Map<IList<Flight>>(flightDetails);

            Journey journey = new Journey
            {
                Origin = origin,
                Destination = destination,
                Flights = flights
            };
            journey.CalculatePrice();

            return journey;
        }
    }
}
