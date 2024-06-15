namespace ParkManager.Api.Models
{
    public record Arrival(Guid Id, DateTime Timestamp, Guid ParkId, Guid VehicleId, Guid DriverId, Guid TagId)
    { }
}
