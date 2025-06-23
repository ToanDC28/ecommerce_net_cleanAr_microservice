using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Queries.Products
{
    public class GetProductByNameQuery : IRequest<IList<ProductResponse>>
    {
        public GetProductByNameQuery(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
