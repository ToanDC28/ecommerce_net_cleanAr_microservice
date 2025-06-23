using Catalog.Core.Entities;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrastructure.Data
{
    public static class BrandContextSeed
    {
        public static void SeedData(IMongoCollection<ProductBrand> brandCollection)
        {
            bool checkExisting = brandCollection.Find(b => true).Any();
            if (!checkExisting) {
                Console.WriteLine("Seeding Brand....");
                string path = Path.Combine(AppContext.BaseDirectory, "Data", "SeedData", "brand.json");
                var data = File.ReadAllText(path);
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(data);
                if (brands != null) {
                    brandCollection.InsertManyAsync(brands);
                    Console.WriteLine("Seeding Brand Successfully....");
                }
            }
        }
    }
}
