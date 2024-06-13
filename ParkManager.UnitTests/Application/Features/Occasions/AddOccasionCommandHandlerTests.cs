using AutoMapper;
using FluentValidation;
using MediatR;
using Moq;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Occasions.Commands.AddOccasion;
using ParkManager.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ParkManager.UnitTests
{
    public class AddOccasionCommandHandlerTests
    {
        private readonly Mock<IOccasionsRepository> _mockOccasionsRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly AddOccasionCommandValidator _validator;
        private readonly IRequestHandler<AddOccasionCommand, AddOccasionCommandResponse> _handler;
        private readonly AddOccasionCommand _command;

        public AddOccasionCommandHandlerTests()
        {
            _mockOccasionsRepository = new Mock<IOccasionsRepository>();
            _mockMapper = new Mock<IMapper>();
            _validator = new AddOccasionCommandValidator();
            _handler = new AddOccasionCommandHandler(_mockOccasionsRepository.Object, _validator, _mockMapper.Object);
            _command = new AddOccasionCommand { Id = Guid.NewGuid(), EndDate = DateTime.Now.AddDays(1), StartDate = DateTime.Now, Name = "Occasion 1" };
        }

        [Fact]
        public async Task Handle_ShouldReturnExpectedResponse_WhenCommandIsValid()
        {
            // Arrange
            var command = _command;
            var occasion = new Occasion(command.Id, "Occasion 1", command.StartDate, command.EndDate);
            var expectedResponse = new AddOccasionCommandResponse
            {
                Id = occasion.Id, EndDate = occasion.EndDate, StartDate = occasion.StartDate, Name = occasion.Name
            };
            _mockMapper.Setup(m => m.Map<Occasion>(It.IsAny<AddOccasionCommand>())).Returns(occasion);
            _mockMapper.Setup(m => m.Map<AddOccasionCommandResponse>(It.IsAny<Occasion>())).Returns(expectedResponse);
            _mockOccasionsRepository.Setup(repo => repo.Add(It.IsAny<Occasion>())).ReturnsAsync(occasion);

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
            var command = new AddOccasionCommand(); // Populate with invalid properties

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => _validator.ValidateAndThrowAsync(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldCallAddOnRepository_WithMappedOccasion()
        {
            // Arrange
            var command = _command;
            var mappedOccasion = new Occasion(command.Id, "Occasion 1", command.StartDate, command.EndDate);

            _mockMapper.Setup(m => m.Map<Occasion>(It.IsAny<AddOccasionCommand>())).Returns(mappedOccasion);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockOccasionsRepository.Verify(r => r.Add(mappedOccasion), Times.Once);
        }
    }
}
