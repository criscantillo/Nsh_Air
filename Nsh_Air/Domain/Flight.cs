namespace Nsh_Air.Domain
{
    public class Flight
    {
        public Transport Transport { get; set; } = new Transport();
        public string Origin { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public double Price { get; set; }
    }
}
