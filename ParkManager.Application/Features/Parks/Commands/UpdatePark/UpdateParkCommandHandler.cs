using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Parks.Commands.AddPark;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Parks.Commands.UpdatePark
{
    public class UpdateParkCommandHandler : IRequestHandler<UpdateParkCommand, UpdateParkCommandResponse>
    {
        private readonly IParksRepository _parksRepository;
        private readonly UpdateParkCommandValidator _validator;
        private readonly IMapper _mapper; 
        private readonly ILogger<UpdateParkCommandHandler> _logger;

        public UpdateParkCommandHandler(IParksRepository parkRepository, UpdateParkCommandValidator validator, IMapper mapper, ILogger<UpdateParkCommandHandler> logger)
        {
            _parksRepository = parkRepository;
            _validator = validator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UpdateParkCommandResponse> Handle(UpdateParkCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Handling {nameof(UpdateParkCommand)} - Payload: {request}");
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            var park = _mapper.Map<Park>(request);
            var response = await _parksRepository.Update(park);
            return _mapper.Map<UpdateParkCommandResponse>(response);
        }
    }
}
