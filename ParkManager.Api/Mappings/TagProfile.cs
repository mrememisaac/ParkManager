using AutoMapper;
using ParkManager.Api.Models;
using ParkManager.Application.Features.Tags.Commands.AddTag;
using ParkManager.Application.Features.Tags.Commands.UpdateTag;
using ParkManager.Application.Features.Tags.Queries.GetTag;

namespace ParkManager.Api.Mappings
{
    public class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<AddTagCommand, Tag>().ReverseMap();
            CreateMap<UpdateTagCommand, Tag>().ReverseMap();
            CreateMap<UpdateTagCommandResponse, Tag>().ReverseMap();
            CreateMap<AddTagCommandResponse, Tag>().ReverseMap();
            CreateMap<Tag, GetTagQueryResponse>().ReverseMap();
        }
    }
}
