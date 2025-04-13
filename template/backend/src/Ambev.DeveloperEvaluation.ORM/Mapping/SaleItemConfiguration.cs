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

            builder.HasKey(i => i.Id);
            builder.Property(u => u.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");
            builder.Property(i => i.ProductName).IsRequired().HasMaxLength(150);
            builder.Property(i => i.UnitPrice).IsRequired();
            builder.Property(i => i.Quantity).IsRequired();
            builder.Property(i => i.Discount).IsRequired();
            builder.Ignore(i => i.TotalAmount);
        }
    }
}
