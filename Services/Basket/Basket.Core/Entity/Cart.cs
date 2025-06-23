namespace Basket.Core.Entity
{
    public class Cart : BaseEntity
    {
        public string UserId { get; set; }
        public CartStatus Status { get; set; }
        public List<CartItem> Items { get; set;} = new List<CartItem>();
    }
}
