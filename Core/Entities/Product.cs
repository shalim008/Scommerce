using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal RegularPrice { get; set; }
        public bool IsManageStock { get; set; }
        public bool IsDownloadable { get; set; }
        public string ProductDownloadUrl { get; set; }
        public DateTimeOffset ProductDownloadUrlExpireOn { get; set; }
        public int ProductDownloadLimit { get; set; }
        public bool IsVirtual { get; set; }
        public decimal StockQuantity { get; set; }
        public decimal ProductWeight { get; set; }
        public decimal ProductLength { get; set; }
        public decimal ProductWidth { get; set; }
        public decimal ProductHeight { get; set; }
        public string PictureUrl { get; set; }
        public ProductType ProductType { get; set; }
        public int ProductTypeId { get; set; }
        public ProductBrand ProductBrand { get; set; }
        public int ProductBrandId { get; set; }        
        public bool HasVariation { get; set; }
        public string ProductSKU { get; set; }

        public ICollection<ProductVariations> ProductVariations { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
        public ICollection<ProductAttribute> ProductAttribute { get; set; }
        public ICollection<ProductAttributeValues> ProductAttributeValues { get; set; }

    }
}