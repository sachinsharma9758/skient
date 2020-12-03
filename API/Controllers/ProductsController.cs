using System.Collections.Generic;
using System.Threading.Tasks;
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

        public ProductsController(IGenericRepository<Product> productRepo)
        {
            this.productRepo = productRepo;
        }

        [HttpGet]
        public async Task<IReadOnlyList<Product>> GetProducts(){
            var spec=new ProductsWithTypesAndBrandsSpec();
            return await productRepo.GetAllListAsync(spec);
        }

        [HttpGet("{id}")]
        public async Task<Product> GetProduct(int id){
            var spec=new ProductsWithTypesAndBrandsSpec();
            return await productRepo.GetEntityBySpec(spec);
        }
    }
}