using Basket.Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Response
{
    public class CartResponse
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public string UserId { get; set; }
        public CartStatus Status { get; set; }
        public decimal TotalPrice 
        {
            get {
                decimal total = 0;
                foreach (var item in Items) {
                    total += item.Price * item.Quantity;
                }
                return total;
            }
        }
        public List<CartItemResponse> Items { get; set; }
    }
}
