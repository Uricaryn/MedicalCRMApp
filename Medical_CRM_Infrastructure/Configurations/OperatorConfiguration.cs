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
    public class OperatorConfiguration : IEntityTypeConfiguration<Operator>
    {
        public void Configure(EntityTypeBuilder<Operator> builder)
        {
            // Primary key tanımlaması
            builder.HasKey(o => o.Id);

            // Property konfigürasyonları
            builder.Property(o => o.FullName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(o => o.OperatorCode)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(o => o.PhoneNumber)
                .HasMaxLength(20);

            builder.Property(o => o.Email)
                .HasMaxLength(100);

            // Operator - Procedure ilişkisi (bir operatör birden çok işlem yapabilir)
            builder.HasMany(o => o.Procedures)
                .WithOne(p => p.PerformedByOperator)
                .HasForeignKey(p => p.PerformedByOperatorId)
                .OnDelete(DeleteBehavior.Restrict); // Operatör silindiğinde işlemler silinmez
        }
    }
}
