using Basket.Application.Commands.Carts;
using Basket.Core.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Handler.Carts
{
    public class DeleteCartItemHandler : IRequestHandler<DeleteCartItemCommand, bool>
    {
        private readonly IBasketRepository _repository;

        public DeleteCartItemHandler(IBasketRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteCartItemCommand request, CancellationToken cancellationToken)
        {
            var result = await _repository.DeleteCartItem(request.UserId, request.CartId, request.ItemId);
            return result;
        }
    }
}
