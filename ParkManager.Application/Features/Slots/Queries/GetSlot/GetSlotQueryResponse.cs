namespace ParkManager.Application.Features.Slots.Queries.GetSlot
{
    public class GetSlotQueryResponse
    {
        public Guid Id { get; set; }
        public Guid LaneId { get; set; }

        public string Name { get; set; }
    }
}
