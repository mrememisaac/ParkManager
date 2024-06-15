using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Persistence.DataContexts;
using ParkManager.Persistence.Repositories;

namespace ParkManager.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ParkManagerDbContext>(options =>
            {
                //options.UseSqlServer(configuration.GetConnectionString("ParkManagerDb"));
                options.UseSqlite(configuration.GetConnectionString("ParkManagerDbSqllite"));
            });
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(IArrivalsRepository), typeof(ArrivalsRepository));
            services.AddScoped(typeof(IDeparturesRepository), typeof(DeparturesRepository));
            services.AddScoped(typeof(IParksRepository), typeof(ParksRepository));
            services.AddScoped(typeof(ITagsRepository), typeof(TagsRepository));
            services.AddScoped(typeof(IVehiclesRepository), typeof(VehiclesRepository));
            services.AddScoped(typeof(IDriversRepository), typeof(DriversRepository));
            services.AddScoped(typeof(IOccasionsRepository), typeof(OccasionsRepository));
            services.AddScoped(typeof(ISlotsRepository), typeof(SlotsRepository));
            services.AddScoped(typeof(ILanesRepository), typeof(LanesRepository));
            return services;
        }
    }
}
