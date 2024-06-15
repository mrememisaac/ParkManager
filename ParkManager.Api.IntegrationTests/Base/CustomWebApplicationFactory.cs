using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MiNET.Particles;
using ParkManager.Persistence.DataContexts;
using System.Data.Common;
using System.Reflection;

namespace ParkManager.Api.IntegrationTests.Base
{
    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {

            var assemblyLocation = Assembly.GetExecutingAssembly().Location;
            var assemblyDirectory = Path.GetDirectoryName(assemblyLocation);
            builder.ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile(Path.Combine(assemblyDirectory, "testsettings.json"));
            });
            builder.UseEnvironment("Development");

            builder.ConfigureServices(services =>
            {
                var dbContextDescriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(DbContextOptions<ParkManagerDbContext>));

                if (dbContextDescriptor is not null) services.Remove(dbContextDescriptor);

                var dbConnectionDescriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(DbConnection));

                if (dbConnectionDescriptor is not null)
                    services.Remove(dbConnectionDescriptor);

                // Create open SqliteConnection so EF won't automatically close it.
                services.AddSingleton<DbConnection>(container =>
                {
                    var connection = new SqliteConnection("DataSource=:memory:");
                    connection.Open();

                    return connection;
                });

                services.AddDbContext<ParkManagerDbContext>((container, options) =>
                {
                    var connection = container.GetRequiredService<DbConnection>();
                    options.UseSqlite(connection);
                    
                });
                var sp = services.BuildServiceProvider();
                SetupDatabase(sp);
            });
        }

        public void SetupDatabase(IServiceProvider sp)
        {
            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var context = scopedServices.GetRequiredService<ParkManagerDbContext>();
                var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TProgram>>>();
                context.Database.EnsureCreated();
                try
                {
                    //initialize db
                    Utilities.InitializeTestDb(context);
                }
                catch (Exception e)
                {
                    logger.LogError(e, $"An error occured while seeding the test db. Error: {e.Message}");
                }
            }
        }

        public HttpClient GetAnonymousClient()
        {
            return CreateClient();
        }
    }
}
