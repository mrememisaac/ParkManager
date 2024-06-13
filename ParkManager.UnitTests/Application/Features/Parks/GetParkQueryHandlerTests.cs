using MediatR;
using Moq;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Parks.Commands.UpdatePark;
using ParkManager.Application.Features.Parks.Queries.GetPark;
using ParkManager.Domain;

namespace ParkManager.UnitTests
{
    public class GetParkQueryHandlerTests
    {
        private readonly Mock<IParksRepository> _mockParksRepository;
        private readonly IRequestHandler<GetParkQuery, Park> _handler;

        public GetParkQueryHandlerTests()
        {
            _mockParksRepository = new Mock<IParksRepository>();
            _handler = new GetParkQueryHandler(_mockParksRepository.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnPark_WhenParkExists()
        {
            // Arrange
            var parkId = Guid.NewGuid();
            var command = new UpdateParkCommand
            {
                Id = parkId,
                City = "City",
                Name = "Name",
                Street = "Street",
                State = "State",
                Country = "Country",
                Latitude = 0,
                Longitude = 0,
            };
            var expectedPark = new Park(command.Id, command.Name, command.Street, command.City, command.State, command.Country, command.Latitude, command.Longitude);
            _mockParksRepository.Setup(repo => repo.Get(parkId)).ReturnsAsync(expectedPark);
            var query = new GetParkQuery(parkId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedPark.Id, result.Id);
        }
    }
}
