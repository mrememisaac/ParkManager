using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Tags.Commands.AddTag;
using ParkManager.Domain;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ParkManager.UnitTests
{
    public class AddTagCommandHandlerTests
    {
        private readonly Mock<ITagsRepository> _mockTagsRepository;
        private readonly Mock<IMapper> _mockMapper; 
        private readonly Mock<ILogger<AddTagCommandHandler>> _mockLogger;
        private readonly AddTagCommandValidator _validator;
        private readonly AddTagCommand _command;
        private readonly AddTagCommandHandler _handler;

        public AddTagCommandHandlerTests()
        {
            _mockTagsRepository = new Mock<ITagsRepository>();
            _mockMapper = new Mock<IMapper>();
            _validator = new AddTagCommandValidator();
            _command = CreateAddTagCommand();
            _mockLogger = new Mock<ILogger<AddTagCommandHandler>>();
            _handler = new AddTagCommandHandler(_mockTagsRepository.Object, _validator, _mockMapper.Object, _mockLogger.Object);
        }

        private AddTagCommand CreateAddTagCommand(bool validInstance = true)
        {
            if (validInstance)
            {
                return new AddTagCommand()
                {
                    Id = Guid.NewGuid(),
                    Number = 1
                };
            }
            return new AddTagCommand();
        }

        [Fact]
        public async Task Handle_ShouldReturnExpectedResponse_WhenCommandIsValid()
        {
            // Arrange
            var command = _command;
            var tag = new Tag(command.Id, command.Number);
            var expectedResponse = new AddTagCommandResponse { Id = tag.Id, Number = tag.Number };
            _mockMapper.Setup(m => m.Map<Tag>(It.IsAny<AddTagCommand>())).Returns(tag);
            _mockMapper.Setup(m => m.Map<AddTagCommandResponse>(It.IsAny<Tag>())).Returns(expectedResponse);
            _mockTagsRepository.Setup(repo => repo.Add(It.IsAny<Tag>())).ReturnsAsync(tag);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResponse.Number, result.Number);
        }

        [Fact]
        public async Task Handle_ShouldThrowValidationException_WhenCommandIsInvalid()
        {
            // Arrange
            var command = new AddTagCommand(); // Invalid because Name is not set

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldCallAddOnRepository_WithMappedTag()
        {
            // Arrange
            var command = _command;
            var mappedTag = new Tag(command.Id, command.Number);
            _mockMapper.Setup(m => m.Map<Tag>(It.IsAny<AddTagCommand>())).Returns(mappedTag);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockTagsRepository.Verify(r => r.Add(mappedTag), Times.Once);
        }
    }
}
