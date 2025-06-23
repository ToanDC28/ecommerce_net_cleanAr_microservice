using Catalog.Core.Entities;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrastructure.Data
{
    public static class TypeContextSeed
    {
        public static void SeedData(IMongoCollection<ProductType> typeCollection)
        {
            bool checkExisting = typeCollection.Find(b => true).Any();
            if (!checkExisting)
            {
                Console.WriteLine("Seeding Type....");
                string path = Path.Combine(AppContext.BaseDirectory, "Data", "SeedData", "type.json");
                var data = File.ReadAllText(path);
                var types = JsonSerializer.Deserialize<List<ProductType>>(data);
                if (types != null) { 
                    typeCollection.InsertMany(types);
                    Console.WriteLine("Seeding Type Successfully....");
                }
            }
        }
    }
}
