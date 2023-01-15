using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specification.Products
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(ProductSpecificationParameters parameters)
            :base(product=>

            (string.IsNullOrEmpty(parameters.Search) || product.name.ToLower().Contains(parameters.Search))
            &&
            (!parameters.BrandId.HasValue || product.productBrandId== parameters.BrandId)  
            &&
            (!parameters.TypeId.HasValue || product.productTypeId== parameters.TypeId) 
            )
        {
            AddInclude(product => product.productType);
            AddInclude(product => product.productBrand);
            AddOrderBy(product => product.name);
            ApplyPaging(parameters.PageSize * (parameters.PageIndex - 1),parameters.PageSize);
            string  sortParameter = parameters.Sort;

            if (!string.IsNullOrEmpty(sortParameter))
            {
                switch (sortParameter)
                {
                    case "priceAsc":
                        AddOrderBy(product => product.price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(product => product.price);
                        break;
                    default:
                        AddOrderBy(product=>product.name);
                        break;
                }
            }
        }

        public ProductsWithTypesAndBrandsSpecification(int id):base(product=>product.id==id)
        {
            AddInclude(product => product.productType);
            AddInclude(product => product.productBrand);
        }
    }
}
