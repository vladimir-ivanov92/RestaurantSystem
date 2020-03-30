namespace RestaurantSystem.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
    using RestaurantSystem.Services.Data;
    using RestaurantSystem.Web.ViewModels;
    using RestaurantSystem.Web.ViewModels.Home;

    public class HomeController : BaseController
    {
        private readonly IItemService itemService;

        public HomeController(IItemService itemService)
        {
            this.itemService = itemService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel
            {
                Items = this.itemService.GetAll<IndexItemViewModel>(),
            };
            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
