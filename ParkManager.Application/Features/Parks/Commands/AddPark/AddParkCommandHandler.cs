using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Parks.Commands.AddPark
{
    public class AddParkCommandHandler : IRequestHandler<AddParkCommand, AddParkCommandResponse>
    {
        private readonly IParksRepository _parkRepository;
        private readonly AddParkCommandValidator _validator;
        private readonly IMapper _mapper; 
        private readonly ILogger<AddParkCommandHandler> _logger;

        public AddParkCommandHandler(IParksRepository parkRepository, AddParkCommandValidator validator, IMapper mapper, ILogger<AddParkCommandHandler> logger)
        {
            _parkRepository = parkRepository;
            _validator = validator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<AddParkCommandResponse> Handle(AddParkCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Handling {nameof(AddParkCommand)} - Payload: {request}");
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            var park = _mapper.Map<Park>(request);
            var response = await _parkRepository.Add(park);
            return _mapper.Map<AddParkCommandResponse>(response);
        }
    }
}
