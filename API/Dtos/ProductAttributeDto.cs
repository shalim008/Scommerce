namespace API.Dtos
{
    public class ProductAttributeDto
    {
        public int Id { get; set; }
        public string AttributeName { get; set; }
        public bool IsVisibleOnProductPage { get; set; }
        public bool IsUsedForVariations { get; set; }        

    }
}