using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Description).IsRequired().HasMaxLength(180);
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
            builder.Property(p => p.RegularPrice).IsRequired().HasDefaultValue(0).HasColumnType("decimal(18,2)");
            builder.Property(p => p.PictureUrl).IsRequired();
            builder.Property(p => p.HasVariation).IsRequired().HasDefaultValue(false);
            builder.Property(p => p.ProductSKU).IsRequired(false).HasMaxLength(20);

            builder.Property(p => p.IsManageStock).IsRequired().HasDefaultValue(false);
            builder.Property(p => p.IsDownloadable).IsRequired().HasDefaultValue(false);
            builder.Property(p => p.ProductDownloadUrl).IsRequired(false);
            builder.Property(p => p.ProductDownloadUrlExpireOn);
            builder.Property(p => p.ProductDownloadLimit);
            builder.Property(p => p.IsVirtual).IsRequired().HasDefaultValue(false);
            builder.Property(p => p.StockQuantity).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(p => p.ProductWeight).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(p => p.ProductLength).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(p => p.ProductWidth).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(p => p.ProductHeight).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(p => p.HasVariation).IsRequired().HasDefaultValue(false);

            builder.HasOne(b => b.ProductBrand).WithMany()
            .HasForeignKey(p => p.ProductBrandId);

            builder.HasOne(t => t.ProductType).WithMany()
            .HasForeignKey(p => p.ProductTypeId);

            builder.HasMany(b => b.ProductImages).WithOne().HasForeignKey(p => p.Id)
            .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.ProductImages).WithOne(e => e.Product);
            builder.HasMany(c => c.ProductAttribute).WithOne(e => e.Product);
            builder.HasMany(c => c.ProductAttributeValues).WithOne(e => e.Product);

            // builder.HasMany(b => b.ProductAttribute).WithOne().HasForeignKey(p => p.Id)
            // .OnDelete(DeleteBehavior.Cascade);

            // builder.HasMany(b => b.ProductAttributeValues).WithOne().HasForeignKey(p => p.Id)
            // .OnDelete(DeleteBehavior.Cascade);

        }


    }
}