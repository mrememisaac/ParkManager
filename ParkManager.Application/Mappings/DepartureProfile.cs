﻿using AutoMapper;
using ParkManager.Application.Features.Arrivals.Queries.GetArrival;
using ParkManager.Application.Features.Departures.Commands.AddDeparture;
using ParkManager.Application.Features.Departures.Commands.UpdateDeparture;
using ParkManager.Application.Features.Departures.Queries.GetDeparture;
using ParkManager.Domain;

namespace ParkManager.Application.Mappings
{
    public class DepartureProfile : Profile
    {
        public DepartureProfile()
        {
            CreateMap<AddDepartureCommand, Departure>();
            CreateMap<UpdateDepartureCommand, Departure>();
            CreateMap<UpdateDepartureCommandResponse, Departure>().ReverseMap();
            CreateMap<AddDepartureCommandResponse, Departure>().ReverseMap();
            CreateMap<Departure, GetDepartureQueryResponse>();
        }
    }
}
