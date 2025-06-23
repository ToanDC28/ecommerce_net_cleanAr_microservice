using Basket.Application.Response;
using Basket.Core.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Query.Carts
{
    public class GetCartByUserId<T> : IRequest<T>
    {
        public string UserId { get; set; }

        public GetCartByUserId(string userId)
        {
            UserId = userId;
        }
    }
}
