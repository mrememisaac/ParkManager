using AutoMapper;
using FluentValidation;
using Moq;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Drivers.Commands.UpdateDriver;
using ParkManager.Domain;

namespace ParkManager.UnitTests
{
    public class UpdateDriverCommandHandlerTests
    {
        private readonly Mock<IDriversRepository> _mockDriversRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UpdateDriverCommandValidator _validator;
        private readonly UpdateDriverCommand _command;

        public UpdateDriverCommandHandlerTests()
        {
            _mockDriversRepository = new Mock<IDriversRepository>();
            _mockMapper = new Mock<IMapper>();
            _validator = new UpdateDriverCommandValidator();
            _command = new UpdateDriverCommand
            {
                Id = Guid.NewGuid(),
                Name = "Valid Name",
                PhoneNumber = "08012345678"
            };
        }

        [Fact]
        public async Task Handle_ShouldReturnExpectedResponse_WhenCommandIsValid()
        {
            // Arrange
            var command = _command;
            var driver = new Driver(command.Id, command.Name, command.PhoneNumber);
            var handler = new UpdateDriverCommandHandler(_mockDriversRepository.Object, _validator, _mockMapper.Object);
            var expectedResponse = new UpdateDriverCommandResponse()
            {
                Id = driver.Id,
                PhoneNumber = driver.PhoneNumber,
                Name = driver.Name
            };
            _mockMapper.Setup(m => m.Map<Driver>(It.IsAny<UpdateDriverCommand>())).Returns(driver);
            _mockMapper.Setup(m => m.Map<UpdateDriverCommandResponse>(It.IsAny<Driver>())).Returns(expectedResponse);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(expectedResponse, result);
        }

        [Fact]
        public async Task Handle_ShouldThrowValidationException_WhenCommandIsInvalid()
        {
            // Arrange
            var command = new UpdateDriverCommand(); // Populate with invalid properties
            var handler = new UpdateDriverCommandHandler(_mockDriversRepository.Object, _validator, _mockMapper.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldCallUpdateOnRepository_WithMappedDriver()
        {
            // Arrange
            var command = _command;
            var handler = new UpdateDriverCommandHandler(_mockDriversRepository.Object, _validator, _mockMapper.Object);
            var mappedDriver = new Driver(command.Id, command.Name, command.PhoneNumber); 
            _mockMapper.Setup(m => m.Map<Driver>(It.IsAny<UpdateDriverCommand>())).Returns(mappedDriver);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            _mockDriversRepository.Verify(r => r.Update(mappedDriver), Times.Once);
        }
    }
}
