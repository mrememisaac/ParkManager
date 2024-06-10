namespace ParkManager.Domain
{
    public class DriverMetadata : Entity
    {
        public int DriverId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
