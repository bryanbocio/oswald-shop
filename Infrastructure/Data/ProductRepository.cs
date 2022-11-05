using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly IProductRepository _productRepository;
        private readonly StoreContext _storeContext;

        public ProductRepository(StoreContext storeContext)
        {
            this._storeContext = storeContext;
        }

        public async Task<IReadOnlyList<Product>> getProductsAsync()
        {
            return await _storeContext.Products.ToListAsync();
        }


        public async Task<Product> getProductsByIdAsync(int id)
        {
             var product =await _storeContext.Products.FindAsync(id);
            return product;
        }
    }
}
