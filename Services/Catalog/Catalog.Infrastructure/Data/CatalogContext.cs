﻿using Catalog.Core.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data
{
    public class CatalogContext : ICatalogContext
    {
        public IMongoCollection<Product> Products { get; }

        public IMongoCollection<ProductBrand> Brands {  get; }

        public IMongoCollection<ProductType> Types { get; }

        public CatalogContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("Connection"));
            var database = client.GetDatabase(configuration.GetSection("DatabaseSetting:DatabaseName").Value);
            Brands = database.GetCollection<ProductBrand>(configuration.GetSection("DatabaseSetting:Brand").Value);
            Types = database.GetCollection<ProductType>(configuration.GetSection("DatabaseSetting:Type").Value);
            Products = database.GetCollection<Product>(configuration.GetSection("DatabaseSetting:Product").Value);
            
            BrandContextSeed.SeedData(Brands);
            TypeContextSeed.SeedData(Types);
            ProductContextSeed.SeedData(Products);
        }
    }
}
