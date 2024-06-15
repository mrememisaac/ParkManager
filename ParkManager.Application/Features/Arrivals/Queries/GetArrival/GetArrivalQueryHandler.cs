using AutoMapper;
using MediatR;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Arrivals.Queries.GetArrival
{
    public class GetArrivalQueryHandler : IRequestHandler<GetArrivalQuery, GetArrivalQueryResponse>
    {
        private readonly IArrivalsRepository _arrivalsRepository;
        private readonly IMapper _mapper;

        public GetArrivalQueryHandler(IArrivalsRepository repository, IMapper mapper)
        {
            _arrivalsRepository = repository;
            _mapper = mapper;
        }

        public async Task<GetArrivalQueryResponse> Handle(GetArrivalQuery request, CancellationToken cancellationToken)
        {
            var response = await _arrivalsRepository.Get(request.Id);
            return _mapper.Map<GetArrivalQueryResponse>(response);
        }
    }
}
