using Discount.Core.Entity;
using Discount.Core.Repository;
using Discount.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Discount.Infrastructure.Repository
{
    public class DiscountRepository : IDicountRepository
    {
        private readonly ApplicationDbContext _context;

        public DiscountRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Coupon> CreateCoupone(Coupon request)
        {
            try
            {
                var result = _context.Coupons.AddAsync(request).Result.Entity;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Coupon> GetCouponById(Guid id)
        {
            try
            {
                var result = await _context.Coupons.Where(p => p.Id == id).FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Coupon>> GetCouponByProductID(string productID)
        {
            try
            {
                var result = await _context.Coupons.Where(p => p.ProductId == productID).ToListAsync();
                return result;
            }
            catch (Exception ex) { 
                throw new Exception(ex.Message);
            }
        }

        public async Task<Coupon> ToggleCoupon(Guid id)
        {
            try
            {
                var result = await _context.Coupons.Where(p => p.Id == id).FirstOrDefaultAsync();
                if(result == null)
                {
                    throw new Exception("Coupon is not found");
                }
                result.isActivate = !result.isActivate;
                await _context.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Coupon> UpdateCoupone(Coupon request)
        {
            try
            {
                var result = await _context.Coupons.Where(p => p.Id == request.Id).FirstOrDefaultAsync();
                if (result == null)
                {
                    throw new Exception("Coupon is not found");
                }
                result.Description = request.Description;
                result.Amount = request.Amount;
                result.ExpiredDate = request.ExpiredDate;
                result.isActivate = request.isActivate;
                await _context.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
