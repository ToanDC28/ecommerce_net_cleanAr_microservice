using Catalog.Application.Mappers;
using Catalog.Application.Queries.Products;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.filters;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.Products
{
    public class GetAllProductHandler : IRequestHandler<GetProductQuery, PaginationResponse<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<PaginationResponse<ProductResponse>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var response = await _productRepository.GetAll(request.Filter);
            var products = ProductMapper.Mapper.Map<IList<Product>, IList<ProductResponse>>(response.Data);
            var result = new PaginationResponse<ProductResponse>(products.ToList(), response.TotalCount, response.CurrentPage, response.PageSize);
            return result;
        }
    }
}
