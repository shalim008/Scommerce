namespace Core.Entities
{
    public class ProductAttribute: BaseEntity
    {
        public string AttributeName { get; set; }
        public bool IsVisibleOnProductPage { get; set; }
        public bool IsUsedForVariations { get; set; }        
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}