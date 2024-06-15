using ParkManager.Domain;
using ParkManager.Persistence.DataContexts;

namespace ParkManager.Api.IntegrationTests.Base
{
    public class Utilities
    {
        public static void InitializeTestDb(ParkManagerDbContext context)
        {
            var vehicle1 = context.Vehicles.Add(new Vehicle(Guid.NewGuid(), "Honda", "CRV", "ABC1234")).Entity;
            vehicle1.CreatedDate = DateTime.Now;
            vehicle1.CreatedBy = "seeder";

            var vehicle2 = context.Vehicles.Add(new Vehicle(Guid.NewGuid(), "Toyota", "Matrix", "XYZ1234")).Entity;
            vehicle2.CreatedDate = DateTime.Now;
            vehicle2.CreatedBy = "seeder";

            context.Vehicles.Add(vehicle1);
            context.Vehicles.Add(vehicle2);

            var result = context.SaveChanges();
        }
    }
}
