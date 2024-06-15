using AutoMapper;
using MediatR;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Lanes.Queries.GetLane;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Lanes.Queries.GetLanes
{
    public class GetLanesQueryHandler : IRequestHandler<GetLanesQuery, GetLanesQueryResponse>
    {
        private readonly ILanesRepository _vehiclesRepository;
        private readonly IMapper _mapper;

        public GetLanesQueryHandler(ILanesRepository vehicleRepository, IMapper mapper)
        {
            _vehiclesRepository = vehicleRepository;
            _mapper = mapper;
        }

        public async Task<GetLanesQueryResponse> Handle(GetLanesQuery request, CancellationToken cancellationToken)
        {
            var vehicles = await _vehiclesRepository.List(request.Count, request.Page);
            return _mapper.Map<GetLanesQueryResponse>(vehicles);
        }
    }
}
