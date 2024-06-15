using MediatR;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Parks.Queries.GetPark
{
    public class GetParkQuery : IRequest<GetParkQueryResponse>
    {
        public Guid Id { get; set; }

        public GetParkQuery(Guid id)
        {
            Id = id;
        }
    }
}
