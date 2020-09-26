using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.NameAlt).IsRequired().HasMaxLength(100);
            builder.Property(p => p.ImageUrl).IsRequired().HasMaxLength(180);
            builder.Property(p => p.ImageThumbUrl).IsRequired().HasMaxLength(180);
            builder.Property(p => p.ImageBigUrl).IsRequired().HasMaxLength(180);
            builder.Property(p => p.IsRelativeUrl).IsRequired();
       
                   
        }
    }
}