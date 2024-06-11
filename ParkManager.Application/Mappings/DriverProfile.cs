using AutoMapper;
using ParkManager.Application.Features.Drivers.Commands.AddDriver;
using ParkManager.Application.Features.Drivers.Commands.UpdateDriver;
using ParkManager.Domain;

namespace ParkManager.Application.Mappings
{
    public class DriverProfile : Profile
    {
        public DriverProfile()
        {
            CreateMap<AddDriverCommand, Driver>();
            CreateMap<UpdateDriverCommand, Driver>();
        }
    }
}
