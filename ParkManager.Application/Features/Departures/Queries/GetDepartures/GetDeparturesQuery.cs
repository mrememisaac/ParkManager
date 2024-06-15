using MediatR;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Departures.Queries.GetDepartures
{
    public record GetDeparturesQuery(int Page, int Count) : IRequest<GetDeparturesQueryResponse>
    { 
    }
}
