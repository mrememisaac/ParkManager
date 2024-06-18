using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Parks.Commands.UpdatePark;
using ParkManager.Application.Features.Parks.Queries.GetPark;
using ParkManager.Application.Features.Slots.Queries.GetSlot;
using ParkManager.Domain;

namespace ParkManager.UnitTests
{
    public class GetParkQueryHandlerTests
    {
        private readonly Mock<IParksRepository> _mockParksRepository;
        private readonly IRequestHandler<GetParkQuery, GetParkQueryResponse> _handler;
        private readonly Mock<IMapper> _mockMapper; 
        private readonly Mock<ILogger<GetParkQueryHandler>> _mockLogger;

        public GetParkQueryHandlerTests()
        {
            _mockParksRepository = new Mock<IParksRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<GetParkQueryHandler>>();
            _handler = new GetParkQueryHandler(_mockParksRepository.Object, _mockMapper.Object, _mockLogger.Object);
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
            var expectedResponse = new GetParkQueryResponse
            {
                Id = expectedPark.Id,
                City = expectedPark.City,
                Name = expectedPark.Name,
                Street = expectedPark.Street,
                State = expectedPark.State,
                Country = expectedPark.Country,
                Latitude = expectedPark.Latitude,
                Longitude = expectedPark.Longitude
            };
            _mockParksRepository.Setup(repo => repo.Get(parkId)).ReturnsAsync(expectedPark);
            _mockMapper.Setup(m => m.Map<GetParkQueryResponse>(expectedPark)).Returns(expectedResponse);
            var query = new GetParkQuery(parkId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedPark.Id, result.Id);
        }
    }
}
