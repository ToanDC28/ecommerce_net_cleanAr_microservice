using Catalog.Application.Commands.Products;
using Catalog.Application.Queries.Brand;
using Catalog.Application.Queries.Products;
using Catalog.Application.Queries.Type;
using Catalog.Application.Responses;
using Catalog.Core.filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Controllers.Catalog
{
    public class CatalogController : ApiController
    {
        private readonly IMediator mediator;

        public CatalogController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("product/getall")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(IList<ProductResponse>), (int)HttpStatusCode.OK)]
        public Task<PaginationResponse<ProductResponse>> GetAllProduct([FromBody] PaginationFilter filter)
        {
            return mediator.Send(new GetProductQuery(filter));
        }

        [HttpGet("product/id={id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
        public Task<ProductResponse> GetProductById(string id)
        {
            return mediator.Send(new GetProductByIdQuery(id));
        }
        
        [HttpGet("product/brandId={id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(List<ProductResponse>), (int)HttpStatusCode.OK)]
        public Task<List<ProductResponse>> GetProductByBrand(string id)
        {
            return mediator.Send(new GetProductByBrandQuery(id));
        }

        [HttpGet("product/name={name}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(IList<ProductResponse>), (int)HttpStatusCode.OK)]
        public Task<IList<ProductResponse>> GetProductByName(string name)
        {
            return mediator.Send(new GetProductByNameQuery(name));
        }

        [HttpPost("product")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
        public Task<ProductResponse> CreateProduct([FromBody] CreateProductCommand request)
        {
            return mediator.Send(request);
        }

        [HttpPut("product")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
        public Task<ProductResponse> UpdateProduct([FromBody] UpdateProductCommand request)
        {
            return mediator.Send(request);
        }

        [HttpDelete("product/id={id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public Task<bool> DeleteProduct(string id)
        {
            return mediator.Send(new DeleteProductCommand(id));
        }

        [HttpGet("brand")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(IList<BrandReponse>), (int)HttpStatusCode.OK)]
        public Task<IList<BrandReponse>> GetAllBrand()
        {
            return mediator.Send(new GetAllBrandQuery());
        }
        [HttpGet("type")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(IList<TypeResponse>), (int)HttpStatusCode.OK)]
        public Task<IList<TypeResponse>> GetAllType()
        {
            return mediator.Send(new GetAllTypeQuery());
        }
    }
}
