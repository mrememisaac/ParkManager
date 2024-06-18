using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Lanes.Commands.AddLane;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Lanes.Commands.UpdateLane
{
    public class UpdateLaneCommandHandler : IRequestHandler<UpdateLaneCommand, UpdateLaneCommandResponse>
    {
        private readonly ILanesRepository _lanesRepository;
        private readonly UpdateLaneCommandValidator _validator;
        private readonly IMapper _mapper; 
        private readonly ILogger<UpdateLaneCommandHandler> _logger;

        public UpdateLaneCommandHandler(ILanesRepository laneRepository, UpdateLaneCommandValidator validator, IMapper mapper, ILogger<UpdateLaneCommandHandler> logger)
        {
            _lanesRepository = laneRepository;
            _validator = validator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UpdateLaneCommandResponse> Handle(UpdateLaneCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Handling {nameof(UpdateLaneCommand)} - {request}");
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            var lane = _mapper.Map<Lane>(request);
            var response = await _lanesRepository.Update(lane);
            return _mapper.Map<UpdateLaneCommandResponse>(response);
        }
    }
}
