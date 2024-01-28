using AutoMapper;
using Moq;
using Nsh_Air.Domain;
using Nsh_Air.Infrastructure;
using Nsh_Air.Services;
using Nsh_Air.Utils;

namespace Nsh_AirTest
{
    public class SearchFlightTest
    {
        private readonly SearchFlight _searchFlight;

        public SearchFlightTest()
        {
            var flightServiceMock = new Mock<IFlightService>();

            flightServiceMock.Setup(x => x.GetFlights())
                .ReturnsAsync(GetFlightsToTest());

            var mapperMock = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfiles());
            });
            var mapper = mapperMock.CreateMapper();

            _searchFlight = new SearchFlight(flightServiceMock.Object, mapper);
        }

        public static IList<FlightDetail> GetFlightsToTest()
        {
            IList<FlightDetail> flightDetails = new List<FlightDetail> 
            {
                new FlightDetail
                {
                    DepartureStation = "MZL",
                    ArrivalStation = "MDE",
                    Price = 180,
                    FlightCarrier = "CO",
                    FlightNumber = "0821"
                },
                new FlightDetail
                {
                    DepartureStation = "MZL",
                    ArrivalStation = "PEI",
                    Price = 120,
                    FlightCarrier = "CO",
                    FlightNumber = "0843"
                },
                new FlightDetail
                {
                    DepartureStation = "MDE",
                    ArrivalStation = "BOG",
                    Price = 90,
                    FlightCarrier = "CO",
                    FlightNumber = "0945"
                },
                new FlightDetail
                {
                    DepartureStation = "BOG",
                    ArrivalStation = "MEX",
                    Price = 225,
                    FlightCarrier = "CO",
                    FlightNumber = "0703"
                }
            };

            return flightDetails;
        }

        [Fact]
        public async Task Search_GiveSimpleRoute_ReturnsFlightDetail()
        {
            string origin = "MZL";
            string destination = "PEI";

            IList<FlightDetail> flightDetails = await _searchFlight.Search(origin, destination);
            FlightDetail? flight = flightDetails.FirstOrDefault();
            
            Assert.NotNull(flightDetails);
            Assert.Equal(1, flightDetails.Count);
            Assert.NotNull(flight);
            Assert.Equal("PEI", flight.ArrivalStation);
        }

        [Fact]
        public async Task Search_GiveDoubleRoute_ReturnsFlightDetail()
        {
            string origin = "MZL";
            string destination = "BOG";

            IList<FlightDetail> flightDetails = await _searchFlight.Search(origin, destination);
            FlightDetail? flight = flightDetails.LastOrDefault();

            Assert.NotNull(flightDetails);
            Assert.Equal(2, flightDetails.Count);
            Assert.NotNull(flight);
            Assert.Equal("BOG", flight.ArrivalStation);
        }

        [Fact]
        public async Task Search_GiveTripleRoute_ReturnsFlightDetail()
        {
            string origin = "MZL";
            string destination = "MEX";

            IList<FlightDetail> flightDetails = await _searchFlight.Search(origin, destination);
            FlightDetail? flight = flightDetails.LastOrDefault();

            Assert.NotNull(flightDetails);
            Assert.Equal(3, flightDetails.Count);
            Assert.NotNull(flight);
            Assert.Equal("MEX", flight.ArrivalStation);
        }

        [Fact]
        public async Task GetJourney_GiveSimpleRoute_ReturnsJourney()
        {
            string origin = "MDE";
            string destination = "BOG";

            Journey journey = await _searchFlight.GetJourney(origin, destination);

            Assert.NotNull(journey);
            Assert.Equal(1, journey.Flights.Count);
            Assert.Equal(90, journey.Price);
        }
    }
}
