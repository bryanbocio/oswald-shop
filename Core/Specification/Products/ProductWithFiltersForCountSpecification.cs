using Core.Entities;

namespace Core.Specification.Products
{
    public class ProductWithFiltersForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFiltersForCountSpecification(ProductSpecificationParameters parameters)
              : base(product =>
             (string.IsNullOrEmpty(parameters.Search) || product.name.ToLower().Contains(parameters.Search))
              &&
             (!parameters.BrandId.HasValue || product.productBrandId == parameters.BrandId)
             &&
             (!parameters.TypeId.HasValue || product.productTypeId == parameters.TypeId)
            )
        {

        }
    }
}
