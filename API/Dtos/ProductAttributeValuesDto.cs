namespace API.Dtos
{
    public class ProductAttributeValuesDto
    {
        public int Id {get; set;}
        public string AttributeValueName { get; set; }
        public int AttributeValueParentId { get; set; } = 0;
        public int ProductAttributeId { get; set; }
    }
}