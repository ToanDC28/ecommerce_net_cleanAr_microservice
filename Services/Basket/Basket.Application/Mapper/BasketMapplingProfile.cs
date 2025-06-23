using AutoMapper;
using Basket.Application.Commands.Carts;
using Basket.Application.Response;
using Basket.Core.Entity;

namespace Basket.Application.Mapper
{
    public class BasketMapplingProfile : Profile
    {
        public BasketMapplingProfile()
        {
            CreateMap<Cart, CartResponse>().ReverseMap();
            CreateMap<CartItem, CartItemResponse>().ReverseMap();
            CreateMap<Cart, CreateCartCommand>().ReverseMap();
            CreateMap<CartItem, CreateCartItemCommand>().ReverseMap();
        }
    }
}
