using AutoMapper;
using MediatR;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Arrivals.Queries.GetArrival;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Arrivals.Queries.GetArrivals
{
    public class GetArrivalsQueryHandler : IRequestHandler<GetArrivalsQuery, GetArrivalsQueryResponse>
    {
        private readonly IArrivalsRepository _arrivalsRepository;
        private readonly IMapper _mapper;

        public GetArrivalsQueryHandler(IArrivalsRepository arrivalRepository, IMapper mapper)
        {
            _arrivalsRepository = arrivalRepository;
            _mapper = mapper;
        }

        public async Task<GetArrivalsQueryResponse> Handle(GetArrivalsQuery request, CancellationToken cancellationToken)
        {
            var arrivals = await _arrivalsRepository.List(request.Count, request.Page);
            return new GetArrivalsQueryResponse { Items = _mapper.Map<List<GetArrivalQueryResponse>>(arrivals) };
        }
    }
}
