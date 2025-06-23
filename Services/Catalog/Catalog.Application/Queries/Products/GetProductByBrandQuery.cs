using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Queries.Products
{
    public class GetProductByBrandQuery : IRequest<List<ProductResponse>>
    {
        public GetProductByBrandQuery(string brandId)
        {
            BrandId = brandId;
        }

        public string BrandId { get; set; }
    }
}
