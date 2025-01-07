using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repsitory.Data
{
    public static class StoreContextSeed
    {
        //Seeding data to the database

        public static async Task SeedAsync(StoreContext dbContext)
        {
            // Seeding ProductBrands

            if (!dbContext.ProductBrands.Any()) { 
            var BrandData = File.ReadAllText("../Talabat.Repsitory/Data/DataSeed/brands.json");
            var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandData);

            if (Brands?.Count > 0)
            {
                foreach (var brand in Brands)
                {
                    await dbContext.Set<ProductBrand>().AddAsync(brand);
                }
                await dbContext.SaveChangesAsync();

            }

        }


            // Seeding ProductTypes

            if (!dbContext.ProductTypes.Any())
            {

                var TypeData = File.ReadAllText("../Talabat.Repsitory/Data/DataSeed/types.json");
                var Types = JsonSerializer.Deserialize<List<ProductType>>(TypeData);

                if (Types?.Count > 0)
                {
                    foreach (var type in Types)
                    {
                        await dbContext.Set<ProductType>().AddAsync(type);
                    }
                    await dbContext.SaveChangesAsync();
                }
            }


            // Seeding Products

            if (!dbContext.Products.Any())
            {


                var ProductData = File.ReadAllText("../Talabat.Repsitory/Data/DataSeed/products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductData);

                if (Products?.Count > 0)
                {
                    foreach (var product in Products)
                    {
                        await dbContext.Set<Product>().AddAsync(product);
                    }
                    await dbContext.SaveChangesAsync();
                }

            }

            //await dbContext.Database.MigrateAsync();
        }
    }
}
