﻿namespace RestaurantSystem.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class IndexViewModel
    {
        public IEnumerable<IndexItemViewModel> Items { get; set; }

        public int PagesCount { get; set; }

        public int CurrentPage { get; set; }

        public int CurrentQuantity { get; set; }

    }
}
