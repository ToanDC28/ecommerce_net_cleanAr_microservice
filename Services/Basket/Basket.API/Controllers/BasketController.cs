using Basket.Application.Commands.Carts;
using Basket.Application.Query.Carts;
using Basket.Application.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.API.Controllers
{
    public class BasketController : ApiController
    {
        private IMediator _mediator;

        public BasketController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("uncheckout/user={userId}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(CartResponse), (int)HttpStatusCode.OK)]
        public Task<CartResponse> GetCartUnCheckOutByUserId(string userId)
        {
            return _mediator.Send(new GetCartByUserId<CartResponse>(userId));
        }
        [HttpGet("checkout/user={userId}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(List<CartResponse>), (int)HttpStatusCode.OK)]
        public Task<List<CartResponse>> GetCartCheckOutByUserId(string userId)
        {
            return _mediator.Send(new GetCartByUserId<List<CartResponse>>(userId));
        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        public Task<CartResponse> CreateCart(CreateCartCommand request)
        {
            return _mediator.Send(request);
        }
        [HttpDelete("userId={userId}/cartId={cartId}/itemId={itemId}")]
        [MapToApiVersion("1.0")]
        public Task<bool> RemoveItemFromCart(string userId, Guid cartId, Guid itemId)
        {
            return _mediator.Send(new DeleteCartItemCommand(userId, cartId, itemId));
        }
        [HttpDelete("userId={userId}/cartId={cartId}")]
        [MapToApiVersion("1.0")]
        public Task<bool> RemoveCart(string userId, Guid cartId)
        {
            return _mediator.Send(new DeleteCartCommand(userId, cartId));
        }
    }
}
