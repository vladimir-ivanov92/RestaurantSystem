namespace RestaurantSystem.Web.ViewModels.Discounts
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using RestaurantSystem.Data.Models;
    using RestaurantSystem.Services.Mapping;

    public class DiscountViewModel : IMapFrom<Discount>
    {
        public string DiscountId { get; set; }
    }
}
