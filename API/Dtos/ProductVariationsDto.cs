using System;

namespace API.Dtos
{
    public class ProductVariationsDto
    {         
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal RegularPrice { get; set; }
        public decimal StockQuantity { get; set; }
        public decimal ProductWeight { get; set; }
        public decimal ProductLength { get; set; }
        public decimal ProductWidth { get; set; }
        public decimal ProductHeight { get; set; }
        public string PictureUrl { get; set; }
        public string ProductSKU { get; set; }

        public int ProductId { get; set; }
        public int ProductAttributeId { get; set; }
        public int ProductAttributeValueId { get; set; }

    }
}