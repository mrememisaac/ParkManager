namespace ParkManager.Api.Models
{
    public record Slot(Guid Id, Guid LaneId, string Name)
    {
    }
}