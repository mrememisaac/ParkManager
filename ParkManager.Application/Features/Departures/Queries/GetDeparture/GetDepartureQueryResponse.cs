namespace ParkManager.Application.Features.Departures.Queries.GetDeparture
{
    public class GetDepartureQueryResponse
    {
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public Guid ParkId { get; set; }
        public Guid VehicleId { get; set; }
        public Guid DriverId { get; set; }
        public Guid TagId { get; set; }
    }
}
