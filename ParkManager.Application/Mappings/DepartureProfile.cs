using AutoMapper;
using ParkManager.Application.Features.Departures.Commands.AddDeparture;
using ParkManager.Application.Features.Departures.Commands.UpdateDeparture;
using ParkManager.Domain;

namespace ParkManager.Application.Mappings
{
    public class DepartureProfile : Profile
    {
        public DepartureProfile()
        {
            CreateMap<AddDepartureCommand, Departure>();
            CreateMap<UpdateDepartureCommand, Departure>();
        }
    }
}
