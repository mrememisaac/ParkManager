using AutoMapper;
using ParkManager.Application.Features.Occasions.Commands.AddOccasion;
using ParkManager.Application.Features.Occasions.Commands.UpdateOccasion;
using ParkManager.Application.Features.Occasions.Queries.GetOccasion;
using ParkManager.Domain;

namespace ParkManager.Application.Mappings
{
    public class OccasionProfile : Profile
    {
        public OccasionProfile()
        {
            CreateMap<AddOccasionCommand, Occasion>();
            CreateMap<UpdateOccasionCommand, Occasion>();
            CreateMap<UpdateOccasionCommandResponse, Occasion>().ReverseMap();
            CreateMap<AddOccasionCommandResponse, Occasion>().ReverseMap();
            CreateMap<Occasion, GetOccasionQueryResponse>();
        }
    }
}
