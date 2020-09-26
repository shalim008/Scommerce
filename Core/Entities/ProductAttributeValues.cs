using System;

namespace Core.Entities
{
    public class ProductAttributeValues : BaseEntity
    {
        public string AttributeValueName { get; set; }
        public int AttributeValueParentId { get; set; } = 0;
        public int ProductAttributeId { get; set; }
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
        public string ProductSKU {get; set;}
        public ProductAttribute ProductAttribute { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}