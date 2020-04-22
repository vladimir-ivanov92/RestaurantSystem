namespace RestaurantSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RestaurantSystem.Services.Data;
    using RestaurantSystem.Web.ViewModels.Items;

    public class ItemsController : BaseController
    {
        private readonly IItemService itemService;

        public ItemsController(IItemService itemService)
        {
            this.itemService = itemService;
        }

        public IActionResult ByName(string name)
        {
            if (name == null)
            {
                return this.Redirect("/");
            }

            var viewModel = this.itemService.GetByName<ItemViewModel>(name);
            return this.View(viewModel);
        }
    }
}