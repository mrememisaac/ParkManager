using AutoMapper;
using ParkManager.Api.Models;
using ParkManager.Application.Features.Drivers.Commands.AddDriver;
using ParkManager.Application.Features.Drivers.Commands.UpdateDriver;
using ParkManager.Application.Features.Drivers.Queries.GetDriver;

namespace ParkManager.Api.Mappings
{
    public class DriverProfile : Profile
    {
        public DriverProfile()
        {
            CreateMap<AddDriverCommand, Driver>().ReverseMap();
            CreateMap<UpdateDriverCommand, Driver>().ReverseMap();
            CreateMap<UpdateDriverCommandResponse, Driver>().ReverseMap();
            CreateMap<AddDriverCommandResponse, Driver>().ReverseMap();
            CreateMap<Driver, GetDriverQueryResponse>().ReverseMap();

        }
    }
}
