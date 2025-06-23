using Catalog.Application.Mappers;
using Catalog.Application.Queries.Type;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.Type
{
    public class GetAllTypeHandler : IRequestHandler<GetAllTypeQuery, IList<TypeResponse>>
    {
        private readonly ITypeRepository _repository;

        public GetAllTypeHandler(ITypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IList<TypeResponse>> Handle(GetAllTypeQuery request, CancellationToken cancellationToken)
        {
            var types = await _repository.GetAll();
            var response = ProductMapper.Mapper.Map<IList<ProductType>, IList<TypeResponse>>(types.ToList());
            return response;
        }
    }
}
