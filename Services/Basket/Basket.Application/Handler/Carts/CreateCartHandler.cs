using Basket.Application.Commands.Carts;
using Basket.Application.Mapper;
using Basket.Application.Response;
using Basket.Core.Entity;
using Basket.Core.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Handler.Carts
{
    public class CreateCartHandler : IRequestHandler<CreateCartCommand, CartResponse>
    {
        private readonly IBasketRepository _basketRepository;

        public CreateCartHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<CartResponse> Handle(CreateCartCommand request, CancellationToken cancellationToken)
        {
            var entity = BasketMapper.Mapper.Map<Cart>(request);
            if (entity == null) {
                throw new Exception("Something wrong when map entity.");
            }
            var result = await _basketRepository.CreateCartItem(entity);
            var res = BasketMapper.Mapper.Map<CartResponse>(result);
            return res;
        }
    }
}
