using Catalog.Application.Responses;
using Catalog.Core.filters;
using MediatR;

namespace Catalog.Application.Queries.Products
{
    public class GetProductQuery : IRequest<PaginationResponse<ProductResponse>>
    {
        public PaginationFilter Filter { get; set; }

        public GetProductQuery(PaginationFilter filter)
        {
            Filter = filter;
        }
    }
}
