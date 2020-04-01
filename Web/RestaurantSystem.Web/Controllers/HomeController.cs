namespace RestaurantSystem.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
    using RestaurantSystem.Services.Data;
    using RestaurantSystem.Web.ViewModels;
    using RestaurantSystem.Web.ViewModels.Home;

    public class HomeController : BaseController
    {
        private const int ItemsPerPage = 3;

        private readonly IItemService itemService;

        public HomeController(IItemService itemService)
        {
            this.itemService = itemService;
        }

        public IActionResult Index(int page = 1)
        {
            var viewModel = new IndexViewModel
            {
                Items = this.itemService.GetAll<IndexItemViewModel>(),
            };

            viewModel.Items = this.itemService.GetItemsPerPage<IndexItemViewModel>(ItemsPerPage, (page - 1) * ItemsPerPage);

            var count = this.itemService.GetCount();
            viewModel.PagesCount = (int)System.Math.Ceiling((double)count / ItemsPerPage);
            if (viewModel.PagesCount == 0)
            {
                viewModel.PagesCount = 1;
            } 

            viewModel.CurrentPage = page;

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