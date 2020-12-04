using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTO;
using API.Errors;
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
        public async Task<IReadOnlyList<ProductDTO>> GetProducts(){
            var spec=new ProductsWithTypesAndBrandsSpec();
            var data= await productRepo.GetAllListAsync(spec);
            return mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductDTO>>(data);
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