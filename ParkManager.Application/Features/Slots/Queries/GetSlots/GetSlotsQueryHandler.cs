using AutoMapper;
using MediatR;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Slots.Queries.GetSlot;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Slots.Queries.GetSlots
{
    public class GetSlotsQueryHandler : IRequestHandler<GetSlotsQuery, GetSlotsQueryResponse>
    {
        private readonly ISlotsRepository _vehiclesRepository;
        private readonly IMapper _mapper;

        public GetSlotsQueryHandler(ISlotsRepository vehicleRepository, IMapper mapper)
        {
            _vehiclesRepository = vehicleRepository;
            _mapper = mapper;
        }

        public async Task<GetSlotsQueryResponse> Handle(GetSlotsQuery request, CancellationToken cancellationToken)
        {
            var vehicles = await _vehiclesRepository.List(request.Count, request.Page);
            return _mapper.Map<GetSlotsQueryResponse>(vehicles);
        }
    }
}
