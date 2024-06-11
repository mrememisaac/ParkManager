using AutoMapper;
using ParkManager.Application.Features.Occasions.Commands.AddOccasion;
using ParkManager.Application.Features.Occasions.Commands.UpdateOccasion;
using ParkManager.Domain;

namespace ParkManager.Application.Mappings
{
    public class OccassionProfile : Profile
    {
        public OccassionProfile()
        {
            CreateMap<AddOccasionCommand, Occasion>();
            CreateMap<UpdateOccasionCommand, Occasion>();
        }
    }
}
