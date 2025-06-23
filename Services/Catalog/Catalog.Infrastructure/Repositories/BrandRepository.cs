using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly ICatalogContext _context;
        public BrandRepository(ICatalogContext context)
        {
            _context = context;   
        }
        public async Task<IEnumerable<ProductBrand>> GetAll()
        {
            return await _context.Brands.Find(p => true).ToListAsync();
        }
    }
}
