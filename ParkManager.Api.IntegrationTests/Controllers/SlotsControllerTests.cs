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
        public async Task AddSlot_ReturnsCreatedResponse()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ParkManagerDbContext>();
                context.Database.EnsureCreated();
            }
            _client = _factory.CreateClient();
            var slot = new Models.Slot(Guid.NewGuid(), Guid.NewGuid(), "Lane 1");
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
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ParkManagerDbContext>();
                context.Database.EnsureCreated();
            }
            _client = _factory.CreateClient();

            var slot = new Models.Slot(Guid.NewGuid(), Guid.NewGuid(), "Slot 1");
            var response = await _client.PostAsJsonAsync("", slot);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            // Assuming you have a slot to update. This might require creating a slot first.
            var slotToUpdate = new Models.Slot(slot.Id, slot.LaneId, "Slot 2");
            response = await _client.PutAsJsonAsync("", slotToUpdate);
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            // Additional validation to ensure the slot was updated as expected
        }

        [Fact]
        public async Task DeleteSlot_ReturnsNoContent()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ParkManagerDbContext>();
                context.Database.EnsureCreated();
            }
            _client = _factory.CreateClient();
            var slot = new Models.Slot(Guid.NewGuid(), Guid.NewGuid(), "Slot 1");
            var response = await _client.PostAsJsonAsync("", slot);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            response = await _client.DeleteAsync($"{slot.Id}");
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            // Additional validation to ensure the slot was deleted as expected
        }

        // Additional tests for other scenarios, including failure cases and validating response content
    }
}
