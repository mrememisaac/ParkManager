using AutoMapper;
using ParkManager.Api.Models;
using ParkManager.Application.Features.Departures.Commands.AddDeparture;
using ParkManager.Application.Features.Departures.Commands.UpdateDeparture;
using ParkManager.Application.Features.Departures.Queries.GetDeparture;

namespace ParkManager.Api.Mappings
{
    public class DepartureProfile : Profile
    {
        public DepartureProfile()
        {
            CreateMap<AddDepartureCommand, Departure>().ReverseMap();
            CreateMap<UpdateDepartureCommand, Departure>().ReverseMap();
            CreateMap<UpdateDepartureCommandResponse, Departure>().ReverseMap();
            CreateMap<AddDepartureCommandResponse, Departure>().ReverseMap();
            CreateMap<Departure, GetDepartureQueryResponse>().ReverseMap();

        }
    }
}
