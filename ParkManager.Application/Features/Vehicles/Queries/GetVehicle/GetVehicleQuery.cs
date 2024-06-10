using MediatR;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Vehicles.Queries.GetVehicle
{
    public class GetVehicleQuery : IRequest<GetVehicleQueryResponse>
    {
        public int Id { get; set; }

        public GetVehicleQuery(int id)
        {
            Id = id;
        }
    }
}
