using AutoMapper;
using ParkManager.Application.Features.Slots.Commands.AddSlot;
using ParkManager.Application.Features.Slots.Commands.UpdateSlot;
using ParkManager.Domain;

namespace ParkManager.Application.Mappings
{
    public class SlotProfile : Profile
    {
        public SlotProfile()
        {
            CreateMap<AddSlotCommand, Slot>();
            CreateMap<UpdateSlotCommand, Slot>();
        }
    }
}
