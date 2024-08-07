﻿using AutoMapper;
using ParkManager.Application.Features.Arrivals.Commands.AddArrival;
using ParkManager.Application.Features.Arrivals.Commands.UpdateArrival;
using ParkManager.Application.Features.Arrivals.Queries.GetArrival;
using ParkManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkManager.Application.Mappings
{
    public class ArrivalProfile : Profile
    {
        public ArrivalProfile()
        {
            CreateMap<AddArrivalCommand, Arrival>();
            CreateMap<UpdateArrivalCommand, Arrival>();
            CreateMap<UpdateArrivalCommandResponse, Arrival>().ReverseMap();
            CreateMap<AddArrivalCommandResponse, Arrival>().ReverseMap();
            CreateMap<Arrival, GetArrivalQueryResponse>();
        }
    }
}
