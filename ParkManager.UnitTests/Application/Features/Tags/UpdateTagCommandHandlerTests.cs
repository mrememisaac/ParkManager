using AutoMapper;
using FluentValidation;
using MediatR;
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
        private readonly UpdateTagCommandValidator _validator;
        private readonly UpdateTagCommand _command;

        public UpdateTagCommandHandlerTests()
        {
            _mockTagsRepository = new Mock<ITagsRepository>();
            _mockMapper = new Mock<IMapper>();
            _validator = new UpdateTagCommandValidator();
            _command = CreateUpdateTagCommand();
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
            var handler = new UpdateTagCommandHandler(_mockTagsRepository.Object, _validator, _mockMapper.Object);
            var expectedResponse = new UpdateTagCommandResponse { Id = tag.Id, Number = tag.Number };
            _mockMapper.Setup(m => m.Map<Tag>(It.IsAny<UpdateTagCommand>())).Returns(tag);
            _mockMapper.Setup(m => m.Map<UpdateTagCommandResponse>(It.IsAny<Tag>())).Returns(expectedResponse);
            _mockTagsRepository.Setup(repo => repo.Update(It.IsAny<Tag>())).ReturnsAsync(tag);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

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
            var handler = new UpdateTagCommandHandler(_mockTagsRepository.Object, _validator, _mockMapper.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldCallUpdateOnRepository_WithMappedTag()
        {
            // Arrange
            var command = _command;
            var mappedTag = new Tag (command.Id, command.Number);
            _mockMapper.Setup(m => m.Map<Tag>(It.IsAny<UpdateTagCommand>())).Returns(mappedTag);
            var handler = new UpdateTagCommandHandler(_mockTagsRepository.Object, _validator, _mockMapper.Object);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            _mockTagsRepository.Verify(r => r.Update(mappedTag), Times.Once);
        }
    }
}

