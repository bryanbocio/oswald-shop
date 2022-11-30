using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Core.Interfaces;

namespace API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductBrand> _productBrandsRepository;
        private readonly IGenericRepository<ProductType> _productTypesRepository;

        public ProductsController(
                            IGenericRepository<Product> productRepository, 
                            IGenericRepository<ProductBrand> productBrandsRepository,
                            IGenericRepository<ProductType> productTypesRepository)
        {
            _productRepository = productRepository;
            _productBrandsRepository = productBrandsRepository;
            _productTypesRepository = productTypesRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> getProducts(){
            return Ok(await _productRepository.listAllAsync());
        }

        [HttpGet("{id}")] 
        public async Task<ActionResult<Product>>getProduct(int id){
            return Ok(await _productRepository.getByIdAsync(id));
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
    }
}