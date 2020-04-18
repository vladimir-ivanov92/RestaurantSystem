namespace RestaurantSystem.Services.Data
{
    using RestaurantSystem.Web.ViewModels.Orders;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IDiscountService
    {
        Task<decimal> CalculateDiscount(decimal discount, decimal sumPrice, string userId);

        Task<bool> CheckDiscountCode(string discountCode);

        decimal CalculateDiscountCode(decimal sumPrice, string userId);

        string GetDiscountCode(string userId);
    }
}
