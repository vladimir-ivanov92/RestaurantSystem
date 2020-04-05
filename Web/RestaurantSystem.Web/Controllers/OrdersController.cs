namespace RestaurantSystem.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SignalR;
    using RestaurantSystem.Data;
    using RestaurantSystem.Data.Models;
    using RestaurantSystem.Services.Data;
    using RestaurantSystem.Web.Hubs;
    using RestaurantSystem.Web.ViewModels.Orders;

    public class OrdersController : BaseController
    {
        private readonly IOrderService orderService;
        private readonly UserManager<ApplicationUser> userManager;
        ApplicationDbContext dbContext;
        private readonly IHubContext<RestaurantHub> restaurantHub;

        public OrdersController(IOrderService orderService, UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext, IHubContext<RestaurantHub> restaurantHub)
        {
            this.orderService = orderService;
            this.userManager = userManager;
            this.dbContext = dbContext;
            this.restaurantHub = restaurantHub;
        }

        [HttpPost]
        public async Task<IActionResult> Order([FromBody] OrderViewModel order)
        {
            await this.restaurantHub.Clients.All.SendAsync("NewOrder", order);
            var orderId = this.orderService.NewOrder();
            return this.Accepted(orderId);
        }

        public async Task<IActionResult> AddItemAsync(int itemId)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId

            var orderExist = this.orderService.CheckForExistingOrder(userId);

            if (!orderExist)
            {
               await this.orderService.CreateOrder(userId);
            }

            this.orderService.AddItemToOrder(itemId, userId);

            return new EmptyResult();
            //return this.Response.Redirect(this.Request.RawUrl)
        }

        public async Task<ApplicationUser> GetCurrentUser()
        {
            ApplicationUser usr = await GetCurrentUserAsync();
            return usr;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => this.userManager.GetUserAsync(this.HttpContext.User);
    }
}
