namespace ParkManager.Domain
{
    public abstract class ArrivalDepartureBase : Entity
    {
        public DateTime Timestamp { get; set; }
        public int ParkId { get; set; }
        public int VehicleId { get; set; }
        public int DriverId { get; set; }
        public int TagId { get; set; }
    }
}
