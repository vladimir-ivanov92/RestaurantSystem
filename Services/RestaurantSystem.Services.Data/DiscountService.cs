namespace RestaurantSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using RestaurantSystem.Data.Common.Repositories;
    using RestaurantSystem.Data.Models;
    using RestaurantSystem.Web.ViewModels.Orders;

    public class DiscountService : IDiscountService
    {
        private readonly IRepository<Discount> discountRepository;

        public DiscountService(IRepository<Discount> discountRepository)
        {
            this.discountRepository = discountRepository;
        }

        public async Task<decimal> CalculateDiscount(decimal discount, decimal sumPrice, string userId)
        {
            if (sumPrice > 50)
            {
                discount = 0.02M * sumPrice;
            }

            if (sumPrice > 100)
            {
                discount = 0.04M * sumPrice;
            }

            if (sumPrice > 200)
            {
                discount = 0.10M * sumPrice;
            }

            if (!this.discountRepository.All().Any(x => x.UserId == userId))
            {
                Random rnd = new Random();
                //int luckyNumber = rnd.Next(1, 100);
                int luckyNumber = 7;
                if (luckyNumber == 7)
                {
                    await this.discountRepository.AddAsync(new Discount
                    {
                        DiscountId = Guid.NewGuid().ToString(),
                        UserId = userId,
                    });
                    await this.discountRepository.SaveChangesAsync();
                }
            }

            return discount;
        }

        public decimal CalculateDiscountCode(decimal sumPrice, string userId)
        {
            decimal discountCode = 0M;
            if (this.discountRepository.All().Any(x => x.UserId == userId))
            {
                var discountDelete = this.discountRepository.All().FirstOrDefault(x => x.UserId == userId);

                if (discountDelete.IsApplied == true)
                {
                    discountCode = sumPrice * 0.1M;
                    this.discountRepository.Delete(discountDelete);
                }
            }

            return discountCode;
        }

        public async Task<bool> CheckDiscountCode(string discountCode)
        {
            bool getDiscount = false;
            if (this.discountRepository.All().Any(x => x.DiscountId == discountCode))
            {
                getDiscount = true;
                var discount = this.discountRepository.All().FirstOrDefault(x => x.DiscountId == discountCode);
                discount.IsApplied = true;
                await this.discountRepository.SaveChangesAsync();
            }

            return getDiscount;
        }

        public string GetDiscountCode(string userId)
        {
            var discountCode = "You did not won a discount code this time :(";
            var discount = this.discountRepository.All().FirstOrDefault(x => x.UserId == userId);

            if (discount != null && discount.IsApplied == false)
            {
                discountCode = discount.DiscountId;
            }

            return discountCode;
        }
    }
}
