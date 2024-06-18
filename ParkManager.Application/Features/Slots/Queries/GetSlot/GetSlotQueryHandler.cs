using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Slots.Queries.GetSlot
{
    public class GetSlotQueryHandler : IRequestHandler<GetSlotQuery, GetSlotQueryResponse>
    {
        private readonly ISlotsRepository _repository;
        private readonly IMapper _mapper; 
        private readonly ILogger<GetSlotQueryHandler> _logger;

        public GetSlotQueryHandler(ISlotsRepository repository, IMapper mapper, ILogger<GetSlotQueryHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetSlotQueryResponse> Handle(GetSlotQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Retrieving slot with id: {request.Id}");
            var slot = await _repository.Get(request.Id);
            return _mapper.Map<GetSlotQueryResponse>(slot);
        }
    }
}