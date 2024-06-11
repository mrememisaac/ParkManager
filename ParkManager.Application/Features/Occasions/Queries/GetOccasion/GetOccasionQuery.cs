using MediatR;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Occasions.Queries.GetOccasion
{
    public class GetOccasionQuery : IRequest<Occasion>
    {
        public Guid Id { get; set; }

        public GetOccasionQuery(Guid id)
        {
            Id = id;
        }
    }
}
