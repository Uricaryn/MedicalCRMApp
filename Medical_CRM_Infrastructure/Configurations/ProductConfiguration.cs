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
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Description)
                .HasMaxLength(500);

            builder.Property(p => p.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.QuantityInStock)
                .IsRequired();

            builder.Property(p => p.ExpiryDate)
                .IsRequired(true);

            builder.Property(p => p.Supplier)
                .HasMaxLength(100);

            builder.HasMany(p => p.StockTransactions)
                .WithOne(st => st.Product)
                .HasForeignKey(st => st.ProductId);
        }
    }
}
