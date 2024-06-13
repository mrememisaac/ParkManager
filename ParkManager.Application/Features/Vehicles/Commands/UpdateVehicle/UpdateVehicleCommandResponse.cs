﻿namespace ParkManager.Application.Features.Vehicles.Commands.UpdateVehicle
{
    public class UpdateVehicleCommandResponse
    {
        public Guid Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Registration { get; set; }
    }
}
