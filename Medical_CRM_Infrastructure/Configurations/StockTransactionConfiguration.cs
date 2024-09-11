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
    public class StockTransactionConfiguration : IEntityTypeConfiguration<StockTransaction>
    {
        public void Configure(EntityTypeBuilder<StockTransaction> builder)
        {
            builder.HasKey(st => st.Id);

            builder.Property(st => st.Quantity)
                .IsRequired();

            builder.Property(st => st.TransactionDate)
                .IsRequired();

            builder.Property(st => st.TransactionType)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(st => st.Supplier)
                .HasMaxLength(100);

            builder.Property(st => st.DocumentNumber)
                .HasMaxLength(100);

            builder.HasOne(st => st.Product)
                .WithMany(p => p.StockTransactions)
                .HasForeignKey(st => st.ProductId);
        }
    }
}
