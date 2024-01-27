using Nsh_Air.Domain;

namespace Nsh_AirTest
{
    public class JourneyTest
    {
        [Fact]
        public void CalculatePrice_ReturnsPrice()
        {
            IList<Flight> flights = new List<Flight> 
            {
                new Flight
                {
                    Price = 250
                },
                new Flight
                {
                    Price = 120
                }
            };

            Journey journey = new Journey
            { 
                Flights = flights
            };

            journey.CalculatePrice();
            double totalPrice = 370;

            Assert.Equal(totalPrice, journey.Price);
        }
    }
}