using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace API.Dtos
{
    public class SetProduct{
         public ProductDetailsForm ProductDetailsForm { get; set; }
    }
    public class ProductDetailsForm
    {
        //public int? Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; } = 0;
        public decimal RegularPrice { get; set; } = 0;
        public bool IsManageStock { get; set; } = true;
        public bool IsDownloadable { get; set; } = false;
        public string ProductDownloadUrl { get; set; }
        public DateTimeOffset? ProductDownloadUrlExpireOn { get; set; }
        public int ProductDownloadLimit { get; set; } = 0;
        public bool IsVirtual { get; set; } = false;
        public int StockQuantity { get; set; } = 0;
        public int LowStockThreshold { get; set; } = 0;
        public decimal ProductWeight { get; set; } = 0;
        public decimal ProductLength { get; set; }= 0;
        public decimal ProductWidth { get; set; }= 0;
        public decimal ProductHeight { get; set; }= 0;
        public string PictureUrl { get; set; }
        public int Types { get; set; }
        public int Brands { get; set; }
        public bool HasVariation { get; set; } = false;
        public string ProductSKU { get; set; }
        public string DeliveryFrom { get; set; }
        public string DeliveryTime { get; set; }
        public int ProductRating { get; set; } = 5;

        public IFormFile  PictureFile{ get; set; }      
        public List<IFormFile>  GallaryImages{ get; set; }

        // public ICollection<ProductVariations> ProductVariations { get; set; }
        // public ICollection<ProductImage> ProdusctImages { get; set; }
        // public ICollection<ProductAttribute> ProductAttribute { get; set; }
        // public ICollection<ProductAttributeValues> ProductAttributeValues { get; set; }
        
    }
}