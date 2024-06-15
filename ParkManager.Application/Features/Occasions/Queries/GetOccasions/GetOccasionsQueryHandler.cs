using AutoMapper;
using MediatR;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Occasions.Queries.GetOccasion;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Occasions.Queries.GetOccasions
{
    public class GetOccasionsQueryHandler : IRequestHandler<GetOccasionsQuery, GetOccasionsQueryResponse>
    {
        private readonly IOccasionsRepository _vehiclesRepository;
        private readonly IMapper _mapper;

        public GetOccasionsQueryHandler(IOccasionsRepository vehicleRepository, IMapper mapper)
        {
            _vehiclesRepository = vehicleRepository;
            _mapper = mapper;
        }

        public async Task<GetOccasionsQueryResponse> Handle(GetOccasionsQuery request, CancellationToken cancellationToken)
        {
            var vehicles = await _vehiclesRepository.List(request.Count, request.Page);
            return _mapper.Map<GetOccasionsQueryResponse>(vehicles);
        }
    }
}
