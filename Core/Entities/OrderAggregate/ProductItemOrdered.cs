namespace Core.Entities.OrderAggregate
{  
    public class ProductItemOrdered
    {
        public ProductItemOrdered()
        {
        }

        public ProductItemOrdered(int productItemId, string productName, string pictureUrl, 
        string type, string variationIds)
        {
            ProductItemId = productItemId;
            ProductName = productName;
            PictureUrl = pictureUrl;
            Type = type;
            VariationIds = variationIds;
        }

        public int ProductItemId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        public string Type { get; set; }
        public string VariationIds { get; set; }
    }
}