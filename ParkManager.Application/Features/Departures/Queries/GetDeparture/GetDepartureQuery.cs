﻿using MediatR;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Departures.Queries.GetDeparture
{
    public class GetDepartureQuery : IRequest<Departure>
    {
        public Guid Id { get; set; }

        public GetDepartureQuery(Guid id)
        {
            Id = id;
        }
    }
}
