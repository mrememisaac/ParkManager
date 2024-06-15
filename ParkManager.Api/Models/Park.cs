namespace ParkManager.Api.Models
{
    public record Park(Guid Id, string Name, string Street, string City, string State, string Country, long Latitude, long Longitude) { }
}
