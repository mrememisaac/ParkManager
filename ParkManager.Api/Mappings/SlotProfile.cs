using AutoMapper;
using ParkManager.Api.Models;
using ParkManager.Application.Features.Slots.Commands.AddSlot;
using ParkManager.Application.Features.Slots.Commands.UpdateSlot;
using ParkManager.Application.Features.Slots.Queries.GetSlot;

namespace ParkManager.Api.Mappings
{
    public class SlotProfile : Profile
    {
        public SlotProfile()
        {
            CreateMap<AddSlotCommand, Slot>().ReverseMap();
            CreateMap<UpdateSlotCommand, Slot>().ReverseMap();
            CreateMap<UpdateSlotCommandResponse, Slot>().ReverseMap();
            CreateMap<AddSlotCommandResponse, Slot>().ReverseMap();
            CreateMap<Slot, GetSlotQueryResponse>().ReverseMap();
        }
    }
}
