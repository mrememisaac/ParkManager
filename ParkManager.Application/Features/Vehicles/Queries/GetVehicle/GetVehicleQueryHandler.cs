using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Vehicles.Queries.GetVehicle
{
    public class GetVehicleQueryHandler : IRequestHandler<GetVehicleQuery, GetVehicleQueryResponse>
    {
        private readonly IVehiclesRepository _vehiclesRepository;
        private readonly IMapper _mapper; 
        private readonly ILogger<GetVehicleQueryHandler> _logger;

        public GetVehicleQueryHandler(IVehiclesRepository vehicleRepository, IMapper mapper, ILogger<GetVehicleQueryHandler> logger)
        {
            _vehiclesRepository = vehicleRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetVehicleQueryResponse> Handle(GetVehicleQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetVehiclesueryHandler.Handle - Retrieving vehicle.");
            var vehicle = await _vehiclesRepository.Get(request.Id);
            return _mapper.Map<GetVehicleQueryResponse>(vehicle);
        }
    }
}
