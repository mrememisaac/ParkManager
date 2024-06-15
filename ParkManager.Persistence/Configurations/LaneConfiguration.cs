using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkManager.Domain;

public class LaneConfiguration : IEntityTypeConfiguration<Lane>
{
    public void Configure(EntityTypeBuilder<Lane> builder)
    {
        builder.ToTable("Lanes");

        builder.HasKey(l => l.Id);

        builder.Property(l => l.ParkId).IsRequired();
        builder.Property(l => l.Name).IsRequired().HasMaxLength(50); // Assuming a max length for the name

        //Configure the relationship between Lane and Park
        builder.HasOne(l => l.Park)
               .WithMany(p => p.Lanes) // Assuming Park entity has a collection of Lanes
               .HasForeignKey(l => l.ParkId);

        // Configure the collection of Slots as a relationship
        builder.HasMany(l => l.Slots)
               .WithOne() // Assuming Slot does not have a navigation property back to Lane
               // If Slot has a LaneId property for the foreign key, specify it here
               .HasForeignKey(s => s.LaneId) // Assuming Slot entity has a LaneId foreign key
               .OnDelete(DeleteBehavior.Cascade); // Configure cascade delete as per your domain requirements

        // Since Slots is a private field, you need to use the string name of the field for EF mapping
        // This is an alternative way if Slots had a public setter or if you're configuring a field
        // builder.Metadata.FindNavigation(nameof(Lane.Slots)).SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
