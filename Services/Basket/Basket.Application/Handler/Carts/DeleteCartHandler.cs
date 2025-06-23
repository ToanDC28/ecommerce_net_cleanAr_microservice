using Basket.Application.Commands.Carts;
using Basket.Core.Repository;
using MediatR;

namespace Basket.Application.Handler.Carts
{
    public class DeleteCartHandler : IRequestHandler<DeleteCartCommand, bool>
    {
        private readonly IBasketRepository _basketRepository;

        public DeleteCartHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<bool> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
        {
            var result = await _basketRepository.DeleteCart(request.UserId, request.CartId);
            return result;
        }
    }
}
