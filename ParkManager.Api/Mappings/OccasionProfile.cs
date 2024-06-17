using AutoMapper;
using ParkManager.Api.Models;
using ParkManager.Application.Features.Occasions.Commands.AddOccasion;
using ParkManager.Application.Features.Occasions.Commands.UpdateOccasion;
using ParkManager.Application.Features.Occasions.Queries.GetOccasion;

namespace ParkManager.Api.Mappings
{
    public class OccasionProfile : Profile
    {
        public OccasionProfile()
        {
            CreateMap<AddOccasionCommand, Occasion>().ReverseMap();
            CreateMap<UpdateOccasionCommand, Occasion>().ReverseMap();
            CreateMap<UpdateOccasionCommandResponse, Occasion>().ReverseMap();
            CreateMap<AddOccasionCommandResponse, Occasion>().ReverseMap();
            CreateMap<Occasion, GetOccasionQueryResponse>().ReverseMap();

        }
    }
}
