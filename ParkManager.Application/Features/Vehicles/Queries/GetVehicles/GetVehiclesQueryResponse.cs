using ParkManager.Application.Features.Vehicles.Queries.GetVehicle;

namespace ParkManager.Application.Features.Vehicles.Queries.GetVehicles
{
    public class GetVehiclesQueryResponse
    {
        public List<GetVehicleQueryResponse> Items { get; set; } = new List<GetVehicleQueryResponse>();
    }
}
