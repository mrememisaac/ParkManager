using AutoMapper;
using ParkManager.Api.Models;
using ParkManager.Application.Features.Slots.Commands.AddSlot;
using ParkManager.Application.Features.Slots.Commands.UpdateSlot;

namespace ParkManager.Api.Mappings
{
    public class SlotProfile : Profile
    {
        public SlotProfile()
        {
            CreateMap<AddSlotCommand, Slot>();
            CreateMap<UpdateSlotCommand, Slot>();
            CreateMap<UpdateSlotCommandResponse, Slot>().ReverseMap();
            CreateMap<AddSlotCommandResponse, Slot>().ReverseMap();
        }
    }
}
