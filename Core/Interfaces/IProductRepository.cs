using Core.Entities;

namespace Core.Interfaces;

public interface IProductRepository
{
    Task<Product> getProductsByIdAsync(int id);
    Task<IReadOnlyList<Product>> getProductsAsync();
    Task<IReadOnlyList<ProductBrand>> getProductsBrandsAsync();
    Task<IReadOnlyList<ProductType>> getProductTypesAsync();

}
