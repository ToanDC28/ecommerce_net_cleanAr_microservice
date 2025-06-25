using Discount.Core.Entity;

namespace Discount.Core.Repository
{
    public interface IDicountRepository
    {
        Task<List<Coupon>> GetCouponByProductID(string productID);
        Task<Coupon> GetCouponById(Guid id);
        Task<Coupon> CreateCoupone(Coupon request);
        Task<Coupon> UpdateCoupone(Coupon request);
        Task<Coupon> ToggleCoupon(Guid id);
    }
}
