﻿namespace ParkManager.Application.Features.Vehicles.Queries.GetVehicle
{
    public class GetVehicleQueryResponse
    {
        public Guid Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Registration { get; set; }
    }
}
