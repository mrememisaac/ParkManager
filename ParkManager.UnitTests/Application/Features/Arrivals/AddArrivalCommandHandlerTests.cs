using AutoMapper;
using FluentValidation;
using MediatR;
using Moq;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Arrivals.Commands.AddArrival;
using ParkManager.Domain;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ParkManager.UnitTests
{
    public class AddArrivalCommandHandlerTests
    {
        private readonly Mock<IArrivalsRepository> _mockArrivalsRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly AddArrivalCommandValidator _validator;
        private readonly AddArrivalCommand _command;

        public AddArrivalCommandHandlerTests()
        {
            _mockArrivalsRepository = new Mock<IArrivalsRepository>();
            _mockMapper = new Mock<IMapper>();
            _validator = new AddArrivalCommandValidator();
            _command = CreateAddArrivalCommand();
        }

        private AddArrivalCommand CreateAddArrivalCommand(bool validInstance = true)
        {
            if (validInstance)
            {
                return new AddArrivalCommand()
                {
                    Id = Guid.NewGuid(),
                    Timestamp = DateTime.Now,
                    VehicleId = Guid.NewGuid(),
                    DriverId = Guid.NewGuid(),
                    TagId = Guid.NewGuid(),
                    ParkId = Guid.NewGuid()
                };
            }
            return new AddArrivalCommand();
        }

        [Fact]
        public async Task Handle_ShouldReturnExpectedResponse_WhenCommandIsValid()
        {
            // Arrange
            var command = _command;

            var arrival = new Arrival(command.Id,command.Timestamp, command.ParkId, command.VehicleId, command.DriverId, command.TagId);

            var handler = new AddArrivalCommandHandler(_mockArrivalsRepository.Object, _validator, _mockMapper.Object);
            
            var expectedResponse = new AddArrivalCommandResponse();
            expectedResponse.DriverId = arrival.DriverId;
            expectedResponse.VehicleId = arrival.VehicleId;
            expectedResponse.TagId = arrival.TagId;
            expectedResponse.ParkId = arrival.ParkId;
            _mockMapper.Setup(m => m.Map<Arrival>(It.IsAny<AddArrivalCommand>())).Returns(arrival); // Adjust mapping as necessary
            _mockMapper.Setup(m => m.Map<AddArrivalCommandResponse>(It.IsAny<Arrival>())).Returns(expectedResponse);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(expectedResponse, result);
        }

        [Fact]
        public async Task Handle_ShouldThrowValidationException_WhenCommandIsInvalid()
        {
            // Arrange
            var command = new AddArrivalCommand(); 

            var handler = new AddArrivalCommandHandler(_mockArrivalsRepository.Object, _validator, _mockMapper.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldCallAddOnRepository_WithMappedArrival()
        {
            // Arrange
            var command = _command;

            var handler = new AddArrivalCommandHandler(_mockArrivalsRepository.Object, _validator, _mockMapper.Object);
            var mappedArrival = new Arrival(command.Id, command.Timestamp, command.ParkId, command.VehicleId, command.DriverId, command.TagId);

            _mockMapper.Setup(m => m.Map<Arrival>(It.IsAny<AddArrivalCommand>())).Returns(mappedArrival);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            _mockArrivalsRepository.Verify(r => r.Add(mappedArrival), Times.Once);
        }
    }
}
