using ParkManager.Application.Features.Slots.Queries.GetSlot;

namespace ParkManager.Application.Features.Slots.Queries.GetSlots
{
    public class GetSlotsQueryResponse
    {
        public List<GetSlotQueryResponse> Results { get; set; } = new List<GetSlotQueryResponse>();
    }
}
