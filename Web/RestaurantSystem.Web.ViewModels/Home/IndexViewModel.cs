namespace RestaurantSystem.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class IndexViewModel
    {
        public IEnumerable<IndexItemViewModel> Items { get; set; }
    }
}
