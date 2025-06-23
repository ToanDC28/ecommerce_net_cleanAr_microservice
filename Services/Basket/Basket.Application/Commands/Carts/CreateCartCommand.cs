using Basket.Application.Response;
using Basket.Core.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Commands.Carts
{
    public class CreateCartCommand : IRequest<CartResponse>
    {
        public string UserId { get; set; }
        public CartStatus Status { get; set; } = CartStatus.UnCheckout;
        public List<CreateCartItemCommand> Items { get; set;} = new List<CreateCartItemCommand>();
    }
}
