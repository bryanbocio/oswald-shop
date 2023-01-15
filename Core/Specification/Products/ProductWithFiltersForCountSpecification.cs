using Core.Entities;

namespace Core.Specification.Products
{
    public class ProductWithFiltersForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFiltersForCountSpecification(ProductSpecificationParameters parameters)
              : base(product =>
             (!parameters.BrandId.HasValue || product.productBrandId == parameters.BrandId)
             &&
             (!parameters.TypeId.HasValue || product.productTypeId == parameters.TypeId)
            )
        {

        }
    }
}
