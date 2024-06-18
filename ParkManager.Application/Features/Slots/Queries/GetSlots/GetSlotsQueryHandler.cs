using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Slots.Queries.GetSlot;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Slots.Queries.GetSlots
{
    public class GetSlotsQueryHandler : IRequestHandler<GetSlotsQuery, GetSlotsQueryResponse>
    {
        private readonly ISlotsRepository _slotsRepository;
        private readonly IMapper _mapper; 
        private readonly ILogger<GetSlotsQueryHandler> _logger;

        public GetSlotsQueryHandler(ISlotsRepository slotRepository, IMapper mapper, ILogger<GetSlotsQueryHandler> logger)
        {
            _slotsRepository = slotRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetSlotsQueryResponse> Handle(GetSlotsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetSlotsQueryHandler - Retrieving slots - {request}");
            var slots = await _slotsRepository.List(request.Count, request.Page);
            return new GetSlotsQueryResponse{ Items =_mapper.Map<List<GetSlotQueryResponse>>(slots) };
        }
    }
}
