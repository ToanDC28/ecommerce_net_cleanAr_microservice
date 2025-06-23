using Catalog.Core.Entities;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrastructure.Data
{
    public static class ProductContextSeed
    {
        public static void SeedData(IMongoCollection<Product> collection) {
            bool checkExisting = collection.Find(p => true).Any();
            if (!checkExisting) {
                Console.WriteLine("Seeding Product....");
                string path = Path.Combine(AppContext.BaseDirectory, "Data", "SeedData", "product.json");
                var data = File.ReadAllText(path);
                var products = JsonSerializer.Deserialize<List<Product>>(data);
                collection.InsertManyAsync(products);
                Console.WriteLine("Seeding Product Successfully....");
            }
        }
    }
}
