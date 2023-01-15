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
        public ProductsWithTypesAndBrandsSpecification(string sort)
        {
            AddInclude(product => product.productType);
            AddInclude(product => product.productBrand);
            AddOrderBy(product => product.name);

            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
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
