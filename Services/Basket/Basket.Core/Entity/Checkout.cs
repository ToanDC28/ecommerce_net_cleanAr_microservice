using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Core.Entity
{
    public class Checkout : BaseEntity
    {
        public string? UserId { get; set; }
        public string CartId { get; set; }
        public decimal TotalPrice { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string Expiration {  get; set; }
        public string Cvv { get; set; }
        public CheckoutStatus Status { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

    }
}
