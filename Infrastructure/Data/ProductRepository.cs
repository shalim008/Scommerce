using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using Microsoft.Extensions.FileProviders;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;

        public ProductRepository()
        {
        }

        public ProductRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            return await _context.ProductBrands.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.ProductType)
                .Include(p => p.ProductBrand)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await _context.Products
                .Include(p => p.ProductType)
                .Include(p => p.ProductBrand)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            return await _context.ProductTypes.ToListAsync();
        }
        public async Task<Product> SetProductInfoAsync(Product data, List<IFormFile> gallaryImages, IFormFile FeatureImage)
        {

            var featureImages = CustomCrop(ContentDispositionHeaderValue.Parse(FeatureImage.ContentDisposition).FileName.Trim('"'), FeatureImage);
            var dataImages = UploadFilesAsync(gallaryImages);
            int i = 0;
            
            
            Product p = new Product();
            p.Name = data.Name;
            p.Description = data.Description;
            p.Price = data.Price;
            p.RegularPrice = data.RegularPrice;
            p.IsManageStock = data.IsManageStock;
            p.IsDownloadable = data.IsDownloadable;
            p.ProductDownloadUrl = data.ProductDownloadUrl;
            p.ProductDownloadUrlExpireOn = data.ProductDownloadUrlExpireOn;
            p.ProductDownloadLimit = data.ProductDownloadLimit;
            p.IsVirtual = data.IsVirtual;
            p.StockQuantity = data.StockQuantity;
            p.LowStockThreshold = data.LowStockThreshold;
            p.ProductWeight = data.ProductWeight;
            p.ProductLength = data.ProductLength;
            p.ProductWidth = data.ProductWidth;
            p.ProductHeight = data.ProductHeight;
            p.ProductTypeId = data.ProductTypeId;
            p.ProductBrandId = data.ProductBrandId;
            p.HasVariation = data.HasVariation;
            p.ProductSKU = data.ProductSKU;
            p.DeliveryFrom = data.DeliveryFrom;
            p.DeliveryTime = data.DeliveryTime;
            p.ProductRating = data.ProductRating;
            // p.IsPublished = isPublished;
            p.ProductVariations = data.ProductVariations;           
            p.ProductAttribute = data.ProductAttribute;
            p.PictureUrl = featureImages.ImageBigUrl;
            _context.Products.Add(p);
            var d = await _context.SaveChangesAsync();
                       
            foreach (var item in dataImages)
            {
                ProductImage ob = new ProductImage();                
                ob.ImageUrl = item.ImageUrl;
                ob.ImageThumbUrl = item.ImageThumbUrl;
                ob.ImageBigUrl = item.ImageBigUrl;
                ob.IsRelativeUrl = item.IsRelativeUrl;
                ob.ProductId = p.Id;
                ob.NameAlt = data.Name + "_" + i++;
                _context.ProductImages.Add(ob);
            }
           var imgStatus= await _context.SaveChangesAsync();
           
           return p;
        }

        public List<ProductImage> UploadFilesAsync(List<IFormFile> files)
        {
            List<ProductImage> lstProdImag = new List<ProductImage>();

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    try
                    {
                        var d = CustomCrop(ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName.Trim('"'), formFile);
                        if (d != null)
                        {
                            lstProdImag.Add(d);
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            return lstProdImag;
        }



        public ProductImage CustomCrop(string filename, IFormFile blob)
        {
            try
            {

                ProductImage ob = new ProductImage();
                using (var image = Image.Load(blob.OpenReadStream()))
                {


                    string systemFileExtenstion = filename.Substring(filename.LastIndexOf('.'));
                    image.Mutate(x => x.Resize(250, 250));
                    var newfileName250 = GenerateFileName("Photo_250_250_", systemFileExtenstion);
                    var filepath250 = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Content", "images//products")).Root + $@"\{newfileName250}";
                    string databasePath =Path.Combine("images", "products") + $@"\{newfileName250}";
                    ob.ImageThumbUrl = databasePath.Replace(@"\","/");

                    image.Save(filepath250);

                    var newfileName650 = GenerateFileName("Photo_750_750_", systemFileExtenstion);
                    var filepath650 = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Content", "images//products")).Root + $@"\{newfileName650}";
                    image.Mutate(x => x.Resize(750, 750));
                    
                    databasePath = Path.Combine("images", "products") + $@"\{newfileName650}";
                    ob.ImageBigUrl =databasePath.Replace(@"\","/");
                    ob.ImageUrl = databasePath.Replace(@"\","/");
                    image.Save(filepath650);


                }

                return ob;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public string GenerateFileName(string fileTypeName, string fileextenstion)
        {
            if (fileTypeName == null) throw new ArgumentNullException(nameof(fileTypeName));
            if (fileextenstion == null) throw new ArgumentNullException(nameof(fileextenstion));
            return $"{fileTypeName}_{DateTime.Now:yyyyMMddHHmmssfff}_{Guid.NewGuid():N}{fileextenstion}";
        }



    }
}