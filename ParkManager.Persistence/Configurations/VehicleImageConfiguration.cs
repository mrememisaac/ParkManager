using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkManager.Domain;

namespace ParkManager.Persistence.Configurations
{
    public class VehicleImageConfiguration : IEntityTypeConfiguration<VehicleImage>
    {
        public void Configure(EntityTypeBuilder<VehicleImage> builder)
        {
            builder.ToTable("VehicleImages");

            builder.HasKey(ai => ai.Id);

            builder.Property(ai => ai.VehicleId)
                .IsRequired();

            builder.Property(ai => ai.Path)
                .IsRequired()
                .HasMaxLength(500); // Assuming a maximum path length of 500 characters

            // If Vehicle has a navigation property to VehicleImages, you might also need to configure the relationship
            // For example, if Vehicle entity has a collection of VehicleImages:
            // builder.HasOne<Vehicle>().WithMany(a => a.VehicleImages).HasForeignKey(ai => ai.VehicleId);
        }
    }
}