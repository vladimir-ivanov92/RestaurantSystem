namespace RestaurantSystem.Web.ViewModels.Orders
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using RestaurantSystem.Data.Models;
    using RestaurantSystem.Services.Mapping;

    public class OrderViewModel : IMapFrom<Order>
    {
        [Range(typeof(decimal), "0", "10000")]
        public decimal NetAmount { get; set; }

        public decimal Quantity { get; set; }

        [Range(typeof(decimal), "0", "100")]
        public decimal Price { get; set; }

        [Range(typeof(decimal), "0", "20")]
        public decimal Discount { get; set; }

        public decimal DeliveryTax { get; set; }

        public string DiscountCode { get; set; }

        public string Address { get; set; }
    }
}
