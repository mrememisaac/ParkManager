using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Vehicles.Queries.GetVehicle;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Vehicles.Queries.GetVehicles
{
    public class GetVehiclesQueryHandler : IRequestHandler<GetVehiclesQuery, GetVehiclesQueryResponse>
    {
        private readonly IVehiclesRepository _vehiclesRepository;
        private readonly IMapper _mapper; 
        private readonly ILogger<GetVehiclesQueryHandler> _logger;

        public GetVehiclesQueryHandler(IVehiclesRepository vehicleRepository, IMapper mapper, ILogger<GetVehiclesQueryHandler> logger)
        {
            _vehiclesRepository = vehicleRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetVehiclesQueryResponse> Handle(GetVehiclesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetVehiclesQueryHandler.Handle - Retrieving vehicles - {request}");
            var vehicles = await _vehiclesRepository.List(request.Count, request.Page);
            return new GetVehiclesQueryResponse { Items = _mapper.Map<List<GetVehicleQueryResponse>>(vehicles) };
        }
    }
}
