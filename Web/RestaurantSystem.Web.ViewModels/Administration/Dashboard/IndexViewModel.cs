namespace RestaurantSystem.Web.ViewModels.Administration.Dashboard
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class IndexViewModel
    {
        public int SettingsCount { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public bool SupplyNeed { get; set; }
    }
}
