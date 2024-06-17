using AutoMapper;
using ParkManager.Api.Models;
using ParkManager.Application.Features.Lanes.Commands.AddLane;
using ParkManager.Application.Features.Lanes.Commands.UpdateLane;
using ParkManager.Application.Features.Lanes.Queries.GetLane;

namespace ParkManager.Api.Mappings
{
    public class LaneProfile : Profile
    {
        public LaneProfile()
        {
            CreateMap<AddLaneCommand, Lane>().ReverseMap();
            CreateMap<UpdateLaneCommand, Lane>().ReverseMap();
            CreateMap<UpdateLaneCommandResponse, Lane>().ReverseMap();
            CreateMap<AddLaneCommandResponse, Lane>().ReverseMap();
            CreateMap<Lane, GetLaneQueryResponse>().ReverseMap();

        }
    }
}
