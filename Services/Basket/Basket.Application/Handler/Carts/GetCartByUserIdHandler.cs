using Basket.Application.Mapper;
using Basket.Application.Query.Carts;
using Basket.Application.Response;
using Basket.Core.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Handler.Carts
{
    public class GetCartByUserIdHandler : IRequestHandler<GetCartByUserId<CartResponse>, CartResponse>
    {
        private readonly IBasketRepository _basketRepository;

        public GetCartByUserIdHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<CartResponse> Handle(GetCartByUserId<CartResponse> request, CancellationToken cancellationToken)
        {
            var carts = await _basketRepository.GetCartUnPaidByUserId(request.UserId);
            if (carts != null)
            {
                var response = BasketMapper.Mapper.Map<CartResponse>(carts);
                if (response == null) {
                    throw new Exception("Something wrong when map data.");
                }
                return response;
            }
            return null;
        }
    }
}
