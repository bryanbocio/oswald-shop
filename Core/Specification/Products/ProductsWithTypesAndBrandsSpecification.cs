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
        public ProductsWithTypesAndBrandsSpecification()
        {
            AddInclude(product => product.productType);
            AddInclude(product => product.productBrand);
        }

        public ProductsWithTypesAndBrandsSpecification(int id):base(product=>product.id==id)
        {
            AddInclude(product => product.productType);
            AddInclude(product => product.productBrand);
        }
    }
}
