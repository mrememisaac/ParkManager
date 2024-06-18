using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Drivers.Queries.GetDriver;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Drivers.Queries.GetDrivers
{
    public class GetDriversQueryHandler : IRequestHandler<GetDriversQuery, GetDriversQueryResponse>
    {
        private readonly IDriversRepository _driversRepository;
        private readonly IMapper _mapper; 
        private readonly ILogger<GetDriversQueryHandler> _logger;

        public GetDriversQueryHandler(IDriversRepository driverRepository, IMapper mapper, ILogger<GetDriversQueryHandler> logger)
        {
            _driversRepository = driverRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetDriversQueryResponse> Handle(GetDriversQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetDriversQueryHandler.Handle - Retrieving drivers. - {request}");
            var drivers = await _driversRepository.List(request.Count, request.Page);
            return new GetDriversQueryResponse{ Items = _mapper.Map<List<GetDriverQueryResponse>>(drivers)};
        }
    }
}
