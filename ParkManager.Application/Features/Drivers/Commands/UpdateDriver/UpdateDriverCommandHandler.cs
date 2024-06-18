using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Drivers.Commands.UpdateDriver
{
    public class UpdateDriverCommandHandler : IRequestHandler<UpdateDriverCommand, UpdateDriverCommandResponse>
    {
        private readonly IDriversRepository _driverRepository;
        private readonly UpdateDriverCommandValidator _validator;
        private readonly IMapper _mapper; 
        private readonly ILogger<UpdateDriverCommandHandler> _logger;

        public UpdateDriverCommandHandler(IDriversRepository driverRepository, UpdateDriverCommandValidator validator, IMapper mapper, ILogger<UpdateDriverCommandHandler> logger)
        {
            _driverRepository = driverRepository;
            _validator = validator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UpdateDriverCommandResponse> Handle(UpdateDriverCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Handling {nameof(UpdateDriverCommand)} - {request}");
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            var driver = _mapper.Map<Driver>(request);
            var response = await _driverRepository.Update(driver);
            return _mapper.Map<UpdateDriverCommandResponse>(response);
        }
    }
}
