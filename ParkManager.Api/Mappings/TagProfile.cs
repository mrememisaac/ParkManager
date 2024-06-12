using AutoMapper;
using ParkManager.Api.Models;
using ParkManager.Application.Features.Tags.Commands.AddTag;
using ParkManager.Application.Features.Tags.Commands.UpdateTag;

namespace ParkManager.Api.Mappings
{
    public class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<AddTagCommand, Tag>();
            CreateMap<UpdateTagCommand, Tag>();
            CreateMap<UpdateTagCommandResponse, Tag>().ReverseMap();
            CreateMap<AddTagCommandResponse, Tag>().ReverseMap();
        }
    }
}
