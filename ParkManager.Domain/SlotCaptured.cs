namespace ParkManager.Domain
{
    public class SlotCaptured : SlotEvent
    {
        public override EventType EventType { get; set; } = EventType.Captured;

    }
}
