using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Core.Interfaces;
using Core.Specification.Products;
using API.DTOs;
using AutoMapper;

namespace API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductBrand> _productBrandsRepository;
        private readonly IGenericRepository<ProductType> _productTypesRepository;
        private readonly IMapper _mapper;
        public ProductsController(
                            IGenericRepository<Product> productRepository, 
                            IGenericRepository<ProductBrand> productBrandsRepository,
                            IGenericRepository<ProductType> productTypesRepository,
                            IMapper mapper)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _productBrandsRepository = productBrandsRepository;
            _productTypesRepository = productTypesRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> getProducts(){

            var products= await _productRepository.listAsync(new ProductsWithTypesAndBrandsSpecification());

            return products.Select(product=>buildProductDto(product)).ToList();
            


        }

        [HttpGet("{id}")] 
        public async Task<ActionResult<ProductDto>>getProduct(int id){
            
            var product =await _productRepository.getEntityWithSpecification(new ProductsWithTypesAndBrandsSpecification(id));

            return _mapper.Map<Product,ProductDto>(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> getProductBrands()
        {
            return Ok(await _productBrandsRepository.listAllAsync());
        }

        [HttpGet("product-types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> getProductTypes()
        {
            return Ok(await _productTypesRepository.listAllAsync());
        }


        private ProductDto buildProductDto(Product product)
        {
           return new ProductDto
            {
                id = product.id,
                name = product.name,
                description = product.description,
                pictureUrl = product.pictureUrl,
                price = product.price,
                productBrand = product.productBrand.name,
                productType = product.productType.name
            };
        }
    }
}