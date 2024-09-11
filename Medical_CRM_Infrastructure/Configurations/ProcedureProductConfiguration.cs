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
    public class ProcedureProductConfiguration : IEntityTypeConfiguration<ProcedureProduct>
    {
        public void Configure(EntityTypeBuilder<ProcedureProduct> builder)
        {
            // Setting up primary key
            builder.HasKey(pp => pp.ProcedureProductId);

            // Configuring ProcedureProductId as ValueGeneratedOnAdd without NEWSEQUENTIALID()
            builder.Property(pp => pp.ProcedureProductId)
                .IsRequired()
                .ValueGeneratedOnAdd(); // Indicates the value is generated on add but handled by EF/application layer

            builder.Property(pp => pp.QuantityUsed)
                .IsRequired();

            // Configuring relationships
            builder.HasOne(pp => pp.Procedure)
                .WithMany(pr => pr.ProcedureProducts)
                .HasForeignKey(pp => pp.ProcedureId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(pp => pp.Product)
                .WithMany()
                .HasForeignKey(pp => pp.ProductId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
