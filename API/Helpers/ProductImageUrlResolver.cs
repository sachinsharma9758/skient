using API.DTO;
using AutoMapper;
using Core.Entities;
using Microsoft.Extensions.Configuration;

namespace API.Helpers
{
    public class ProductImageUrlResolver : IValueResolver<Product, ProductDTO, string>
    {
        private string ImageUrl=string.Empty;

        public ProductImageUrlResolver(IConfiguration config)
        {
            ImageUrl=config["ApiUrl"].ToString();
        }
        public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PictureUrl)){
                return ImageUrl+source.PictureUrl;
            }

            return null;
        }
    }
}