using AutoMapper;
using MediatR;
using Moq;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Drivers.Queries.GetDriver;
using ParkManager.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ParkManager.UnitTests
{
    public class GetDriverQueryHandlerTests
    {
        private readonly Mock<IDriversRepository> _mockDriversRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly IRequestHandler<GetDriverQuery, GetDriverQueryResponse> _handler;

        public GetDriverQueryHandlerTests()
        {
            _mockDriversRepository = new Mock<IDriversRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new GetDriverQueryHandler(_mockDriversRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnMappedDriver_WhenDriverExists()
        {
            // Arrange
            var driverId = Guid.NewGuid();
            var driver = new Driver(driverId, "John Doe", "08012345678" );
            var response = new GetDriverQueryResponse { Id = driverId, Name = "John Doe", PhoneNumber = "08012345678" };
            _mockDriversRepository.Setup(repo => repo.Get(driverId)).ReturnsAsync(driver);
            _mockMapper.Setup(mapper => mapper.Map<GetDriverQueryResponse>(driver)).Returns(response);
            var query = new GetDriverQuery(driverId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(response.Id, result.Id);
            Assert.Equal(response.Name, result.Name);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenDriverDoesNotExist()
        {
            // Arrange
            var driverId = Guid.NewGuid();
            _mockDriversRepository.Setup(repo => repo.Get(driverId)).ReturnsAsync((Driver)null);
            var query = new GetDriverQuery(driverId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}
