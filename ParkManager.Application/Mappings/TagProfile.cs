using AutoMapper;
using ParkManager.Application.Features.Tags.Commands.AddTag;
using ParkManager.Application.Features.Tags.Commands.UpdateTag;
using ParkManager.Domain;

namespace ParkManager.Application.Mappings
{
    public class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<AddTagCommand, Tag>();
            CreateMap<UpdateTagCommand, Tag>();
        }
    }
}
