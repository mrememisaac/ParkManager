using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Tags.Commands.RemoveTag;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ParkManager.UnitTests
{
    public class RemoveTagCommandHandlerTests
    {
        private readonly Mock<ITagsRepository> _mockTagsRepository;
        private readonly Mock<IMapper> _mockMapper; 
        private readonly Mock<ILogger<RemoveTagCommandHandler>> _mockLogger;
        private readonly IRequestHandler<RemoveTagCommand> _handler;

        public RemoveTagCommandHandlerTests()
        {
            _mockTagsRepository = new Mock<ITagsRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<RemoveTagCommandHandler>>();
            _handler = new RemoveTagCommandHandler(_mockTagsRepository.Object, _mockMapper.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Handle_ShouldCallDeleteOnRepository_WithCorrectTagId()
        {
            // Arrange
            var tagId = Guid.NewGuid();
            var command = new RemoveTagCommand (tagId);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockTagsRepository.Verify(r => r.Delete(tagId), Times.Once);
        }
    }
}

