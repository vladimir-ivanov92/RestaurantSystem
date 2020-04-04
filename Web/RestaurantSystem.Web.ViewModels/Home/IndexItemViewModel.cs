namespace RestaurantSystem.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using RestaurantSystem.Data.Models;
    using RestaurantSystem.Services.Mapping;

    public class IndexItemViewModel : IMapFrom<Item>
    {
        public int ItemId { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Url => $"/v/{this.Name.Replace(' ', '-')}";
    }
}
