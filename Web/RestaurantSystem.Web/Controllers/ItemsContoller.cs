namespace RestaurantSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using RestaurantSystem.Services.Data;
    using RestaurantSystem.Web.ViewModels.Items;

    public class ItemsContoller : BaseController
    {
        private readonly IItemService itemService;

        public ItemsContoller(IItemService itemService)
        {
            this.itemService = itemService;
        }

        public IActionResult ByName(string name)
        {
            var viewModel = this.itemService.GetByName<ItemViewModel>(name);
            return this.View(viewModel);
        }
    }
}
