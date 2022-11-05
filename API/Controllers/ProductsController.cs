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
        private readonly IProductRepository _productRepository;
        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> getProducts(){
            return Ok(await _productRepository.getProductsAsync());
        }

        [HttpGet("{id}")] 
        public async Task<ActionResult<Product>>getProduct(int id){
            return Ok(await _productRepository.getProductsByIdAsync(id));
        }

    }
}