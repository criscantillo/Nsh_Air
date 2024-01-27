namespace Nsh_Air.Domain
{
    public interface ISearchFlight
    {
        Task<IList<FlightDetail>> Search(string origin, string destination);
        Task<Journey> GetJourney(string origin, string destination);
    }
}
