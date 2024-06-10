using MediatR;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Arrivals.Queries.GetArrival
{
    public class GetArrivalQueryHandler : IRequestHandler<GetArrivalQuery, Arrival>
    {
        private readonly IArrivalsRepository _arrivalsRepository;
        
        public GetArrivalQueryHandler(IArrivalsRepository repository)
        {
            _arrivalsRepository = repository;
        }

        public async Task<Arrival> Handle(GetArrivalQuery request, CancellationToken cancellationToken)
        {
            return await _arrivalsRepository.Get(request.Id);
        }
    }
}
