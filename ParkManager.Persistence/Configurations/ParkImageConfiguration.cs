using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkManager.Domain;

namespace ParkManager.Persistence.Configurations
{
    public class ParkImageConfiguration : IEntityTypeConfiguration<ParkImage>
    {
        public void Configure(EntityTypeBuilder<ParkImage> builder)
        {
            builder.ToTable("ParkImages");

            builder.HasKey(ai => ai.Id);

            builder.Property(ai => ai.ParkId)
                .IsRequired();

            builder.Property(ai => ai.Path)
                .IsRequired()
                .HasMaxLength(500); // Assuming a maximum path length of 500 characters

            //Configure the relationship between ParkImage and Park
            builder.HasOne(ai => ai.Park)
                   .WithMany(p => p.Images) // Assuming Park entity has a collection of Lanes
                   .HasForeignKey(ai => ai.ParkId);
        }
    }
}