using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkManager.Domain;

public class ArrivalDepartureBaseConfiguration<T> : EntityConfiguration<T>, IEntityTypeConfiguration<T> where T : ArrivalDepartureBase
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        // Configure the common properties
        builder.Property(ad => ad.Timestamp)
               .IsRequired();

        builder.Property(ad => ad.ParkId)
               .IsRequired();

        builder.Property(ad => ad.VehicleId)
               .IsRequired();

        builder.Property(ad => ad.DriverId)
               .IsRequired();

        builder.Property(ad => ad.TagId)
               .IsRequired();

        // Configure relationships if there are navigation properties in ArrivalDepartureBase
        // For example, assuming there's a navigation property for Park:
        // builder.HasOne<Park>().WithMany().HasForeignKey(ad => ad.ParkId);
    }
}

