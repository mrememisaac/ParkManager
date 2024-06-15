using ParkManager.Application.Features.Drivers.Queries.GetDriver;

namespace ParkManager.Application.Features.Drivers.Queries.GetDrivers
{
    public class GetDriversQueryResponse
    {
        public List<GetDriverQueryResponse> Results { get; set; } = new List<GetDriverQueryResponse>();
    }
}
