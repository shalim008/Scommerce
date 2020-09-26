using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
        public class ProductVariationsConfiguration : IEntityTypeConfiguration<ProductVariations>
    {
        public void Configure(EntityTypeBuilder<ProductVariations> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Name).IsRequired(false).HasMaxLength(100);
            builder.Property(p => p.Description).IsRequired(false).HasMaxLength(180);
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
            builder.Property(p => p.RegularPrice).IsRequired().HasDefaultValue(0).HasColumnType("decimal(18,2)");
            builder.Property(p => p.PictureUrl).IsRequired();           
            builder.Property(p => p.ProductSKU).IsRequired(false).HasMaxLength(20);
            builder.Property(p => p.StockQuantity).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(p => p.ProductWeight).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(p => p.ProductLength).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(p => p.ProductWidth).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(p => p.ProductHeight).IsRequired().HasColumnType("decimal(18,2)");
            
            builder.HasOne(b => b.ProductAttribute).WithMany()
            .HasForeignKey(p => p.ProductAttributeId);
            
            builder.HasOne(b => b.ProductAttributeValues).WithMany()
            .HasForeignKey(p => p.ProductAttributeId);

        }


    }
}