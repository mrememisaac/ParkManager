using AutoMapper;
using ParkManager.Api.Models;
using ParkManager.Application.Features.Departures.Commands.AddDeparture;
using ParkManager.Application.Features.Departures.Commands.UpdateDeparture;

namespace ParkManager.Api.Mappings
{
    public class DepartureProfile : Profile
    {
        public DepartureProfile()
        {
            CreateMap<AddDepartureCommand, Departure>();
            CreateMap<UpdateDepartureCommand, Departure>();
            CreateMap<UpdateDepartureCommandResponse, Departure>().ReverseMap();
            CreateMap<AddDepartureCommandResponse, Departure>().ReverseMap();
        }
    }
}
