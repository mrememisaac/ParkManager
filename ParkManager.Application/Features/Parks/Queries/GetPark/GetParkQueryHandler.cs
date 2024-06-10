using MediatR;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Parks.Queries.GetPark
{
    public class GetParkQueryHandler : IRequestHandler<GetParkQuery, Park>
    {
        private readonly IParksRepository _parksRepository;
        
        public GetParkQueryHandler(IParksRepository parkRepository)
        {
            _parksRepository = parkRepository;
        }

        public async Task<Park> Handle(GetParkQuery request, CancellationToken cancellationToken)
        {
            return await _parksRepository.Get(request.Id);
        }
    }
}