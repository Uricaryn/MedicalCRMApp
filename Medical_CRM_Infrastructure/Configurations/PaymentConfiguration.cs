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
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            // Primary Key Configuration
            builder.HasKey(p => p.Id);

            // Property Configurations
            builder.Property(p => p.Amount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.PaymentDate)
                .IsRequired();

            builder.Property(p => p.PaymentMethod)
                .HasMaxLength(50);

            // Relationships
            builder.HasOne(p => p.Patient) // Configure Payment -> Patient relationship
                .WithMany(pat => pat.Payments) // A Patient can have many Payments
                .HasForeignKey(p => p.PatientId) // Foreign key in Payment table
                .OnDelete(DeleteBehavior.Cascade); // Configure delete behavior

            builder.HasOne(p => p.Procedure) // Configure Payment -> Procedure relationship
                .WithMany(proc => proc.Payments) // A Procedure can have many Payments (adjust this if it's WithOne)
                .HasForeignKey(p => p.ProcedureId)
                .OnDelete(DeleteBehavior.Restrict); // Configure delete behavior to restrict or cascade based on your needs
        }
    }
}
