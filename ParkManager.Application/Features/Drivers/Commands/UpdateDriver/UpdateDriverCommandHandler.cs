using AutoMapper;
using FluentValidation;
using MediatR;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;

namespace ParkManager.Application.Features.Drivers.Commands.UpdateDriver
{
    public class UpdateDriverCommandHandler : IRequestHandler<UpdateDriverCommand, UpdateDriverCommandResponse>
    {
        private readonly IDriversRepository _driverRepository;
        private readonly UpdateDriverCommandValidator _validator;
        private readonly IMapper _mapper;

        public UpdateDriverCommandHandler(IDriversRepository driverRepository, UpdateDriverCommandValidator validator, IMapper mapper)
        {
            _driverRepository = driverRepository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<UpdateDriverCommandResponse> Handle(UpdateDriverCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            var driver = _mapper.Map<Driver>(request);
            var response = await _driverRepository.Update(driver);
            return _mapper.Map<UpdateDriverCommandResponse>(response);
        }
    }
}
