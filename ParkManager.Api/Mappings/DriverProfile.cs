using AutoMapper;
using ParkManager.Api.Models;
using ParkManager.Application.Features.Drivers.Commands.AddDriver;
using ParkManager.Application.Features.Drivers.Commands.UpdateDriver;

namespace ParkManager.Api.Mappings
{
    public class DriverProfile : Profile
    {
        public DriverProfile()
        {
            CreateMap<AddDriverCommand, Driver>();
            CreateMap<UpdateDriverCommand, Driver>();
            CreateMap<UpdateDriverCommandResponse, Driver>().ReverseMap();
            CreateMap<AddDriverCommandResponse, Driver>().ReverseMap();
        }
    }
}
