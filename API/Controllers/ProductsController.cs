using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTO;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductsController :BaseApiController
    {
        private readonly IGenericRepository<Product> productRepo;
        private readonly IMapper mapper;

        public ProductsController(IGenericRepository<Product> productRepo, IMapper mapper)
        {
            this.productRepo = productRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductDTO>>> GetProducts([FromQuery]ProductSpecParams productParams){
            var spec=new ProductsWithTypesAndBrandsSpec(productParams);
            var countSpec=new ProductWithFiltersForCountSpec(productParams);
            var totalItems=await productRepo.CountAsync(countSpec);

            var products= await productRepo.GetAllListAsync(spec);
            var data=mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductDTO>>(products);

            return Ok(new Pagination<ProductDTO>(productParams.PageIndex,productParams.PageSize,totalItems,data)) ;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id){
            var spec=new ProductsWithTypesAndBrandsSpec(id);
            var data =await productRepo.GetEntityBySpec(spec);
            
            if(data==null) return NotFound(new ApiResponse(404));

            return mapper.Map<Product,ProductDTO>(data);
        }
    }
}