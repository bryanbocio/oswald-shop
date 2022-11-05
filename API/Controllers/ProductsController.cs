using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
namespace API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly StoreContext _storeContext;

        public ProductsController(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> getProducts(){
            return Ok(await _storeContext.Products.ToListAsync());
        }

        [HttpGet("{id}")] 
        public async Task<ActionResult<Product>>getProduct(int id){
            var product = await _storeContext.Products.FindAsync(id);
            return Ok(product);

        }

    }
}