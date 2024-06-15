using MediatR;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Vehicles.Queries.GetVehicles
{
    public record GetVehiclesQuery(int Page, int Count) : IRequest<GetVehiclesQueryResponse>
    { 
    }
}
