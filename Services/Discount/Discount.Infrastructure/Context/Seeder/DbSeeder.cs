using Discount.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Discount.Infrastructure.Context.Seeder
{
    public static class DbSeeder
    {
        public static IHost MigrateDatabase<TContext>(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var service = scope.ServiceProvider;
                var config = service.GetRequiredService<IConfiguration>();
                var logger = service.GetRequiredService<ILogger<TContext>>();
                try
                {
                    logger.LogInformation("Seeding Discount Data...");
                    SeedDiscountData(config);
                    logger.LogInformation("Completed To Seeding Discount Data...");
                }
                catch (Exception ex) { 
                    logger.LogError($"Error when seeding discount data. Error Message: {ex.Message}");
                }
            }
            return host;
        }

        private static void SeedDiscountData(IConfiguration config)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var connectionString = config.GetConnectionString("DefaultConnection");

            optionsBuilder.UseNpgsql(connectionString);

            using var context = new ApplicationDbContext(optionsBuilder.Options);
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            if (context.Coupons.Any()) {
                return;
            }
            var coupons = new List<Coupon>
    {
        new Coupon
        {
            ProductId = "fe9183ff-5a4b-46a1-9f44-2d61d83b42d9",
            Description = "10% off",
            Amount = 10,
            ExpiredDate = DateTime.Now.AddMonths(1),
            isActivate = true,
            CreateAt = DateTime.Now,
            UpdateAt = DateTime.Now
        },
        new Coupon
        {
            ProductId = "9e801245-bbd1-4de7-9c43-3d36a576e82d",
            Description = "15% off for summer sale",
            Amount = 15,
            ExpiredDate = DateTime.Now.AddMonths(2),
            isActivate = true,
            CreateAt = DateTime.Now,
            UpdateAt = DateTime.Now
        }
    };
            context.Coupons.AddRange(coupons);
            context.SaveChanges();
        }
    }
}
