using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.ProductBrands.Any())
                {
                    var brandsData =
                        File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");

                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    foreach (var item in brands)
                    {
                        context.ProductBrands.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.ProductTypes.Any())
                {
                    var typesData =
                        File.ReadAllText("../Infrastructure/Data/SeedData/types.json");

                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    foreach (var item in types)
                    {
                        context.ProductTypes.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.Products.Any())
                {
                    var productsData =
                        File.ReadAllText("../Infrastructure/Data/SeedData/products.json");

                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    foreach (var item in products)
                    {
                        context.Products.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.ProductImages.Any())
                {
                    var productsData = new ProductImage
                    {
                        Id = 1,
                        NameAlt = "na",
                        ImageUrl = "assets/images/hero1.jpg",
                        ImageThumbUrl = "assets/images/hero1.jpg",
                        ImageBigUrl = "assets/images/hero1.jpg",
                        IsRelativeUrl = true,
                        ProductId = 1
                    };

                    context.Add(productsData);
                    await context.SaveChangesAsync();

                    productsData = new ProductImage
                    {
                        Id = 2,
                        NameAlt = "na",
                        ImageUrl = "assets/images/hero2.jpg",
                        ImageThumbUrl = "assets/images/hero2.jpg",
                        ImageBigUrl = "assets/images/hero2.jpg",
                        IsRelativeUrl = true,
                        ProductId = 1
                    };
                    context.Add(productsData);
                    await context.SaveChangesAsync();
                }

                if (!context.DeliveryMethods.Any())
                {
                    var dmData =
                        File.ReadAllText("../Infrastructure/Data/SeedData/delivery.json");

                    var methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(dmData);

                    foreach (var item in methods)
                    {
                        context.DeliveryMethods.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.ProductAttribute.Any())
                {
                    var productAttribute = new ProductAttribute
                    {
                        Id = 1,
                        AttributeName = "Color",
                        IsVisibleOnProductPage = true,
                        IsUsedForVariations = true,
                        ProductId = 1
                    };
                    context.Add(productAttribute);

                    productAttribute = new ProductAttribute
                    {
                        Id = 2,
                        AttributeName = "Size",
                        IsVisibleOnProductPage = true,
                        IsUsedForVariations = true,
                        ProductId = 1
                    };
                    context.Add(productAttribute);
                    await context.SaveChangesAsync();
                }

                if (!context.ProductAttributeValues.Any())
                {
                    var productAttributeValue = new ProductAttributeValues
                    {
                        Id = 1,
                        AttributeValueName = "Red",
                        AttributeValueParentId = 0,
                        ProductAttributeId = 1,
                        ProductId = 1
                    };
                    context.Add(productAttributeValue);

                    productAttributeValue = new ProductAttributeValues
                    {
                        Id = 2,
                        AttributeValueName = "Blue",
                        AttributeValueParentId = 0,
                        ProductAttributeId = 1,
                        ProductId = 1
                    };
                    context.Add(productAttributeValue);

                    productAttributeValue = new ProductAttributeValues
                    {
                        Id = 3,
                        AttributeValueName = "Green",
                        AttributeValueParentId = 0,
                        ProductAttributeId = 1,
                        ProductId = 1
                    };
                    context.Add(productAttributeValue);

                    productAttributeValue = new ProductAttributeValues
                    {
                        Id = 4,
                        AttributeValueName = "M",
                        AttributeValueParentId = 0,
                        ProductAttributeId = 2,
                        ProductId = 1
                    };
                    context.Add(productAttributeValue);

                    productAttributeValue = new ProductAttributeValues
                    {
                        Id = 5,
                        AttributeValueName = "XL",
                        AttributeValueParentId = 0,
                        ProductAttributeId = 2,
                        ProductId = 1
                    };
                    context.Add(productAttributeValue);

                    await context.SaveChangesAsync();


                   var productVariations = new ProductVariations
                    {
                        Id = 1,
                        Name = "Red",                       
                        Price = 2,
                        RegularPrice = 3,
                        StockQuantity = 100,            
                        ProductWeight = 100,
                        ProductLength = 11,
                        ProductWidth =1,
                        ProductHeight = 2,
                        PictureUrl = "/",
                        ProductSKU = "Red",
                        ProductId = 1,
                        ProductAttributeId = 1,
                        ProductAttributeValuesId = 1,
                    };
                    context.Add(productVariations);

                    await context.SaveChangesAsync();

                    productVariations = new ProductVariations
                    {
                        Id = 2,
                        Name = "Blue",                       
                        Price = 20,
                        RegularPrice = 30,
                        StockQuantity = 100,            
                        ProductWeight = 100,
                        ProductLength = 11,
                        ProductWidth =1,
                        ProductHeight = 2,
                        PictureUrl = "/",
                        ProductSKU = "Blue",
                        ProductId = 1,
                        ProductAttributeId = 1,
                        ProductAttributeValuesId = 2,
                    };
                    context.Add(productVariations);

                    await context.SaveChangesAsync();
                }



            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}