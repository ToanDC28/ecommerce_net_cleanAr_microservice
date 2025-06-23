using AutoMapper;
using Catalog.Application.Mappers;
using Catalog.Application.Queries.Brand;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.Brand
{
    public class GetAllBrandHandler : IRequestHandler<GetAllBrandQuery, IList<BrandReponse>>
    {
        private readonly IBrandRepository _brandRepository;
        //private readonly IMapper _mapper;

        public GetAllBrandHandler(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
            //_mapper = mapper;
        }

        public async Task<IList<BrandReponse>> Handle(GetAllBrandQuery request, CancellationToken cancellationToken)
        {
            var brands = await _brandRepository.GetAll();
            //var response = _mapper.Map<List<BrandReponse>>(brands.ToList());
            var response = ProductMapper.Mapper.Map<IList<ProductBrand>, IList<BrandReponse>>(brands.ToList());
            return response;
        }
    }
}
