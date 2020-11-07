using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public class Product : BaseEntity
    {




      public Product()
        {

        }


       public Product(string name, string description, decimal price, decimal regularPrice, 
        bool isManageStock, bool isDownloadable, string productDownloadUrl, 
        DateTimeOffset productDownloadUrlExpireOn, int productDownloadLimit, bool isVirtual, 
        int stockQuantity, int lowStockThreshold, decimal productWeight, 
        decimal productLength, decimal productWidth, decimal productHeight, string pictureUrl, 
        ProductType productType, 
        int productTypeId, ProductBrand productBrand, int productBrandId, bool hasVariation, string productSKU, 
        string deliveryFrom, string deliveryTime, int productRating, bool isPublished,  
        ICollection<ProductVariations> productVariations,
         ICollection<ProductImage> productImages,
         ICollection<ProductAttribute> productAttribute)
        {
            this.Name = name;
            this.Description = description;
            this.Price = price;
            this.RegularPrice = regularPrice;
            this.IsManageStock = isManageStock;
            this.IsDownloadable = isDownloadable;
            this.ProductDownloadUrl = productDownloadUrl;
            this.ProductDownloadUrlExpireOn = productDownloadUrlExpireOn;
            this.ProductDownloadLimit = productDownloadLimit;
            this.IsVirtual = isVirtual;
            this.StockQuantity = stockQuantity;
            this.LowStockThreshold = lowStockThreshold;
            this.ProductWeight = productWeight;
            this.ProductLength = productLength;
            this.ProductWidth = productWidth;
            this.ProductHeight = productHeight;
            this.PictureUrl = pictureUrl;
            this.ProductType = productType;
            this.ProductTypeId = productTypeId;
            this.ProductBrand = productBrand;
            this.ProductBrandId = productBrandId;
            this.HasVariation = hasVariation;
            this.ProductSKU = productSKU;
            this.DeliveryFrom = deliveryFrom;
            this.DeliveryTime = deliveryTime;
            this.ProductRating = productRating;
           // this.IsPublished = isPublished;
              this.ProductVariations = productVariations;
                this.ProductImages = productImages;
                  this.ProductAttribute = productAttribute;

        }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal RegularPrice { get; set; }
        public bool IsManageStock { get; set; }
        public bool IsDownloadable { get; set; }
        public string ProductDownloadUrl { get; set; }
        public DateTimeOffset ProductDownloadUrlExpireOn { get; set; }
        public int ProductDownloadLimit { get; set; }
        public bool IsVirtual { get; set; }
        public int StockQuantity { get; set; }
        public int LowStockThreshold { get; set; }
        public decimal ProductWeight { get; set; }
        public decimal ProductLength { get; set; }
        public decimal ProductWidth { get; set; }
        public decimal ProductHeight { get; set; }
        public string PictureUrl { get; set; }
        public ProductType ProductType { get; set; }
        public int ProductTypeId { get; set; }
        public ProductBrand ProductBrand { get; set; }
        public int ProductBrandId { get; set; }
        public bool HasVariation { get; set; }
        public string ProductSKU { get; set; }
        public string DeliveryFrom { get; set; }
        public string DeliveryTime { get; set; }
        public int ProductRating { get; set; }
        
        public bool IsPublished { get; set; }
        public ICollection<ProductVariations> ProductVariations { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
        public ICollection<ProductAttribute> ProductAttribute { get; set; }
        public ICollection<ProductAttributeValues> ProductAttributeValues { get; set; }

    }
}