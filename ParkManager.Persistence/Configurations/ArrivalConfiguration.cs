using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkManager.Domain;

namespace ParkManager.Persistence.Configurations
{
    public class ArrivalConfiguration : ArrivalDepartureBaseConfiguration<Arrival>
    {
        public void Configure(EntityTypeBuilder<Arrival> builder)
        {
            builder.ToTable("Arrivals");

            // Assuming Id is a property inherited from a base class
            //builder.HasKey(a => a.Id);


            // Configure the one-to-many relationship between Arrival and ArrivalImage
            builder.HasMany(a => a.Images)
                   .WithOne() // Assuming ArrivalImage does not have a navigation property back to Arrival
                   .HasForeignKey(ai => ai.ArrivalId) // Assuming ArrivalImage has a foreign key property named ArrivalId
                   .OnDelete(DeleteBehavior.Cascade); // Configure cascade delete as per your domain requirements

            // Additional configurations as needed
        }
    }
}