using AutoMapper;
using FluentValidation;
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
        private readonly AddDepartureCommandValidator _validator;
        private readonly AddDepartureCommand _command;

        public AddDepartureCommandHandlerTests()
        {
            _mockDeparturesRepository = new Mock<IDeparturesRepository>();
            _mockMapper = new Mock<IMapper>();
            _validator = new AddDepartureCommandValidator();
            _command = CreateAddDepartureCommand();
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
            var handler = new AddDepartureCommandHandler(_mockDeparturesRepository.Object, _validator, _mockMapper.Object);
            var expectedResponse = new AddDepartureCommandResponse();
            expectedResponse.DriverId = departure.DriverId;
            expectedResponse.VehicleId = departure.VehicleId;
            expectedResponse.TagId = departure.TagId;
            expectedResponse.ParkId = departure.ParkId;
            _mockMapper.Setup(m => m.Map<Departure>(It.IsAny<AddDepartureCommand>())).Returns(departure);
            _mockMapper.Setup(m => m.Map<AddDepartureCommandResponse>(It.IsAny<Departure>())).Returns(expectedResponse);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(expectedResponse, result);
        }

        [Fact]
        public async Task Handle_ShouldThrowValidationException_WhenCommandIsInvalid()
        {
            // Arrange
            var command = new AddDepartureCommand();
            var handler = new AddDepartureCommandHandler(_mockDeparturesRepository.Object, _validator, _mockMapper.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldCallAddOnRepository_WithMappedDeparture()
        {
            // Arrange
            var command = _command;
            var handler = new AddDepartureCommandHandler(_mockDeparturesRepository.Object, _validator, _mockMapper.Object);
            var mappedDeparture = new Departure(Guid.NewGuid(), command.Timestamp, command.ParkId, command.VehicleId, command.DriverId, command.TagId);
            _mockMapper.Setup(m => m.Map<Departure>(It.IsAny<AddDepartureCommand>())).Returns(mappedDeparture);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            _mockDeparturesRepository.Verify(r => r.Add(mappedDeparture), Times.Once);
        }
    }
}