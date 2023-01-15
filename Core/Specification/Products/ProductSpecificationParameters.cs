using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specification.Products
{
    public class ProductSpecificationParameters
    {
        private const int MaxPageSize = 50;
        public int PageIndex { get; set; } = 1;

        private int _pagSize = 6;

        public int PageSize 
        { 
            get => _pagSize; 
            set => _pagSize = (value>MaxPageSize)?MaxPageSize: value; 
        }

        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string? Sort { get; set; }

        private string _search;

        public string Search 
        {
            get => _search;
            set => _search =value.ToLower();
        }

    }
}
