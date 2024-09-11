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
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            // Configure primary key
            builder.HasKey(r => r.RoleId);

            // Configure properties
            builder.Property(r => r.RoleId)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(50);

            // Seed roles
            var superAdminRole = new Role
            {
                RoleId = Guid.NewGuid(),
                Name = "SuperAdmin"
            };

            builder.HasData(superAdminRole);
        }
    }
}
