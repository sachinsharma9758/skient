using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
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
            return await productRepo.GetAllListAsync();
        }

        [HttpGet("{id}")]
        public async Task<Product> GetProduct(int id){
            return await productRepo.GetByIdAsync(id);
        }
    }
}