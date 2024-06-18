using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Drivers.Commands.AddDriver;
using ParkManager.Domain;

namespace ParkManager.UnitTests
{
    public class AddDriverCommandHandlerTests
    {
        private readonly Mock<IDriversRepository> _mockDriversRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILogger<AddDriverCommandHandler>> _mockLogger;
        private readonly AddDriverCommandValidator _validator;
        private readonly AddDriverCommand _command;
        private readonly AddDriverCommandHandler _handler;

        public AddDriverCommandHandlerTests()
        {
            _mockDriversRepository = new Mock<IDriversRepository>();
            _mockMapper = new Mock<IMapper>();
            _validator = new AddDriverCommandValidator();
            _command = CreateAddDriverCommand();
            _mockLogger = new Mock<ILogger<AddDriverCommandHandler>>();
            _handler = new AddDriverCommandHandler(_mockDriversRepository.Object, _validator, _mockMapper.Object, _mockLogger.Object);
        }

        private AddDriverCommand CreateAddDriverCommand(bool validInstance = true)
        {
            if (validInstance)
            {
                return new AddDriverCommand()
                {
                    Id = Guid.NewGuid(),
                    Name = "Emem",
                    PhoneNumber = "08012345678"
                };
            }
            return new AddDriverCommand();
        }

        [Fact]
        public async Task Handle_ShouldReturnExpectedResponse_WhenCommandIsValid()
        {
            // Arrange
            var command = _command;
            var driver = new Driver(command.Id, _command.Name, _command.PhoneNumber);
            var expectedResponse = new AddDriverCommandResponse()
            {
                Name = driver.Name,
                PhoneNumber = driver.PhoneNumber
            };
            _mockMapper.Setup(m => m.Map<Driver>(It.IsAny<AddDriverCommand>())).Returns(driver);
            _mockMapper.Setup(m => m.Map<AddDriverCommandResponse>(It.IsAny<Driver>())).Returns(expectedResponse);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(expectedResponse, result);
        }

        [Fact]
        public async Task Handle_ShouldThrowValidationException_WhenCommandIsInvalid()
        {
            // Arrange
            var command = new AddDriverCommand(); // Populate with invalid properties

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldCallAddOnRepository_WithMappedDriver()
        {
            // Arrange
            var command = _command;
            var mappedDriver = new Driver(command.Id, command.Name, command.PhoneNumber); // Adjust mapping as necessary
            _mockMapper.Setup(m => m.Map<Driver>(It.IsAny<AddDriverCommand>())).Returns(mappedDriver);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockDriversRepository.Verify(r => r.Add(mappedDriver), Times.Once);
        }
    }
}
