using System.Net;
using ParkManager.Api.IntegrationTests.Base;
using ParkManager.Application.Features.Arrivals.Queries.GetArrival;
using ParkManager.Persistence.DataContexts;

namespace ParkManager.Api.IntegrationTests.Controllers
{
    public class ArrivalsControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;
        private HttpClient _client;

        public async Task InitializeAsync()
        {
            var sp = _factory.Services.CreateScope().ServiceProvider;
            _factory.SetupDatabase(sp);
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }

        public ArrivalsControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            factory.ClientOptions.BaseAddress = new Uri("http://localhost/api/arrivals/");
            _factory = factory;
            //_client = factory.CreateClient();
        }

        [Fact]
        public async Task ListArrivals_ReturnsSuccessStatusCode()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ParkManagerDbContext>();
                context.Database.EnsureCreated();
            }

            _client = _factory.CreateClient();
            var response = await _client.GetAsync("");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetArrival_ReturnsNotFound_ForInvalidId()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ParkManagerDbContext>();
                context.Database.EnsureCreated();
            }
            _client = _factory.CreateClient();
            var response = await _client.GetAsync($"/{Guid.NewGuid()}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task AddArrival_ReturnsCreatedResponse()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ParkManagerDbContext>();
                context.Database.EnsureCreated();
            }
            _client = _factory.CreateClient();
            var arrival = new Models.Arrival(Guid.NewGuid(), DateTime.Now, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            var response = await _client.PostAsJsonAsync("", arrival);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var createdArrival = await response.Content.ReadFromJsonAsync<GetArrivalQueryResponse>();
            Assert.NotNull(createdArrival);
            Assert.Equal(arrival.Id, createdArrival.Id);
            Assert.Equal(arrival.Timestamp, createdArrival.Timestamp);
            Assert.Equal(arrival.VehicleId, createdArrival.VehicleId);
            Assert.Equal(arrival.ParkId, createdArrival.ParkId);
            Assert.Equal(arrival.DriverId, createdArrival.DriverId);
            Assert.Equal(arrival.TagId, createdArrival.TagId);
        }

        [Fact]
        public async Task UpdateArrival_ReturnsNoContent()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ParkManagerDbContext>();
                context.Database.EnsureCreated();
            }
            _client = _factory.CreateClient();

            var arrival = new Models.Arrival(Guid.NewGuid(), DateTime.Now, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            var response = await _client.PostAsJsonAsync("", arrival);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            // Assuming you have a arrival to update. This might require creating a arrival first.
            var arrivalToUpdate = new Models.Arrival(arrival.Id, DateTime.Now, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            response = await _client.PutAsJsonAsync("", arrivalToUpdate);
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            // Additional validation to ensure the arrival was updated as expected
        }

        [Fact]
        public async Task DeleteArrival_ReturnsNoContent()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ParkManagerDbContext>();
                context.Database.EnsureCreated();
            }
            _client = _factory.CreateClient();
            var arrival = new Models.Arrival(Guid.NewGuid(), DateTime.Now, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            var response = await _client.PostAsJsonAsync("", arrival);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            response = await _client.DeleteAsync($"{arrival.Id}");
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            // Additional validation to ensure the arrival was deleted as expected
        }

        // Additional tests for other scenarios, including failure cases and validating response content
    }
}
