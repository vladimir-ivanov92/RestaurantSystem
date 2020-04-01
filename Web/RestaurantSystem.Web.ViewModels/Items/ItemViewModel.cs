﻿namespace RestaurantSystem.Web.ViewModels.Items
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using RestaurantSystem.Data.Models;
    using RestaurantSystem.Services.Mapping;

    public class ItemViewModel : IMapFrom<Item>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int PreparationTime { get; set; }

        public decimal Price { get; set; }
    }
}