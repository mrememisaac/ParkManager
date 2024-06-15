using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkManager.Domain;

public class SlotConfiguration : IEntityTypeConfiguration<Slot>
{
    public void Configure(EntityTypeBuilder<Slot> builder)
    {
        builder.ToTable("Slots");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.LaneId).IsRequired();
        builder.Property(s => s.Name).IsRequired().HasMaxLength(50); // Assuming a max length for the name

        // Configure the relationship between Slot and Lane
        builder.HasOne(s => s.Lane)
               .WithMany(l => l.Slots) // Assuming Lane entity has a collection of Slots
               .HasForeignKey(s => s.LaneId)
               .OnDelete(DeleteBehavior.Cascade); // Adjust as necessary

        // If Slot has additional properties or relationships, configure them here
    }
}
