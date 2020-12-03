using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTO;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController :ControllerBase
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
        public async Task<ProductDTO> GetProduct(int id){
            var spec=new ProductsWithTypesAndBrandsSpec(id);
            var data =await productRepo.GetEntityBySpec(spec);
            return mapper.Map<Product,ProductDTO>(data);
        }
    }
}