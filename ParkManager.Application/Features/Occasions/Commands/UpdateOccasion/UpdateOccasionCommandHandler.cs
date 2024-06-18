using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Occasions.Commands.AddOccasion;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Occasions.Commands.UpdateOccasion
{
    public class UpdateOccasionCommandHandler : IRequestHandler<UpdateOccasionCommand, UpdateOccasionCommandResponse>
    {
        private readonly IOccasionsRepository _occasionsRepository;
        private readonly UpdateOccasionCommandValidator _validator;
        private readonly IMapper _mapper; 
        private readonly ILogger<UpdateOccasionCommandHandler> _logger;

        public UpdateOccasionCommandHandler(IOccasionsRepository occasionRepository, UpdateOccasionCommandValidator validator, IMapper mapper, ILogger<UpdateOccasionCommandHandler> logger)
        {
            _occasionsRepository = occasionRepository;
            _validator = validator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UpdateOccasionCommandResponse> Handle(UpdateOccasionCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Handling {nameof(UpdateOccasionCommand)} - {request}");
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            var occasion = _mapper.Map<Occasion>(request);
            var response = await _occasionsRepository.Update(occasion);
            return _mapper.Map<UpdateOccasionCommandResponse>(response);
        }
    }
}
