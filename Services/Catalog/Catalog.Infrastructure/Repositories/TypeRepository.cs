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
    public class TypeRepository : ITypeRepository
    {
        private readonly ICatalogContext _context;

        public TypeRepository(ICatalogContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductType>> GetAll()
        {
            return await _context.Types.Find(p => true).ToListAsync();
        }
    }
}
