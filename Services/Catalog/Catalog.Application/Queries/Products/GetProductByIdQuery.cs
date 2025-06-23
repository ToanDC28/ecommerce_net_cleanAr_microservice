using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Queries.Products
{
    public class GetProductByIdQuery : IRequest<ProductResponse>
    {
        public GetProductByIdQuery(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }
}
