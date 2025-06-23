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
    public class GetCheckOutCartHandler : IRequestHandler<GetCartByUserId<List<CartResponse>>, List<CartResponse>>
    {
        private readonly IBasketRepository _basketRepository;

        public GetCheckOutCartHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<List<CartResponse>> Handle(GetCartByUserId<List<CartResponse>> request, CancellationToken cancellationToken)
        {
            var result = await _basketRepository.GetCartPaidByUserId(request.UserId);
            var response = BasketMapper.Mapper.Map<List<CartResponse>>(result);
            return response;
        }
    }
}
