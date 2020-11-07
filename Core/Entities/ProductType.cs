namespace Core.Entities
{
    public class ProductType : BaseEntity
    {
        public string Name { get; set; }
        public int ParentProductId { get; set; } = 0;
        public int? VendorRegistrationId { get; set; }
        public string BrandsJson { get; set; }
    }
}