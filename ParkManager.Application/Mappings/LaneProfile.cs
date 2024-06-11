using AutoMapper;
using ParkManager.Application.Features.Lanes.Commands.AddLane;
using ParkManager.Application.Features.Lanes.Commands.UpdateLane;
using ParkManager.Domain;

namespace ParkManager.Application.Mappings
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
