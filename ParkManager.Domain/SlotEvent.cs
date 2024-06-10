namespace ParkManager.Domain
{
    public abstract class SlotEvent : Entity
    {
        public int SlotId { get; set; }
        public int VehicleId { get; set; }
        public DateTime Timestamp { get; set; }
        public virtual EventType EventType { get; set; }
    }
}
