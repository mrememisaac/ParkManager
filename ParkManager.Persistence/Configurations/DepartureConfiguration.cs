using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkManager.Domain;

namespace ParkManager.Persistence.Configurations
{
    public class DepartureConfiguration : ArrivalDepartureBaseConfiguration<Departure>
    {
        public void Configure(EntityTypeBuilder<Departure> builder)
        {
            builder.ToTable("Departures");

            // Assuming Id is a property inherited from a base class
            builder.HasKey(a => a.Id);


            // Configure the one-to-many relationship between Departure and DepartureImage
            builder.HasMany(a => a.Images)
                   .WithOne() // Assuming DepartureImage does not have a navigation property back to Departure
                   .HasForeignKey(ai => ai.DepartureId) // Assuming DepartureImage has a foreign key property named DepartureId
                   .OnDelete(DeleteBehavior.Cascade); // Configure cascade delete as per your domain requirements

            // Additional configurations as needed
        }
    }
}