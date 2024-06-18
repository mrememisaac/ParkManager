using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Tags.Commands.UpdateTag;
using ParkManager.Domain;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ParkManager.UnitTests
{
    public class UpdateTagCommandHandlerTests
    {
        private readonly Mock<ITagsRepository> _mockTagsRepository;
        private readonly Mock<IMapper> _mockMapper; 
        private readonly Mock<ILogger<UpdateTagCommandHandler>> _mockLogger;
        private readonly UpdateTagCommandValidator _validator;
        private readonly UpdateTagCommand _command;
        private readonly UpdateTagCommandHandler _handler;

        public UpdateTagCommandHandlerTests()
        {
            _mockTagsRepository = new Mock<ITagsRepository>();
            _mockMapper = new Mock<IMapper>();
            _validator = new UpdateTagCommandValidator();
            _command = CreateUpdateTagCommand();
            _mockLogger = new Mock<ILogger<UpdateTagCommandHandler>>();
            _handler = new UpdateTagCommandHandler(_mockTagsRepository.Object, _validator, _mockMapper.Object, _mockLogger.Object);
        }

        private UpdateTagCommand CreateUpdateTagCommand(bool validInstance = true)
        {
            if (validInstance)
            {
                return new UpdateTagCommand()
                {
                    Id = Guid.NewGuid(),
                    Number = 2
                };
            }
            return new UpdateTagCommand();
        }

        [Fact]
        public async Task Handle_ShouldReturnExpectedResponse_WhenCommandIsValid()
        {
            // Arrange
            var command = _command;
            var tag = new Tag(command.Id, command.Number);
            var expectedResponse = new UpdateTagCommandResponse { Id = tag.Id, Number = tag.Number };
            _mockMapper.Setup(m => m.Map<Tag>(It.IsAny<UpdateTagCommand>())).Returns(tag);
            _mockMapper.Setup(m => m.Map<UpdateTagCommandResponse>(It.IsAny<Tag>())).Returns(expectedResponse);
            _mockTagsRepository.Setup(repo => repo.Update(It.IsAny<Tag>())).ReturnsAsync(tag);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResponse.Id, result.Id);
            Assert.Equal(expectedResponse.Number, result.Number);
        }

        [Fact]
        public async Task Handle_ShouldThrowValidationException_WhenCommandIsInvalid()
        {
            // Arrange
            var command = new UpdateTagCommand(); // Invalid because Id and Name are not set

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldCallUpdateOnRepository_WithMappedTag()
        {
            // Arrange
            var command = _command;
            var mappedTag = new Tag (command.Id, command.Number);
            _mockMapper.Setup(m => m.Map<Tag>(It.IsAny<UpdateTagCommand>())).Returns(mappedTag);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockTagsRepository.Verify(r => r.Update(mappedTag), Times.Once);
        }
    }
}

