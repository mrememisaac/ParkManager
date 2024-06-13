using AutoMapper;
using FluentValidation;
using MediatR;
using Moq;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Parks.Commands.AddPark;
using ParkManager.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ParkManager.UnitTests
{
    public class AddParkCommandHandlerTests
    {
        private readonly Mock<IParksRepository> _mockParksRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly AddParkCommandValidator _validator;
        private readonly IRequestHandler<AddParkCommand, AddParkCommandResponse> _handler;
        private readonly AddParkCommand _command;

        public AddParkCommandHandlerTests()
        {
            _mockParksRepository = new Mock<IParksRepository>();
            _mockMapper = new Mock<IMapper>();
            _validator = new AddParkCommandValidator();
            _handler = new AddParkCommandHandler(_mockParksRepository.Object, _validator, _mockMapper.Object);
            _command = new AddParkCommand
            {
                Id = Guid.NewGuid(),
                City = "City",
                Name = "Name",
                Street = "Street",
                State = "State",
                Country = "Country",
                Latitude = 11,
                Longitude = 2.2,
            };
        }

        [Fact]
        public async Task Handle_ShouldReturnExpectedResponse_WhenCommandIsValid()
        {
            // Arrange
            var command = _command;
            var park = new Park(command.Id,command.Name, command.Street, command.City, command.State, command.Country, command.Latitude, command.Longitude); 
            var expectedResponse = new AddParkCommandResponse(); // Populate with expected properties
            _mockMapper.Setup(m => m.Map<Park>(It.IsAny<AddParkCommand>())).Returns(park);
            _mockMapper.Setup(m => m.Map<AddParkCommandResponse>(It.IsAny<Park>())).Returns(expectedResponse);
            _mockParksRepository.Setup(repo => repo.Add(park)).ReturnsAsync(park);

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
            var command = new AddParkCommand(); // Populate with invalid properties

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => _validator.ValidateAndThrowAsync(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldCallAddOnRepository_WithMappedPark()
        {
            // Arrange
            var command = _command;
            var mappedPark = new Park(command.Id, command.Name, command.Street, command.City, command.State, command.Country, command.Latitude, command.Longitude);
            _mockMapper.Setup(m => m.Map<Park>(It.IsAny<AddParkCommand>())).Returns(mappedPark);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockParksRepository.Verify(r => r.Add(mappedPark), Times.Once);
        }
    }
}
