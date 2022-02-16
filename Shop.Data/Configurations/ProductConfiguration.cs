using Microsoft.EntityFrameworkCore;
using Shop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name).IsRequired(true).HasMaxLength(20);
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(x => x.ModifiedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
            builder.Property(x => x.SalePrice).IsRequired(true).HasColumnType("decimal(18,2)");
            builder.Property(x => x.CostPrice).IsRequired(true).HasColumnType("decimal(18,2)");
            builder.Property(x => x.Image).HasMaxLength(200).IsRequired(false);
        }
    }
}
