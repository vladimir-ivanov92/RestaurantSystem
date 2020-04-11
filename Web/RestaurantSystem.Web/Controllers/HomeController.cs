namespace RestaurantSystem.Web.Controllers
{
    using System.Diagnostics;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Mvc;
    using RestaurantSystem.Services.Data;
    using RestaurantSystem.Web.ViewModels;
    using RestaurantSystem.Web.ViewModels.Home;

    public class HomeController : BaseController
    {
        private const int ItemsPerPage = 12;

        private readonly IItemService itemService;
        private readonly IOrderService orderService;

        public HomeController(IItemService itemService, IOrderService orderService)
        {
            this.itemService = itemService;
            this.orderService = orderService;
        }

        public IActionResult Index(int page = 1)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var viewModel = new IndexViewModel
            {
                Items = this.itemService.GetAll<IndexItemViewModel>(),
                CheckForOrder = this.orderService.CheckForExistingOrder(userId),
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