using AutoMapper;
using MediatR;
using Moq;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Parks.Commands.RemovePark;
using ParkManager.Domain.Exceptions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ParkManager.UnitTests
{
    public class RemoveParkCommandHandlerTests
    {
        private readonly Mock<IParksRepository> _mockParksRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly IRequestHandler<RemoveParkCommand> _handler;

        public RemoveParkCommandHandlerTests()
        {
            _mockParksRepository = new Mock<IParksRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new RemoveParkCommandHandler(_mockParksRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ShouldCallDeleteOnRepository_WithCorrectParkId()
        {
            // Arrange
            var parkId = Guid.NewGuid();
            var command = new RemoveParkCommand(parkId);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockParksRepository.Verify(r => r.Delete(parkId), Times.Once);
        }       
    }
}
