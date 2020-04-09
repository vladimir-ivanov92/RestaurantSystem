namespace RestaurantSystem.Web.ViewModels.Orders
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using RestaurantSystem.Data.Models;
    using RestaurantSystem.Services.Mapping;

    public class OrderViewModel : IMapFrom<Order>
    {
        public decimal NetAmount { get; set; }

        public decimal Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
