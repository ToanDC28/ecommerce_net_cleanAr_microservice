using Catalog.Application.Mappers;
using Catalog.Application.Queries.Products;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.Products
{
    public class GetProductByBrandIdHandler : IRequestHandler<GetProductByBrandQuery, List<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByBrandIdHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<ProductResponse>> Handle(GetProductByBrandQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetProductsByBrand(request.BrandId);
            var res = ProductMapper.Mapper.Map<List<ProductResponse>>(products);
            return res;
        }
    }
}
