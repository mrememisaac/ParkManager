using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Drivers.Queries.GetDriver
{
    public class GetDriverQueryHandler : IRequestHandler<GetDriverQuery, GetDriverQueryResponse>
    {
        private readonly IDriversRepository _driverRepository;
        private readonly IMapper _mapper; 
        private readonly ILogger<GetDriverQueryHandler> _logger;

        public GetDriverQueryHandler(IDriversRepository driverRepository, IMapper mapper, ILogger<GetDriverQueryHandler> logger)
        {
            _driverRepository = driverRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetDriverQueryResponse> Handle(GetDriverQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Retrieving driver with id: {request.Id}");
            var response = await _driverRepository.Get(request.Id);
            return _mapper.Map<GetDriverQueryResponse>(response);
        }
    }
}
