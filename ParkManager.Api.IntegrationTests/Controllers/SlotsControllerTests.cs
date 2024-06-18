using System.Net;
using ParkManager.Api.IntegrationTests.Base;
using ParkManager.Application.Features.Slots.Queries.GetSlot;
using ParkManager.Persistence.DataContexts;

namespace ParkManager.Api.IntegrationTests.Controllers
{
    public class SlotsControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
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

        public SlotsControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            factory.ClientOptions.BaseAddress = new Uri("http://localhost/api/slots/");
            _factory = factory;
            //_client = factory.CreateClient();
        }

        [Fact]
        public async Task ListSlots_ReturnsSuccessStatusCode()
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
        public async Task GetSlot_ReturnsNotFound_ForInvalidId()
        {

            var slotId = Guid.NewGuid();
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ParkManagerDbContext>();
                context.Database.EnsureCreated();
                Domain.Park park = new Domain.Park(Guid.NewGuid(), "Park 1", "Street", "City", "State", "Country", 1.1, 2.2);
                context.Parks.Add(park);
                await context.SaveChangesAsync();
                var lane = new Domain.Lane(Guid.NewGuid(), park.Id, "Lane 1");
                await context.SaveChangesAsync();
                var slot = new Domain.Slot(slotId, lane.Id, "Slot 1");
                
            }
            _client = _factory.CreateClient();
            var response = await _client.GetAsync($"{Guid.NewGuid()}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task AddSlot_ReturnsCreatedResponse()
        {
            Domain.Park park = new Domain.Park(Guid.NewGuid(), "Park 1", "Street", "City", "State", "Country", 1.1, 2.2);
            var lane = new Domain.Lane(Guid.NewGuid(), park.Id, "Lane 1");

            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ParkManagerDbContext>();
                context.Database.EnsureCreated();
                context.Parks.Add(park);
                context.Lanes.Add(lane);
                await context.SaveChangesAsync();
            }

            _client = _factory.CreateClient();

            var slot = new Domain.Slot(Guid.NewGuid(), lane.Id, "Slot 1");
            var response = await _client.PostAsJsonAsync("", slot);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var createdSlot = await response.Content.ReadFromJsonAsync<GetSlotQueryResponse>();
            Assert.NotNull(createdSlot);
            Assert.Equal(slot.Name, createdSlot.Name);
            Assert.Equal(slot.LaneId, createdSlot.LaneId);
            Assert.Equal(slot.Id, createdSlot.Id);
            // Additional assertions as necessary
        }

        [Fact]
        public async Task UpdateSlot_ReturnsNoContent()
        {
            Domain.Park park = new Domain.Park(Guid.NewGuid(), "Park 1", "Street", "City", "State", "Country", 1.1, 2.2);
            var lane = new Domain.Lane(Guid.NewGuid(), park.Id, "Lane 1");
            var slot = new Domain.Slot(Guid.NewGuid(), lane.Id, "Lane 1");

            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ParkManagerDbContext>();
                context.Database.EnsureCreated();
                context.Parks.Add(park);
                context.Lanes.Add(lane);
                slot = context.Slots.Add(slot).Entity;
                await context.SaveChangesAsync();
            }

            _client = _factory.CreateClient();


            var slotToUpdate = new Models.Slot(slot.Id, slot.LaneId, "Slot 2");
            var response = await _client.PutAsJsonAsync("", slotToUpdate);
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            // Additional validation to ensure the slot was updated as expected
        }

        [Fact]
        public async Task DeleteSlot_ReturnsNoContent()
        {
            Domain.Park park = new Domain.Park(Guid.NewGuid(), "Park 1", "Street", "City", "State", "Country", 1.1, 2.2);
            var lane = new Domain.Lane(Guid.NewGuid(), park.Id, "Lane 1");
            var slot = new Domain.Slot(Guid.NewGuid(), lane.Id, "Lane 1");

            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ParkManagerDbContext>();
                context.Database.EnsureCreated();
                context.Parks.Add(park);
                context.Lanes.Add(lane);
                slot = context.Slots.Add(slot).Entity;
                await context.SaveChangesAsync();
            }

            _client = _factory.CreateClient();

            var response = await _client.DeleteAsync($"{slot.Id}");
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            // Additional validation to ensure the slot was deleted as expected
        }

        // Additional tests for other scenarios, including failure cases and validating response content
    }
}
