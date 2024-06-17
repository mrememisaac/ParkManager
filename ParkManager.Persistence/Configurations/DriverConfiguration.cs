using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using ParkManager.Domain;

namespace ParkManager.Persistence.Configurations
{
    public class DriverConfiguration : EntityConfiguration<Driver>, IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
        {
            builder.ToTable("Drivers");
            //builder.HasKey(b => b.Id);
            builder.Property(b => b.Name).IsRequired().HasMaxLength(50);
            builder.Property(b => b.PhoneNumber).IsRequired().HasMaxLength(11);
            builder.Property(b => b.CreatedBy).IsRequired().HasMaxLength(50);
            builder.HasMany(b => b.DriverDetails).WithOne().HasForeignKey(p => p.DriverId);
        }
    }
}