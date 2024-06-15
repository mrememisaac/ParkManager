using MediatR;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Drivers.Queries.GetDrivers
{
    public record GetDriversQuery(int Page, int Count) : IRequest<GetDriversQueryResponse>
    { 
    }
}
