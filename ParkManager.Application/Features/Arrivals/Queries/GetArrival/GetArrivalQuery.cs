using MediatR;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Arrivals.Queries.GetArrival
{
    public class GetArrivalQuery : IRequest<Arrival>
    {
        public Guid Id { get; set; }

        public GetArrivalQuery(Guid id)
        {
            Id = id;
        }
    }
}
