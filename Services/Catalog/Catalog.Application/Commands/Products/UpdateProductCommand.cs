using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Commands.Products
{
    public class UpdateProductCommand : IRequest<ProductResponse>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Sumary { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }
        public decimal Price { get; set; }
        public string BrandId { get; set; }
        public string TypeId { get; set; }
    }
}
