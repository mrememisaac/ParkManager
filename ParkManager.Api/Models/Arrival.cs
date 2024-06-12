namespace ParkManager.Api.Models
{
    public record Arrival(DateTime Timestamp, Guid ParkId, Guid VehicleId, Guid DriverId, Guid TagId)
    { }
}
