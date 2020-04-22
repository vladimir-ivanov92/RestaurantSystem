namespace RestaurantSystem.Web.ViewModels.Discounts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using RestaurantSystem.Data.Models;
    using RestaurantSystem.Services.Mapping;

    public class DiscountViewModel : IMapFrom<Discount>
    {
        [Required]
        [NonEmptyGuid]
        public string DiscountId { get; set; }
    }
}
