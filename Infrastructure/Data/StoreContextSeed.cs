using Core.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Infrastructure.Data
{
    public  class StoreContextSeed
    {
        
        public static async Task seedAsync(StoreContext storeContext, ILoggerFactory loggerFactory)
        {
            try
            {
                await seedProductTypeDataAsync(storeContext);
                await seedProductBrandDataAsync(storeContext);
                await seedProductDataAsync(storeContext);
            }
            catch (Exception exception)
            {
                var logger=loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(exception.Message);
            }
        }

        private static async Task seedProductBrandDataAsync(StoreContext storeContext)
        {
            if (!storeContext.ProductBrands.Any())
            {
                var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                var productBrands= JsonConvert.DeserializeObject<List<ProductBrand>>(brandsData);

                productBrands.ForEach(productBrand=> storeContext.ProductBrands.Add(productBrand));
                await storeContext.SaveChangesAsync();
            }
        }

        private static async Task seedProductTypeDataAsync(StoreContext storeContext)
        {
            if (!storeContext.ProductTypes.Any())
            {
                var productTypeData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                var productTypes = JsonConvert.DeserializeObject<List<ProductType>>(productTypeData);
               
                productTypes.ForEach(productType => storeContext.ProductTypes.Add(productType));
                await storeContext.SaveChangesAsync();
            }
        }

        private static async Task seedProductDataAsync(StoreContext storeContext)
        {
            if (!storeContext.Products.Any())
            {
                var productData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                var products = JsonConvert.DeserializeObject<List<Product>>(productData);

                products.ForEach(product => storeContext.Products.Add(product));
                await storeContext.SaveChangesAsync();
            }

        }
    }
}
