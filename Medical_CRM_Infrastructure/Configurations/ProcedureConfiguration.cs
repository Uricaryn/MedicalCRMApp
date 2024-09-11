using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medical_CRM_Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Medical_CRM_Domain.Enums;

namespace Medical_CRM_Infrastructure.Configurations
{
    public class ProcedureConfiguration : IEntityTypeConfiguration<Procedure>
    {
        public void Configure(EntityTypeBuilder<Procedure> builder)
        {
            builder.HasKey(pr => pr.Id);

            builder.Property(pr => pr.ProcedureType)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => (ProcedureType)Enum.Parse(typeof(ProcedureType), v)
                );

            builder.Property(pr => pr.ProcedureDate)
                .IsRequired();

            builder.Property(pr => pr.Description)
                .HasMaxLength(500);

            builder.Property(pr => pr.Duration)
                .IsRequired();

            builder.Property(pr => pr.PostProcedureNotes)
                .HasMaxLength(500);

            builder.HasOne(pr => pr.Patient)
                .WithMany(p => p.Procedures)
                .HasForeignKey(pr => pr.PatientId);

            builder.HasOne(pr => pr.PerformedByOperator)
                .WithMany()
                .HasForeignKey(pr => pr.PerformedByOperatorId);

            builder.HasMany(pr => pr.ProcedureProducts)
                .WithOne(pp => pp.Procedure)
                .HasForeignKey(pp => pp.ProcedureId);

            builder.HasMany(pr => pr.Payments)
                .WithOne(p => p.Procedure)
                .HasForeignKey(p => p.ProcedureId);
        }
    }
}
