using Catalog.Core.Entities;
using Catalog.Core.filters;

namespace Catalog.Core.Repositories
{
    public interface IProductRepository
    {
        Task<PaginationResponse<Product>> GetAll(PaginationFilter filter);
        Task<Product> GetProductByID(string id);
        Task<IEnumerable<Product>> GetProductsByBrand(string brandId);
        Task<IEnumerable<Product>> GetProductsByName(string name);
        Task<Product> CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(string id);
    }
}
