using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Arrivals.Queries.GetArrival;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Arrivals.Queries.GetArrivals
{
    public class GetArrivalsQueryHandler : IRequestHandler<GetArrivalsQuery, GetArrivalsQueryResponse>
    {
        private readonly IArrivalsRepository _arrivalsRepository;
        private readonly IMapper _mapper; 
        private readonly ILogger<GetArrivalsQueryHandler> _logger;

        public GetArrivalsQueryHandler(IArrivalsRepository arrivalRepository, IMapper mapper, ILogger<GetArrivalsQueryHandler> logger)
        {
            _arrivalsRepository = arrivalRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetArrivalsQueryResponse> Handle(GetArrivalsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetArrivalsQueryHandler - Request: {request}");
            var arrivals = await _arrivalsRepository.List(request.Count, request.Page);
            return new GetArrivalsQueryResponse { Items = _mapper.Map<List<GetArrivalQueryResponse>>(arrivals) };
        }
    }
}
