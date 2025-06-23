using Basket.Core.Entity;
using Basket.Core.Repository;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.Infrastructure.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCache;
        public BasketRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache;
        }

        public async Task<Cart> CreateCartItem(Cart request)
        {
            try
            {
                var data = await _redisCache.GetStringAsync(request.UserId);
                if (string.IsNullOrEmpty(data))
                {
                    List<Cart> carts = new List<Cart>();
                    carts.Add(request);
                    await _redisCache.SetStringAsync(request.UserId, JsonConvert.SerializeObject(carts));
                }
                else
                {
                    var carts = JsonConvert.DeserializeObject<List<Cart>>(data);
                    if (carts == null)
                    {
                        throw new Exception("Deserialize Cart Fail");
                    }
                    carts.Add(request);
                    await _redisCache.RemoveAsync(request.UserId);
                    await _redisCache.SetStringAsync(request.UserId, JsonConvert.SerializeObject(carts));
                }
                return request;
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteCart(string userId, Guid cartId)
        {
            var data = await _redisCache.GetStringAsync(userId);
            if (string.IsNullOrEmpty(data))
            {
                return true;
            }
            var carts = JsonConvert.DeserializeObject<List<Cart>>(data);
            if (carts == null)
            {
                throw new Exception("Deserialize Cart Fail");
            }
            var result = carts.Where(p => p.Status == CartStatus.UnCheckout && p.Id == cartId).FirstOrDefault();
            if (result == null) {
                throw new Exception("Can not find cart or cart can not be removed");
            }
            carts.Remove(result);
            await _redisCache.RemoveAsync(userId);
            await _redisCache.SetStringAsync(userId, JsonConvert.SerializeObject(carts));
            return true;
        }

        public async Task<bool> DeleteCartItem(string userId, Guid cartId, Guid itemId)
        {
            var data = await _redisCache.GetStringAsync(userId);
            if (string.IsNullOrEmpty(data))
            {
                return true;
            }
            var carts = JsonConvert.DeserializeObject<List<Cart>>(data);
            if (carts == null)
            {
                throw new Exception("Deserialize Cart Fail");
            }
            var cart = carts.Where(_ => _.Id == cartId).FirstOrDefault();
            if (cart == null) {
                throw new Exception("Cart can not be found");
            }
            var item = cart.Items.FirstOrDefault(x => x.Id == itemId);
            if (item == null) {
                return true;
            }
            var result = cart.Items.Remove(item);
            await _redisCache.RemoveAsync(userId);
            await _redisCache.SetStringAsync(userId, JsonConvert.SerializeObject(carts));
            return true;
        }

        public async Task<List<Cart>> GetCartPaidByUserId(string userId)
        {
            var data = await _redisCache.GetStringAsync(userId);
            if (string.IsNullOrEmpty(data))
            {
                return null;
            }
            var carts = JsonConvert.DeserializeObject<List<Cart>>(data);
            if (carts == null)
            {
                throw new Exception("Deserialize Cart Fail");
            }
            var result = carts.Where(p => p.Status == CartStatus.Checkout).ToList();
            return result!;
        }

        public async Task<Cart> GetCartUnPaidByUserId(string userId)
        {
            var data = await _redisCache.GetStringAsync(userId);
            if (string.IsNullOrEmpty(data)) {
                return null;
            }
            var carts = JsonConvert.DeserializeObject<List<Cart>>(data);
            if(carts == null)
            {
                throw new Exception("Deserialize Cart Fail");
            }
            var result = carts.Where(p => p.Status == CartStatus.UnCheckout).FirstOrDefault();
            return result!;
        }

        public async Task<Cart> UpdateCart(Cart request)
        {
            var data = await _redisCache.GetStringAsync(request.UserId);
            if (string.IsNullOrEmpty(data))
            {
                return null;
            }
            var carts = JsonConvert.DeserializeObject<List<Cart>>(data);
            if (carts == null)
            {
                throw new Exception("Deserialize Cart Fail");
            }
            var cart = carts.Where(p => p.Status == CartStatus.UnCheckout && p.Id == request.Id).FirstOrDefault();
            if (cart == null) {
                throw new Exception("Can not find cart");
            }
            carts.Remove(cart);
            carts.Add(request);
            await _redisCache.RemoveAsync(request.UserId);
            await _redisCache.SetStringAsync(request.UserId, JsonConvert.SerializeObject(carts));
            return request;
        }
    }
}
