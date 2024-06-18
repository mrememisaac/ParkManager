using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Departures.Commands.AddDeparture;
using ParkManager.Domain;

namespace ParkManager.UnitTests
{
    public class AddDepartureCommandHandlerTests
    {
        private readonly Mock<IDeparturesRepository> _mockDeparturesRepository;
        private readonly Mock<IMapper> _mockMapper; 
        private readonly Mock<ILogger<AddDepartureCommandHandler>> _mockLogger;
        private readonly AddDepartureCommandValidator _validator;
        private readonly AddDepartureCommand _command;
        private readonly AddDepartureCommandHandler _handler;

        public AddDepartureCommandHandlerTests()
        {
            _mockDeparturesRepository = new Mock<IDeparturesRepository>();
            _mockMapper = new Mock<IMapper>();
            _validator = new AddDepartureCommandValidator();
            _command = CreateAddDepartureCommand();
            _mockLogger = new Mock<ILogger<AddDepartureCommandHandler>>();
            _handler = new AddDepartureCommandHandler(_mockDeparturesRepository.Object, _validator, _mockMapper.Object, _mockLogger.Object);
        }

        private AddDepartureCommand CreateAddDepartureCommand(bool validInstance = true)
        {
            if (validInstance)
            {
                return new AddDepartureCommand()
                {
                    Id = Guid.NewGuid(),
                    Timestamp = DateTime.Now,
                    VehicleId = Guid.NewGuid(),
                    DriverId = Guid.NewGuid(),
                    TagId = Guid.NewGuid(),
                    ParkId = Guid.NewGuid()
                };
            }
            return new AddDepartureCommand();
        }

        [Fact]
        public async Task Handle_ShouldReturnExpectedResponse_WhenCommandIsValid()
        {
            // Arrange
            var command = _command;
            var departure = new Departure(command.Id, command.Timestamp, command.ParkId, command.VehicleId, command.DriverId, command.TagId);
            var expectedResponse = new AddDepartureCommandResponse();
            expectedResponse.DriverId = departure.DriverId;
            expectedResponse.VehicleId = departure.VehicleId;
            expectedResponse.TagId = departure.TagId;
            expectedResponse.ParkId = departure.ParkId;
            _mockMapper.Setup(m => m.Map<Departure>(It.IsAny<AddDepartureCommand>())).Returns(departure);
            _mockMapper.Setup(m => m.Map<AddDepartureCommandResponse>(It.IsAny<Departure>())).Returns(expectedResponse);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(expectedResponse, result);
        }

        [Fact]
        public async Task Handle_ShouldThrowValidationException_WhenCommandIsInvalid()
        {
            // Arrange
            var command = new AddDepartureCommand();

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldCallAddOnRepository_WithMappedDeparture()
        {
            // Arrange
            var command = _command;
            var mappedDeparture = new Departure(Guid.NewGuid(), command.Timestamp, command.ParkId, command.VehicleId, command.DriverId, command.TagId);
            _mockMapper.Setup(m => m.Map<Departure>(It.IsAny<AddDepartureCommand>())).Returns(mappedDeparture);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockDeparturesRepository.Verify(r => r.Add(mappedDeparture), Times.Once);
        }
    }
}