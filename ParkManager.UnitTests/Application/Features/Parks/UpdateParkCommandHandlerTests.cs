using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Parks.Commands.UpdatePark;
using ParkManager.Domain;

namespace ParkManager.UnitTests
{
    public class UpdateParkCommandHandlerTests
    {
        private readonly Mock<IParksRepository> _mockParksRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UpdateParkCommandValidator _mockValidator;
        private readonly UpdateParkCommandHandler _handler;
        private readonly UpdateParkCommand _command;

        public UpdateParkCommandHandlerTests()
        {
            _mockParksRepository = new Mock<IParksRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockValidator = new UpdateParkCommandValidator();
            _handler = new UpdateParkCommandHandler(_mockParksRepository.Object, _mockValidator, _mockMapper.Object);
            _command = new UpdateParkCommand
            {
                Id = Guid.NewGuid(),
                City = "City",
                Name = "Name",
                Street = "Street",
                State = "State",
                Country = "Country",
                Latitude = 1.1,
                Longitude = 1.1
            };
        }

        [Fact]
        public async Task Handle_ShouldReturnExpectedResponse_WhenCommandIsValid()
        {
            // Arrange
            var command = _command;
            var park = new Park(command.Id, command.Name, command.Street, command.City, command.State, command.Country, command.Latitude, command.Longitude);
            var expectedResponse = new UpdateParkCommandResponse
            {
                Id = park.Id,
                Longitude = park.Longitude,
                Latitude = park.Latitude,
                Country = park.Country,
                State = park.State,
                City = park.City,
                Street = park.Street,
                Name = park.Name
            };
            _mockMapper.Setup(m => m.Map<Park>(It.IsAny<UpdateParkCommand>())).Returns(park);
            _mockMapper.Setup(m => m.Map<UpdateParkCommandResponse>(It.IsAny<Park>())).Returns(expectedResponse);
            _mockParksRepository.Setup(repo => repo.Update(park)).ReturnsAsync(park);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            // Additional assertions for response properties if necessary
        }

        [Fact]
        public async Task Handle_ShouldThrowValidationException_WhenCommandIsInvalid()
        {
            // Arrange
            var command = new UpdateParkCommand(); // Populate with invalid properties
            
            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldCallUpdateOnRepository_WithMappedPark()
        {
            // Arrange
            var command = _command;
            var mappedPark = new Park(command.Id, command.Name, command.Street, command.City, command.State, command.Country, command.Latitude, command.Longitude);
            _mockMapper.Setup(m => m.Map<Park>(It.IsAny<UpdateParkCommand>())).Returns(mappedPark);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockParksRepository.Verify(r => r.Update(mappedPark), Times.Once);
        }
    }
}
