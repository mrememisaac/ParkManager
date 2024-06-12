using AutoMapper;
using ParkManager.Api.Models;
using ParkManager.Application.Features.Vehicles.Commands.AddVehicle;
using ParkManager.Application.Features.Vehicles.Commands.UpdateVehicle;

namespace ParkManager.Api.Mappings
{
    public class VehicleProfile : Profile
    {
        public VehicleProfile()
        {
            CreateMap<AddVehicleCommand, Vehicle>();
            CreateMap<UpdateVehicleCommand, Vehicle>();
            CreateMap<UpdateVehicleCommandResponse, Vehicle>().ReverseMap();
            CreateMap<AddVehicleCommandResponse, Vehicle>().ReverseMap();
        }
    }
}
