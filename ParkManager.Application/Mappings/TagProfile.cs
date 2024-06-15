using AutoMapper;
using ParkManager.Application.Features.Tags.Commands.AddTag;
using ParkManager.Application.Features.Tags.Commands.UpdateTag;
using ParkManager.Application.Features.Tags.Queries.GetTag;
using ParkManager.Domain;

namespace ParkManager.Application.Mappings
{
    public class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<AddTagCommand, Tag>();
            CreateMap<UpdateTagCommand, Tag>();
            CreateMap<UpdateTagCommandResponse, Tag>().ReverseMap();
            CreateMap<AddTagCommandResponse, Tag>().ReverseMap();
            CreateMap<Tag, GetTagQueryResponse>();
        }
    }
}
