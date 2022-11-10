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
        private readonly StoreContext _storeContext;

        public ProductRepository(StoreContext storeContext)
        {
            this._storeContext = storeContext;
        }

        public async Task<IReadOnlyList<Product>> getProductsAsync()
        {
            return await _storeContext.Products
                   .Include(product => product.productBrand)
                   .Include(product => product.productType)
                   .ToListAsync();

        }

        public async Task<Product> getProductsByIdAsync(int id)
        {
            var product = await _storeContext.Products
                         .Include(product=>product.productBrand)
                         .Include(product=>product.productType)
                         .FirstOrDefaultAsync(product=>product.id==id);
            return product;
        }

        public async Task<IReadOnlyList<ProductBrand>> getProductsBrandsAsync()
        {
            return await _storeContext.ProductBrands.ToListAsync();
        }

        public async Task<IReadOnlyList<ProductType>> getProductTypesAsync()
        {
            return await _storeContext.ProductTypes.ToListAsync();  
        }
    }
}
