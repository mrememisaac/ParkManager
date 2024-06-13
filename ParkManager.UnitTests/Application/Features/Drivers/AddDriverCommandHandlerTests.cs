using AutoMapper;
using FluentValidation;
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
        private readonly AddDriverCommandValidator _validator;
        private readonly AddDriverCommand _command;

        public AddDriverCommandHandlerTests()
        {
            _mockDriversRepository = new Mock<IDriversRepository>();
            _mockMapper = new Mock<IMapper>();
            _validator = new AddDriverCommandValidator();
            _command = CreateAddDriverCommand();
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
            var handler = new AddDriverCommandHandler(_mockDriversRepository.Object, _validator, _mockMapper.Object);
            var expectedResponse = new AddDriverCommandResponse()
            {
                Name = driver.Name,
                PhoneNumber = driver.PhoneNumber
            };
            _mockMapper.Setup(m => m.Map<Driver>(It.IsAny<AddDriverCommand>())).Returns(driver);
            _mockMapper.Setup(m => m.Map<AddDriverCommandResponse>(It.IsAny<Driver>())).Returns(expectedResponse);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(expectedResponse, result);
        }

        [Fact]
        public async Task Handle_ShouldThrowValidationException_WhenCommandIsInvalid()
        {
            // Arrange
            var command = new AddDriverCommand(); // Populate with invalid properties
            var handler = new AddDriverCommandHandler(_mockDriversRepository.Object, _validator, _mockMapper.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldCallAddOnRepository_WithMappedDriver()
        {
            // Arrange
            var command = _command;
            var handler = new AddDriverCommandHandler(_mockDriversRepository.Object, _validator, _mockMapper.Object);
            var mappedDriver = new Driver(command.Id, command.Name, command.PhoneNumber); // Adjust mapping as necessary
            _mockMapper.Setup(m => m.Map<Driver>(It.IsAny<AddDriverCommand>())).Returns(mappedDriver);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            _mockDriversRepository.Verify(r => r.Add(mappedDriver), Times.Once);
        }
    }
}
