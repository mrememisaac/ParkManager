using MediatR;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Vehicles.Queries.GetVehicle
{
    public class GetVehicleQuery : IRequest<GetVehicleQueryResponse>
    {
        public Guid Id { get; set; }

        public GetVehicleQuery(Guid id)
        {
            Id = id;
        }
    }
}
