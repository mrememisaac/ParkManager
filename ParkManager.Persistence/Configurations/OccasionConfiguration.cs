using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkManager.Domain;

public class OccasionConfiguration : EntityConfiguration<Occasion>, IEntityTypeConfiguration<Occasion>
{
    public void Configure(EntityTypeBuilder<Occasion> builder)
    {
        builder.ToTable("Occasions");

        //builder.HasKey(o => o.Id);

        builder.Property(o => o.Name)
               .IsRequired()
               .HasMaxLength(50); // Assuming a max length for the name

        builder.Property(o => o.StartDate)
               .IsRequired();

        builder.Property(o => o.EndDate)
               .IsRequired();

        // Ensure StartDate is before EndDate with a model-level check constraint (if supported by your DBMS)
        builder.HasCheckConstraint("CK_Occasions_StartDateBeforeEndDate", "[StartDate] < [EndDate]");

        // If there are any relationships to configure, do so here
        // For example, if Occasion is related to another entity:
        // builder.HasOne(o => o.OtherEntity)
        //        .WithMany(oe => oe.Occasions)
        //        .HasForeignKey(o => o.OtherEntityId);
    }
}
