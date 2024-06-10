namespace ParkManager.Application.Features.Arrivals.Commands.AddArrival
{
    public class AddArrivalCommandResponse 
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public int ParkId { get; set; }
        public int VehicleId { get; set; }
        public int DriverId { get; set; }
        public int TagId { get; set; }
    }
}
