using System.Collections.Generic;

namespace API.Dtos
{
    public class ProductToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public string ProductType { get; set; }
        public string ProductBrand { get; set; }
         public IReadOnlyList<ProductVariationsDto> ProductVariations { get; set; }
        public IReadOnlyList<ProductImageDto> ProductImages { get; set; }
        public IReadOnlyList<ProductAttributeDto> ProductAttribute { get; set; }
        public IReadOnlyList<ProductAttributeValuesDto> ProductAttributeValues { get; set; }
    }
}
