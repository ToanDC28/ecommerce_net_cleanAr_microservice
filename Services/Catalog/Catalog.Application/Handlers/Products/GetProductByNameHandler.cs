using Catalog.Application.Mappers;
using Catalog.Application.Queries.Products;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.Products
{
    public class GetProductByNameHandler : IRequestHandler<GetProductByNameQuery, IList<ProductResponse>>
    {
        private readonly IProductRepository _repository;

        public GetProductByNameHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IList<ProductResponse>> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
        {
            var products = await _repository.GetProductsByName(request.Name);
            var res = ProductMapper.Mapper.Map<IList<ProductResponse>>(products);
            return res;
        }
    }
}
