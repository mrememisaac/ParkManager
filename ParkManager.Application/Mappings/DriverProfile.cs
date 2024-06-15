using AutoMapper;
using ParkManager.Application.Features.Drivers.Commands.AddDriver;
using ParkManager.Application.Features.Drivers.Commands.UpdateDriver;
using ParkManager.Application.Features.Drivers.Queries.GetDriver;
using ParkManager.Domain;

namespace ParkManager.Application.Mappings
{
    public class DriverProfile : Profile
    {
        public DriverProfile()
        {
            CreateMap<AddDriverCommand, Driver>();
            CreateMap<UpdateDriverCommand, Driver>();
            CreateMap<UpdateDriverCommandResponse, Driver>().ReverseMap();
            CreateMap<AddDriverCommandResponse, Driver>().ReverseMap();
            CreateMap<Driver, GetDriverQueryResponse>();
        }
    }
}
