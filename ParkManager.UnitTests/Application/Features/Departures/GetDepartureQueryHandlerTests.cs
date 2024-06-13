using MediatR;
using Moq;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Departures.Queries.GetDeparture;
using ParkManager.Domain;

namespace ParkManager.UnitTests
{
    public class GetDepartureQueryHandlerTests
    {
        private readonly Mock<IDeparturesRepository> _mockDeparturesRepository;
        private readonly IRequestHandler<GetDepartureQuery, Departure> _handler;

        public GetDepartureQueryHandlerTests()
        {
            _mockDeparturesRepository = new Mock<IDeparturesRepository>();
            _handler = new GetDepartureQueryHandler(_mockDeparturesRepository.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnCorrectDeparture_WhenIdIsValid()
        {
            // Arrange
            var validId = Guid.NewGuid();
            var expectedDeparture = new Departure(validId, DateTime.Now, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            _mockDeparturesRepository.Setup(repo => repo.Get(validId)).ReturnsAsync(expectedDeparture);
            var query = new GetDepartureQuery(validId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedDeparture, result);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenDepartureDoesNotExist()
        {
            // Arrange
            var invalidId = Guid.NewGuid();
            _mockDeparturesRepository.Setup(repo => repo.Get(invalidId)).ReturnsAsync((Departure)null);
            var query = new GetDepartureQuery(invalidId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}
