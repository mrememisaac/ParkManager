using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkManager.Domain;

public class ParkConfiguration : EntityConfiguration<Park>, IEntityTypeConfiguration<Park>
{
    public void Configure(EntityTypeBuilder<Park> builder)
    {
        builder.ToTable("Parks");

        //builder.HasKey(p => p.Id);

        builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
        builder.Property(p => p.Street).IsRequired().HasMaxLength(50);
        builder.Property(p => p.City).IsRequired().HasMaxLength(50);
        builder.Property(p => p.State).IsRequired().HasMaxLength(50);
        builder.Property(p => p.Country).IsRequired().HasMaxLength(50);
        builder.Property(p => p.Latitude).IsRequired();
        builder.Property(p => p.Longitude).IsRequired();

        // Assuming ParkImage and Lane have a foreign key back to Park
        //builder.HasMany(p => p.Images)
        //       .WithOne() // If ParkImage has a navigation property back to Park, specify it here
        //       .HasForeignKey(pi => pi.ParkId)
        //       .OnDelete(DeleteBehavior.Cascade); // Adjust as necessary

        //builder.HasMany(p => p.Lanes)
        //       .WithOne() // If Lane has a navigation property back to Park, specify it here
        //       .HasForeignKey(l => l.ParkId)
        //       .OnDelete(DeleteBehavior.Cascade); // Adjust as necessary

        // If Images and Lanes are private fields, you might need to use reflection or specify the field name directly
        // For example, for Images:
        // builder.Metadata.FindNavigation(nameof(Park.Images)).SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
