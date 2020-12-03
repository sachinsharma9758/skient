using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpec :BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpec()
        {
            IncludeValues();
        }

        private void IncludeValues(){
            AddIncludes(x=>x.ProductBrand);
            AddIncludes(x=>x.ProductType);
        }

        public ProductsWithTypesAndBrandsSpec(int id) 
        : base(x=>x.Id==id)
        {
            IncludeValues();
        }
    }
}