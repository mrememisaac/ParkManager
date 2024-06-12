namespace ParkManager.Api.Models
{
    public record Departure(DateTime Timestamp, Guid ParkId, Guid VehicleId, Guid DriverId, Guid TagId) { }
}
