namespace ParkManager.Api.Models
{
    public record Departure(Guid Id, DateTime Timestamp, Guid ParkId, Guid VehicleId, Guid DriverId, Guid TagId) { }
}
