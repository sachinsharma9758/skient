using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context,ILoggerFactory loggerFactory){
            try
            {
                var brandsJson=await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/brands.json");
            var typesJson=await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/types.json");
            var productsJson=await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/products.json");

            var brands=JsonSerializer.Deserialize<List<ProductBrand>>(brandsJson);
            var types=JsonSerializer.Deserialize<List<ProductType>>(typesJson);
            var products=JsonSerializer.Deserialize<List<Product>>(productsJson);

            if(!context.ProductBrands.Any()){
                foreach(var item in brands){
                context.ProductBrands.Add(item);
            }
            await context.SaveChangesAsync();
            }

            if(!context.ProductTypes.Any()){
                foreach(var item in types){
                context.ProductTypes.Add(item);
            }
            await context.SaveChangesAsync();
            }
            if(!context.Products.Any()){
                foreach(var item in products){
                context.Products.Add(item);
            }
            await context.SaveChangesAsync();
            }
            
            }
            catch (System.Exception ex)
            {
                var logger=loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex,"Some error occured during data seeding");
            }

        }
    }
}