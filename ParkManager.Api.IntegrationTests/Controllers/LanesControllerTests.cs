using System.Net;
using ParkManager.Api.IntegrationTests.Base;
using ParkManager.Application.Features.Lanes.Queries.GetLane;
using ParkManager.Persistence.DataContexts;

namespace ParkManager.Api.IntegrationTests.Controllers
{
    public class LanesControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
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

        public LanesControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            factory.ClientOptions.BaseAddress = new Uri("http://localhost/api/lanes/");
            _factory = factory;
            //_client = factory.CreateClient();
        }

        [Fact]
        public async Task ListLanes_ReturnsSuccessStatusCode()
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
        public async Task GetLane_ReturnsNotFound_ForInvalidId()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ParkManagerDbContext>();
                context.Database.EnsureCreated();
            }
            _client = _factory.CreateClient();
            var response = await _client.GetAsync($"{Guid.NewGuid()}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task AddLane_ReturnsCreatedResponse()
        {
            var parkId = Guid.NewGuid();
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ParkManagerDbContext>();
                context.Database.EnsureCreated();
                Domain.Park park = new Domain.Park(parkId, "Park 1", "Street", "City", "State", "Country", 1.1, 2.2);
                context.Parks.Add(park);
                await context.SaveChangesAsync();
            }

            _client = _factory.CreateClient();
            var lane = new Models.Lane(Guid.NewGuid(), parkId, "Lane 1");
            var response = await _client.PostAsJsonAsync("", lane);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var createdLane = await response.Content.ReadFromJsonAsync<GetLaneQueryResponse>();
            Assert.NotNull(createdLane);
            Assert.Equal(lane.Name, createdLane.Name);
            Assert.Equal(lane.ParkId, createdLane.ParkId);
            Assert.Equal(lane.Id, createdLane.Id);
            // Additional assertions as necessary
        }

        [Fact]
        public async Task UpdateLane_ReturnsNoContent()
        {
            var laneId = Guid.NewGuid();
            Domain.Park park = new Domain.Park(Guid.NewGuid(), "Park 1", "Street", "City", "State", "Country", 1.1, 2.2);
            var lane = new Domain.Lane(laneId, park.Id, "Lane 1");

            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ParkManagerDbContext>();
                context.Database.EnsureCreated();
                context.Parks.Add(park);
                lane = context.Lanes.Add(lane).Entity;
                await context.SaveChangesAsync();
            }
            _client = _factory.CreateClient();


            // Assuming you have a lane to update. This might require creating a lane first.
            var laneToUpdate = new Models.Lane(lane.Id, lane.ParkId, "Lane 2");
            var response = await _client.PutAsJsonAsync("", laneToUpdate);
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            // Additional validation to ensure the lane was updated as expected
        }

        [Fact]
        public async Task DeleteLane_ReturnsNoContent()
        {
            var laneId = Guid.NewGuid();
            Domain.Park park = new Domain.Park(Guid.NewGuid(), "Park 1", "Street", "City", "State", "Country", 1.1, 2.2);
            var lane = new Domain.Lane(laneId, park.Id, "Lane 1");

            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ParkManagerDbContext>();
                context.Database.EnsureCreated();
                context.Parks.Add(park);
                lane = context.Lanes.Add(lane).Entity;
                await context.SaveChangesAsync();
            }
            _client = _factory.CreateClient();

            var response = await _client.DeleteAsync($"{laneId}");
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            // Additional validation to ensure the lane was deleted as expected
        }

        // Additional tests for other scenarios, including failure cases and validating response content
    }
}
