using AutoMapper;
using ParkManager.Api.Models;
using ParkManager.Application.Features.Parks.Commands.AddPark;
using ParkManager.Application.Features.Parks.Commands.UpdatePark;

namespace ParkManager.Api.Mappings
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
