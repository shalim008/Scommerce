using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IGenericRepository<ProductAttribute> _productAttrRepo;
        private readonly IGenericRepository<ProductAttributeValues> _productAttrValuesRepo;
        private readonly IProductRepository _prodRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productsRepo, 
        IGenericRepository<ProductBrand> productBrandRepo, 
        IGenericRepository<ProductType> productTypeRepo, 
        IGenericRepository<ProductAttribute> productAttrRepo, 
        IGenericRepository<ProductAttributeValues> productAttrValuesRepo, 
        IProductRepository prodRepo,
        IMapper mapper)
        {
            _mapper = mapper;
            _productTypeRepo = productTypeRepo;
            _productBrandRepo = productBrandRepo;
            _productsRepo = productsRepo;
            _productAttrRepo = productAttrRepo;
            _productAttrValuesRepo = productAttrValuesRepo;
            _prodRepo = prodRepo;
        }
        
        [Cached(600)]
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts(
            [FromQuery]ProductSpecParams productParams)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(productParams);

            var countSpec = new ProductWithFiltersForCountSpecificication(productParams);

            var totalItems = await _productsRepo.CountAsync(countSpec);

            var products = await _productsRepo.ListAsync(spec);

            var data = _mapper
                .Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

            return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex, productParams.PageSize, totalItems, data));
        }
        
        [Cached(600)]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);

            var product = await _productsRepo.GetEntityWithSpec(spec);            

            if (product == null) return NotFound(new ApiResponse(404));

            var data = _mapper.Map<Product, ProductToReturnDto>(product);

            return data;
        }

        [Cached(600)]
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            var data = await _productBrandRepo.ListAllAsync();
            return Ok(data.Where(ob=>ob.DataStatus !=0));
        }

        [Cached(600)]
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            var data = await _productTypeRepo.ListAllAsync();
            return Ok(data.Where(ob=>ob.DataStatus !=0));
        }

        [Cached(600)]
        [HttpGet("productAttribute")]
        public async Task<ActionResult<IReadOnlyList<ProductAttribute>>> GetProductAttributes()
        {
            var data = await _productAttrRepo.ListAllAsync();
            return Ok(data.Where(ob=>ob.DataStatus !=0));
        }

        [Cached(600)]
        [HttpGet("productAttributeValues")]
        public async Task<ActionResult<IReadOnlyList<ProductAttributeValues>>> GetProductAttributeValues(int id)
        {
            // Need to Improve
            var data = await _productAttrValuesRepo.ListAllAsync();
            return Ok(data.Where(ob=>ob.DataStatus !=0 && ob.Id == id).Select(ob=>ob.AttributeValueName).Distinct());
        }

        [HttpPost("product-info")]
        public async Task<ActionResult<ProductDetailsForm>> ProductEntry([FromForm]ProductDetailsForm values)
        {                   
             IFormFile  pictureFile = values.PictureFile;
             List<IFormFile> gallaryImages = values.GallaryImages;
             var productInfo = _mapper.Map<Product>(values);
             productInfo.ProductBrandId = values.Brands;
             productInfo.ProductTypeId = values.Types;
             var data = await _prodRepo.SetProductInfoAsync(productInfo, gallaryImages, pictureFile);
             return Ok(data);
        }


    }
}