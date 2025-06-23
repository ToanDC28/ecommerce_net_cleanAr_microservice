using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.filters;
using Catalog.Core.Repositories;
using Catalog.Core.Specifications;
using Catalog.Infrastructure.Data;
using MongoDB.Driver;
using System.Threading;

namespace Catalog.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;
        public ProductRepository(ICatalogContext context)
        {
            _context = context;
        }
        public async Task<Product> CreateProduct(Product product)
        {
            await _context.Products.InsertOneAsync(product);
            return product;
        }

        public async Task<bool> DeleteProduct(string id)
        {
            var deleted = await _context.Products.DeleteOneAsync(p => p.Id == id);
            return deleted.IsAcknowledged && deleted.DeletedCount > 0;
        }

        public async Task<PaginationResponse<Product>> GetAll(PaginationFilter filter)
        {
            var spec = CatalogSpecificationBuilder.Build<Product>(filter);
            var query = _context.Products.Find(spec.Filter);
            if (spec.Sort != null) { 
                query = query.Sort(spec.Sort);
            }

            var total = await query.CountDocumentsAsync();
            var data = await query.Skip(spec.Skip).Limit(spec.Take).ToListAsync();

            return new PaginationResponse<Product>(data, (int)total, filter.PageNumber, filter.PageSize);
        }

        public async Task<Product> GetProductByID(string id)
        {
            return await _context.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByBrand(string brandId)
        {
            return await _context.Products.Find(p => p.BrandId == brandId).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByName(string name)
        {
            return await _context.Products.Find(p => p.Name.ToLower().Contains(name.ToLower())).ToListAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var updated = await _context.Products.ReplaceOneAsync(p => p.Id == product.Id, product);
            return updated.IsAcknowledged && updated.ModifiedCount > 0;
        }
    }
}
