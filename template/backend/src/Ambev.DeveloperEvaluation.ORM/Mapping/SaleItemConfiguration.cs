using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.ToTable("SaleItems");

            builder.HasKey(si => si.Id);
            builder.Property(si => si.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(si => si.ProductId).IsRequired();
            builder.Property(si => si.ProductName).IsRequired().HasMaxLength(100);
            builder.Property(si => si.Quantity).IsRequired();

            builder.Property(si => si.UnitPrice).IsRequired();
            builder.Property(si => si.Discount).IsRequired();
            builder.Property(si => si.TotalAmount).IsRequired();

            builder.Property(si => si.IsCancelled).IsRequired();
        }
    }
}
