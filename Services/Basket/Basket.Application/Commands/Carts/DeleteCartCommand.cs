
using MediatR;

namespace Basket.Application.Commands.Carts
{
    public class DeleteCartCommand : IRequest<bool>
    {
        public string UserId { get; set; }
        public Guid CartId { get; set; }

        public DeleteCartCommand(string userId, Guid cartId)
        {
            UserId = userId;
            CartId = cartId;
        }
    }
}
