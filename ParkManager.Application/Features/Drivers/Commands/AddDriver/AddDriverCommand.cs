﻿using MediatR;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Drivers.Commands.AddDriver
{
    public class AddDriverCommand : IRequest<AddDriverCommandResponse>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }
}
