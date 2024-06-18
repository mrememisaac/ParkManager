using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Drivers.Commands.AddDriver
{
    public class AddDriverCommandHandler : IRequestHandler<AddDriverCommand, AddDriverCommandResponse>
    {
        private readonly IDriversRepository _driverRepository;
        private readonly AddDriverCommandValidator _validator;
        private readonly IMapper _mapper; 
        private readonly ILogger<AddDriverCommandHandler> _logger;

        public AddDriverCommandHandler(IDriversRepository driverRepository, AddDriverCommandValidator validator, IMapper mapper, ILogger<AddDriverCommandHandler> logger)
        {
            _driverRepository = driverRepository;
            _validator = validator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<AddDriverCommandResponse> Handle(AddDriverCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Handling AddDriverCommand for {request}");
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            var driver = _mapper.Map<Driver>(request);
            var response = await _driverRepository.Add(driver);
            return _mapper.Map<AddDriverCommandResponse>(response);
        }
    }
}
