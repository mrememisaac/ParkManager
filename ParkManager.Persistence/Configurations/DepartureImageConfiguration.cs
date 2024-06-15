using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkManager.Domain;

namespace ParkManager.Persistence.Configurations
{
    public class DepartureImageConfiguration : IEntityTypeConfiguration<DepartureImage>
    {
        public void Configure(EntityTypeBuilder<DepartureImage> builder)
        {
            builder.ToTable("DepartureImages");

            builder.HasKey(ai => ai.Id);

            builder.Property(ai => ai.DepartureId)
                .IsRequired();

            builder.Property(ai => ai.Path)
                .IsRequired()
                .HasMaxLength(500); // Assuming a maximum path length of 500 characters

            // If Departure has a navigation property to DepartureImages, you might also need to configure the relationship
            // For example, if Departure entity has a collection of DepartureImages:
            // builder.HasOne<Departure>().WithMany(a => a.DepartureImages).HasForeignKey(ai => ai.DepartureId);
        }
    }
}