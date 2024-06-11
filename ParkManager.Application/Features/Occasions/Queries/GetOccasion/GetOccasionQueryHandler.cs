using MediatR;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Occasions.Queries.GetOccasion
{
    public class GetOccasionQueryHandler : IRequestHandler<GetOccasionQuery, Occasion>
    {
        private readonly IOccasionsRepository _OccasionsRepository;
        
        public GetOccasionQueryHandler(IOccasionsRepository OccasionRepository)
        {
            _OccasionsRepository = OccasionRepository;
        }

        public async Task<Occasion> Handle(GetOccasionQuery request, CancellationToken cancellationToken)
        {
            return await _OccasionsRepository.Get(request.Id);
        }
    }
}