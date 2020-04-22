namespace RestaurantSystem.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using RestaurantSystem.Data.Models;
    using RestaurantSystem.Services.Mapping;

    public class IndexItemViewModel : IMapFrom<Item>
    {
        public int ItemId { get; set; }

        [Required]
        public string Name { get; set; }

        [Url]
        public string ImageUrl { get; set; }

        public string Url => $"/v/{this.Name.Replace(' ', '-')}";

        [Range(typeof(decimal), "0", "100")]
        public decimal Price { get; set; }

        [Range(1, 100)]
        public int Quantity { get; set; }
    }
}
