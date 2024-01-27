using AutoMapper;
using Nsh_Air.Domain;

namespace Nsh_Air.Utils
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<FlightDetail, Flight>()
                .ForMember(
                    dest => dest.Origin,
                    opt => opt.MapFrom(src => src.DepartureStation)
                )
                .ForMember(
                    dest => dest.Destination,
                    opt => opt.MapFrom(src => src.ArrivalStation)
                )
                .ForMember(
                    dest => dest.Transport,
                    opt => opt.MapFrom(src => new Transport {
                        FlightCarrier = src.FlightCarrier,
                        FlightNumber = src.FlightNumber
                    })
                );
        }
    }
}
