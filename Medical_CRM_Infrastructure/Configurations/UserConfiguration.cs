using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medical_CRM_Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Medical_CRM_Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Configure FullName property
            builder.Property(u => u.FullName)
                .IsRequired()
                .HasMaxLength(100);

            // Configure LastLoginDate property with a default value
            builder.Property(u => u.LastLoginDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()"); // Default value is the current date/time when the record is created

            // Configure IsActive property with a default value
            builder.Property(u => u.IsActive)
                .IsRequired()
                .HasDefaultValue(true); // Default value is true, meaning the user is active by default

        }
    }
}
