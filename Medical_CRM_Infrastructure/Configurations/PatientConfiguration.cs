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
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Email)
                .HasMaxLength(100);

            builder.Property(p => p.PhoneNumber)
                .HasMaxLength(15);

            builder.Property(p => p.Address)
                .HasMaxLength(200);

            builder.Property(p => p.DateOfBirth)
                .IsRequired();

            builder.HasMany(p => p.Appointments)
                .WithOne(a => a.Patient)
                .HasForeignKey(a => a.PatientId);

            builder.HasMany(p => p.Procedures)
                .WithOne(pr => pr.Patient)
                .HasForeignKey(pr => pr.PatientId);

            builder.HasMany(p => p.Payments)
                .WithOne(pay => pay.Patient)
                .HasForeignKey(pay => pay.PatientId);
        }
    }
}
