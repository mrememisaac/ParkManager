using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Occasions.Commands.AddOccasion
{
    public class AddOccasionCommandHandler : IRequestHandler<AddOccasionCommand, AddOccasionCommandResponse>
    {
        private readonly IOccasionsRepository _occasionRepository;
        private readonly AddOccasionCommandValidator _validator;
        private readonly IMapper _mapper; 
        private readonly ILogger<AddOccasionCommandHandler> _logger;

        public AddOccasionCommandHandler(IOccasionsRepository occasionRepository, AddOccasionCommandValidator validator, IMapper mapper, ILogger<AddOccasionCommandHandler> logger)
        {
            _occasionRepository = occasionRepository;
            _validator = validator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<AddOccasionCommandResponse> Handle(AddOccasionCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Handling AddOccasionCommand for {request}");
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            var occasion = _mapper.Map<Occasion>(request);
            var response = await _occasionRepository.Add(occasion);
            return _mapper.Map<AddOccasionCommandResponse>(response);
        }
    }
}
