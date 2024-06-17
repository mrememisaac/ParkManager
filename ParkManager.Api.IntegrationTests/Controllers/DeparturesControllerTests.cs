using System.Net;
using ParkManager.Api.IntegrationTests.Base;
using ParkManager.Application.Features.Departures.Queries.GetDeparture;
using ParkManager.Persistence.DataContexts;

namespace ParkManager.Api.IntegrationTests.Controllers
{
    public class DeparturesControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
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

        public DeparturesControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            factory.ClientOptions.BaseAddress = new Uri("http://localhost/api/departures/");
            _factory = factory;
            //_client = factory.CreateClient();
        }

        [Fact]
        public async Task ListDepartures_ReturnsSuccessStatusCode()
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
        public async Task GetDeparture_ReturnsNotFound_ForInvalidId()
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
        public async Task AddDeparture_ReturnsCreatedResponse()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ParkManagerDbContext>();
                context.Database.EnsureCreated();
            }
            _client = _factory.CreateClient();
            var departure = new Models.Departure(Guid.NewGuid(), DateTime.Now, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            var response = await _client.PostAsJsonAsync("", departure);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var createdDeparture = await response.Content.ReadFromJsonAsync<GetDepartureQueryResponse>();
            Assert.NotNull(createdDeparture);
            Assert.Equal(departure.Id, createdDeparture.Id);
            Assert.Equal(departure.Timestamp, createdDeparture.Timestamp);
            Assert.Equal(departure.VehicleId, createdDeparture.VehicleId);
            Assert.Equal(departure.ParkId, createdDeparture.ParkId);
            Assert.Equal(departure.DriverId, createdDeparture.DriverId);
            Assert.Equal(departure.TagId, createdDeparture.TagId);
        }

        [Fact]
        public async Task UpdateDeparture_ReturnsNoContent()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ParkManagerDbContext>();
                context.Database.EnsureCreated();
            }
            _client = _factory.CreateClient();

            var departure = new Models.Departure(Guid.NewGuid(), DateTime.Now, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            var response = await _client.PostAsJsonAsync("", departure);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            // Assuming you have a departure to update. This might require creating a departure first.
            var departureToUpdate = new Models.Departure(departure.Id, DateTime.Now, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            response = await _client.PutAsJsonAsync("", departureToUpdate);
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            // Additional validation to ensure the departure was updated as expected
        }

        [Fact]
        public async Task DeleteDeparture_ReturnsNoContent()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ParkManagerDbContext>();
                context.Database.EnsureCreated();
            }
            _client = _factory.CreateClient();
            var departure = new Models.Departure(Guid.NewGuid(), DateTime.Now, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            var response = await _client.PostAsJsonAsync("", departure);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            response = await _client.DeleteAsync($"{departure.Id}");
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            // Additional validation to ensure the departure was deleted as expected
        }

        // Additional tests for other scenarios, including failure cases and validating response content
    }
}
