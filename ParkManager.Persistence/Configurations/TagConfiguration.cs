using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkManager.Domain;

public class TagConfiguration : EntityConfiguration<Tag>, IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.ToTable("Tags");

        //builder.HasKey(t => t.Id);

        builder.Property(t => t.Number)
               .IsRequired();

        // Since the constructor and property setters are private, ensure EF Core can still bind them
        //builder.Property(t => t.Number).HasField("Number");
        //builder.Property(t => t.Id).HasField("Id");

        // If Tag has relationships with other entities, configure them here
        // For example, if Tag is related to a Vehicle entity:
        // builder.HasOne<Tag>().WithMany().HasForeignKey(t => t.VehicleId);
    }
}

