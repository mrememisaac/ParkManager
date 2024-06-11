using AutoMapper;
using ParkManager.Application.Features.Parks.Commands.AddPark;
using ParkManager.Application.Features.Parks.Commands.UpdatePark;
using ParkManager.Domain;

namespace ParkManager.Application.Mappings
{
    public class ParkProfile : Profile
    {
        public ParkProfile()
        {
            CreateMap<AddParkCommand, Park>();
            CreateMap<UpdateParkCommand, Park>();
            CreateMap<UpdateParkCommandResponse, Park>().ReverseMap();
            CreateMap<AddParkCommandResponse, Park>().ReverseMap();
        }
    }
}
