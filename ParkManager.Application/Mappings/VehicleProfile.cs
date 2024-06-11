using AutoMapper;
using ParkManager.Application.Features.Vehicles.Commands.AddVehicle;
using ParkManager.Application.Features.Vehicles.Commands.UpdateVehicle;
using ParkManager.Domain;

namespace ParkManager.Application.Mappings
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
