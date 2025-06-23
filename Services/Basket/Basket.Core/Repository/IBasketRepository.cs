using Basket.Core.Entity;

namespace Basket.Core.Repository
{
    public interface IBasketRepository
    {
        Task<Cart> GetCartUnPaidByUserId(string userId);
        Task<List<Cart>> GetCartPaidByUserId(string userId);
        Task<Cart> CreateCartItem(Cart request);
        Task<Cart> UpdateCart(Cart request);
        Task<bool> DeleteCart(string userId, Guid cartId);
        Task<bool> DeleteCartItem(string userId, Guid cartId, Guid itemId);
    }
}
