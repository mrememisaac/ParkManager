﻿using AutoMapper;
using ParkManager.Application.Features.Slots.Commands.AddSlot;
using ParkManager.Application.Features.Slots.Commands.UpdateSlot;
using ParkManager.Application.Features.Slots.Queries.GetSlot;
using ParkManager.Domain;

namespace ParkManager.Application.Mappings
{
    public class SlotProfile : Profile
    {
        public SlotProfile()
        {
            CreateMap<AddSlotCommand, Slot>();
            CreateMap<UpdateSlotCommand, Slot>();
            CreateMap<UpdateSlotCommandResponse, Slot>().ReverseMap();
            CreateMap<AddSlotCommandResponse, Slot>().ReverseMap();
            CreateMap<Slot, GetSlotQueryResponse>();
        }
    }
}
