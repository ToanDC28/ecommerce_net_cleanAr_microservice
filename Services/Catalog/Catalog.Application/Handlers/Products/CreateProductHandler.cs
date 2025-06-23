using Catalog.Application.Commands.Products;
using Catalog.Application.Mappers;
using Catalog.Core.Repositories;
using MediatR;
using Catalog.Core.Entities;
using Catalog.Application.Responses;

namespace Catalog.Application.Handlers.Products
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, ProductResponse>
    {
        private readonly IProductRepository _repository;

        public CreateProductHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = ProductMapper.Mapper.Map<Product>(request);
            if (product == null) {
                throw new ArgumentException("There is something wrong when mapping product");
            }
            var result = await _repository.CreateProduct(product);
            var res = ProductMapper.Mapper.Map<ProductResponse>(result);
            return res;
        }
    }
}
