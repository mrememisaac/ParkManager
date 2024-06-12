using AutoMapper;
using ParkManager.Api.Models;
using ParkManager.Application.Features.Occasions.Commands.AddOccasion;
using ParkManager.Application.Features.Occasions.Commands.UpdateOccasion;

namespace ParkManager.Api.Mappings
{
    public class OccasionProfile : Profile
    {
        public OccasionProfile()
        {
            CreateMap<AddOccasionCommand, Occasion>();
            CreateMap<UpdateOccasionCommand, Occasion>();
            CreateMap<UpdateOccasionCommandResponse, Occasion>().ReverseMap();
            CreateMap<AddOccasionCommandResponse, Occasion>().ReverseMap();
        }
    }
}
