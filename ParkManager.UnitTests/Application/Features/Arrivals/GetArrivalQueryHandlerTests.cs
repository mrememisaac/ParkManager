using MediatR;
using Moq;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Arrivals.Queries.GetArrival;
using ParkManager.Domain;

namespace ParkManager.UnitTests
{
    public class GetArrivalQueryHandlerTests
    {
        private readonly Mock<IArrivalsRepository> _mockArrivalsRepository;
        private readonly IRequestHandler<GetArrivalQuery, Arrival> _handler;

        public GetArrivalQueryHandlerTests()
        {
            _mockArrivalsRepository = new Mock<IArrivalsRepository>();
            _handler = new GetArrivalQueryHandler(_mockArrivalsRepository.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnCorrectArrival_WhenIdIsValid()
        {
            // Arrange
            var validId = Guid.NewGuid();
            var expectedArrival = new Arrival(validId, DateTime.Now, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            _mockArrivalsRepository.Setup(repo => repo.Get(validId)).ReturnsAsync(expectedArrival);
            var query = new GetArrivalQuery(validId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedArrival, result);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenArrivalDoesNotExist()
        {
            // Arrange
            var invalidId = Guid.NewGuid();
            _mockArrivalsRepository.Setup(repo => repo.Get(invalidId)).ReturnsAsync((Arrival)null);
            var query = new GetArrivalQuery(invalidId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}
