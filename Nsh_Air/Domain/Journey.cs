namespace Nsh_Air.Domain
{
    public class Journey
    {
        public IList<Flight> Flights { get; set; } = new List<Flight>();
        public string Origin { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public double Price { get; set; }

        public void CalculatePrice()
        {
            Price = Flights.Sum(f => f.Price);
        }
    }
}
