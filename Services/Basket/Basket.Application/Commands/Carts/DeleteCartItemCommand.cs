
using MediatR;

namespace Basket.Application.Commands.Carts
{
    public class DeleteCartItemCommand : IRequest<bool>
    {
        public string UserId { get; set; }
        public Guid CartId { get; set; }
        public Guid ItemId { get; set; }

        public DeleteCartItemCommand(string userId, Guid cartId, Guid itemId)
        {
            UserId = userId;
            CartId = cartId;
            ItemId = itemId;
        }
    }
}
