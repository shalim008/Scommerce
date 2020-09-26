namespace API.Dtos
{
    public class ProductImageDto
    {
         public string NameAlt { get; set; }
        public string ImageUrl { get; set; }
        public string ImageThumbUrl { get; set; }
        public string ImageBigUrl { get; set; }
        public bool IsRelativeUrl { get; set; }
        public int ProductId { get; set; }
    }
}