using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpec :BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpec(ProductSpecParams productParams)
        :base(x=>
            (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search))&&
            (!productParams.BrandId.HasValue || x.ProductBrandId==productParams.BrandId) &&
            (!productParams.TypeId.HasValue || x.ProductTypeId==productParams.TypeId)
        )
        {
            IncludeValues();
            AddOrderBy(x=>x.Name);
            ApplyPaging(productParams.PageSize,(productParams.PageSize*(productParams.PageIndex-1)));
            if(!string.IsNullOrEmpty(productParams.Sort)){
                switch(productParams.Sort){
                    case "priceAsc":
                        AddOrderBy(x=>x.Price);
                        break;
                    case "priceDsc":
                        AddOrderByDescending(x=>x.Price);
                        break;
                    default:
                        AddOrderBy(x=>x.Name);
                        break;
                }
            }
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