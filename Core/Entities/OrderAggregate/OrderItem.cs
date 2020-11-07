namespace Core.Entities.OrderAggregate
{
    public class OrderItem : BaseEntity
    {
        public OrderItem()
        {
        }
        public OrderItem(ProductItemOrdered itemOrdered, decimal price, int quantity, string type)
        {
            ItemOrdered = itemOrdered;
            Price = price;
            Quantity = quantity;
            Type = type;
        }
        public ProductItemOrdered ItemOrdered { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Type { get; set; }
         public string VariationIds { get; set; }
    }
} 