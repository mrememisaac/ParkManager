using AutoMapper;
using ParkManager.Api.Models;
using ParkManager.Application.Features.Parks.Commands.AddPark;
using ParkManager.Application.Features.Parks.Commands.UpdatePark;
using ParkManager.Application.Features.Parks.Queries.GetPark;

namespace ParkManager.Api.Mappings
{
    public class ParkProfile : Profile
    {
        public ParkProfile()
        {
            CreateMap<AddParkCommand, Park>().ReverseMap();
            CreateMap<UpdateParkCommand, Park>().ReverseMap();
            CreateMap<UpdateParkCommandResponse, Park>().ReverseMap();
            CreateMap<AddParkCommandResponse, Park>().ReverseMap();
            CreateMap<Park, GetParkQueryResponse>().ReverseMap();
        }
    }
}
