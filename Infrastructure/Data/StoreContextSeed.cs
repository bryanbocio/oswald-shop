using Core.Entities;
using Core.Entities.OrderAggregate;
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
                await seedDeliveryDataAsync(storeContext);

                if(storeContext.ChangeTracker.HasChanges()) await storeContext.SaveChangesAsync();
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

               storeContext.ProductBrands.AddRange(productBrands);
            }
        }

        private static async Task seedProductTypeDataAsync(StoreContext storeContext)
        {
            if (!storeContext.ProductTypes.Any())
            {
                var productTypeData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                var productTypes = JsonConvert.DeserializeObject<List<ProductType>>(productTypeData);
               
                storeContext.ProductTypes.AddRange(productTypes);
            }
        }

        private static async Task seedProductDataAsync(StoreContext storeContext)
        {
            if (!storeContext.Products.Any())
            {
                var productData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                var products = JsonConvert.DeserializeObject<List<Product>>(productData);

                storeContext.Products.AddRange(products);
            }

        }

        private static async Task seedDeliveryDataAsync(StoreContext storeContext)
        {
            if (!storeContext.DeliveryMethods.Any())
            {
                var deliveryMethodsData= File.ReadAllText("../Infrastructure/Data/SeedData/delivery.json");
                var deliveryMethods = JsonConvert.DeserializeObject<List<DeliveryMethod>>(deliveryMethodsData);
                    
                storeContext.DeliveryMethods.AddRange(deliveryMethods); 
              
            }

        }

    }
}
