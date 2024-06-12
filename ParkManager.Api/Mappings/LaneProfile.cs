using AutoMapper;
using ParkManager.Api.Models;
using ParkManager.Application.Features.Lanes.Commands.AddLane;
using ParkManager.Application.Features.Lanes.Commands.UpdateLane;

namespace ParkManager.Api.Mappings
{
    public class LaneProfile : Profile
    {
        public LaneProfile()
        {
            CreateMap<AddLaneCommand, Lane>();
            CreateMap<UpdateLaneCommand, Lane>();
            CreateMap<UpdateLaneCommandResponse, Lane>().ReverseMap();
            CreateMap<AddLaneCommandResponse, Lane>().ReverseMap();
        }
    }
}
