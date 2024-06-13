namespace ParkManager.Api.Models
{
    public record Occasion(Guid Id, string Name, DateTime StartDate, DateTime EndDate)
    {
    }
}