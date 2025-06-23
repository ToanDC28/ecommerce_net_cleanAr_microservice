namespace Basket.Core.Entity
{
    public class CartItem : BaseEntity
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImageFile { get; set; }
    }
}
