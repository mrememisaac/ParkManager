namespace ParkManager.Domain
{
    public class SlotReleased : SlotEvent
    {
        public override EventType EventType { get; set; } = EventType.Released;
    }
}
