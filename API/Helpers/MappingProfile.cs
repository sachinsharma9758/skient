using API.DTO;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product,ProductDTO>()
            .ForMember(x=>x.ProductBrand,o=>o.MapFrom(s=>s.ProductBrand.Name))
            .ForMember(x=>x.ProductType, o=>o.MapFrom(s=>s.ProductType.Name))
            .ForMember(x=>x.PictureUrl, o=>o.MapFrom<ProductImageUrlResolver>());
        }
    }
}