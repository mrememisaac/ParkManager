using MediatR;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Occasions.Queries.GetOccasion
{
    public class GetOccasionQueryHandler : IRequestHandler<GetOccasionQuery, Occasion>
    {
        private readonly IOccasionsRepository _occasionsRepository;
        
        public GetOccasionQueryHandler(IOccasionsRepository occasionRepository)
        {
            _occasionsRepository = occasionRepository;
        }

        public async Task<Occasion> Handle(GetOccasionQuery request, CancellationToken cancellationToken)
        {
            return await _occasionsRepository.Get(request.Id);
        }
    }
}