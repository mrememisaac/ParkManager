using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Occasions.Commands.UpdateOccasion;
using ParkManager.Domain;

namespace ParkManager.UnitTests
{
    public class UpdateOccasionCommandHandlerTests
    {
        private readonly Mock<IOccasionsRepository> _mockOccasionsRepository;
        private readonly Mock<IMapper> _mockMapper; 
        private readonly Mock<ILogger<UpdateOccasionCommandHandler>> _mockLogger;
        private readonly UpdateOccasionCommandValidator _validator;
        private readonly UpdateOccasionCommandHandler _handler;
        private readonly UpdateOccasionCommand _command;

        public UpdateOccasionCommandHandlerTests()
        {
            _mockOccasionsRepository = new Mock<IOccasionsRepository>();
            _mockMapper = new Mock<IMapper>();
            _validator = new UpdateOccasionCommandValidator();
            _mockLogger = new Mock<ILogger<UpdateOccasionCommandHandler>>();
            _handler = new UpdateOccasionCommandHandler(_mockOccasionsRepository.Object, _validator, _mockMapper.Object, _mockLogger.Object);
            _command = new UpdateOccasionCommand
            {
                Id = Guid.NewGuid(), EndDate = DateTime.Now.AddDays(1), StartDate = DateTime.Now, Name = "Test Occasion"
            };
        }

        [Fact]
        public async Task Handle_ShouldReturnExpectedResponse_WhenCommandIsValid()
        {
            // Arrange
            var command = _command;
            var occasion = new Occasion(command.Id, command.Name,command.StartDate, command.EndDate);
            var expectedResponse = new UpdateOccasionCommandResponse
            {
                EndDate = occasion.EndDate,
                StartDate = occasion.StartDate,
                Name = occasion.Name,
                Id = occasion.Id
            };
            _mockMapper.Setup(m => m.Map<Occasion>(It.IsAny<UpdateOccasionCommand>())).Returns(occasion);
            _mockMapper.Setup(m => m.Map<UpdateOccasionCommandResponse>(It.IsAny<Occasion>())).Returns(expectedResponse);
            _mockOccasionsRepository.Setup(repo => repo.Update(It.IsAny<Occasion>())).ReturnsAsync(occasion);

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
            var command = new UpdateOccasionCommand(); 

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => _validator.ValidateAndThrowAsync(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldCallUpdateOnRepository_WithMappedOccasion()
        {
            // Arrange
            var command = _command;
            var mappedOccasion = new Occasion(command.Id, command.Name, command.StartDate, command.EndDate); 
            _mockMapper.Setup(m => m.Map<Occasion>(It.IsAny<UpdateOccasionCommand>())).Returns(mappedOccasion);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockOccasionsRepository.Verify(r => r.Update(mappedOccasion), Times.Once);
        }
    }
}
