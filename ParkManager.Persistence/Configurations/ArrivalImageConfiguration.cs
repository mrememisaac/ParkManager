using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkManager.Domain;

namespace ParkManager.Persistence.Configurations
{

    public class ArrivalImageConfiguration : EntityConfiguration<ArrivalImage>, IEntityTypeConfiguration<ArrivalImage>
    {
        public void Configure(EntityTypeBuilder<ArrivalImage> builder)
        {
            builder.ToTable("ArrivalImages");

            //builder.HasKey(ai => ai.Id);

            builder.Property(ai => ai.ArrivalId)
                .IsRequired();

            builder.Property(ai => ai.Path)
                .IsRequired()
                .HasMaxLength(500); // Assuming a maximum path length of 500 characters

            // If Arrival has a navigation property to ArrivalImages, you might also need to configure the relationship
            // For example, if Arrival entity has a collection of ArrivalImages:
            // builder.HasOne<Arrival>().WithMany(a => a.ArrivalImages).HasForeignKey(ai => ai.ArrivalId);
        }
    }
}