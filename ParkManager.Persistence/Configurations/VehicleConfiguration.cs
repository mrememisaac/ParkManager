using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkManager.Domain;

public class VehicleConfiguration : EntityConfiguration<Vehicle>, IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.ToTable("Vehicles");

        //builder.HasKey(v => v.Id);

        builder.Property(v => v.Make).IsRequired().HasMaxLength(255); // Assuming a max length for the make
        builder.Property(v => v.Model).IsRequired().HasMaxLength(255); // Assuming a max length for the model
        builder.Property(v => v.Registration).IsRequired().HasMaxLength(255); // Assuming a max length for the registration

        // Configure the private field _images to be a one-to-many relationship
        //builder.HasMany(typeof(VehicleImage), "_images")
        //       .WithOne() // Assuming VehicleImage does not have a navigation property back to Vehicle
        //       // If VehicleImage has a VehicleId property for the foreign key, specify it here
        //       .HasForeignKey("VehicleId") // Assuming VehicleImage entity has a VehicleId foreign key
        //       .OnDelete(DeleteBehavior.Cascade); // Configure cascade delete as per your domain requirements

        // Since _images is a private field, you need to use the string name of the field for EF mapping
        // This is an alternative way if _images had a public setter or if you're configuring a field
        // builder.Metadata.FindNavigation(nameof(Vehicle._images)).SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
