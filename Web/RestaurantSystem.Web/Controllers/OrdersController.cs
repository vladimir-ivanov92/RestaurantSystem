namespace RestaurantSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
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

        public async Task<IActionResult> AddItemAsync([FromQuery(Name = "page")] int page, int itemId, int quantity)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId

            var orderExist = this.orderService.CheckForExistingOrder(userId);

            if (!orderExist)
            {
                await this.orderService.CreateOrder(userId);
            }

            await this.orderService.AddItemToOrder(itemId, userId, quantity);

            return this.Redirect($"/?page={page}");
        }

        public async Task<ApplicationUser> GetCurrentUser()
        {
            ApplicationUser usr = await this.GetCurrentUserAsync();
            return usr;
        }

        public async Task<IActionResult> CheckOut()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = this.dbContext.Orders.Where(x => x.UserId == userId).FirstOrDefault();
            decimal sumPrice = 0.00M;

            var itemPrice = this.dbContext.Orders.
                     Join(this.dbContext.OrderItems, u => u.Id, uir => uir.OrderId, (u, uir) => new { u, uir }).
                     Join(this.dbContext.Items, r => r.uir.ItemId, ro => ro.ItemId, (r, ro) => new { r, ro })
                     .Where(m => m.r.u.Id == order.Id)
                     .Select(m => new OrderViewModel
                     {
                         Price = m.ro.Price,
                         Quantity = m.r.uir.Quantity,
                     });

            foreach (var item in itemPrice)
            {
                sumPrice += item.Price * item.Quantity;
            }

            OrderViewModel netAmount = new OrderViewModel
            {
                NetAmount = sumPrice,
            };

            Order alreadyDeliveredOrder = this.dbContext.Orders.FirstOrDefault(x => x.UserId == userId);
            alreadyDeliveredOrder.IsDeleted = true;
            alreadyDeliveredOrder.DeletedOn = DateTime.Now;
            alreadyDeliveredOrder.NetAmount = sumPrice;

            List<OrderItem> alreadyDeliveredItems = this.dbContext.OrderItems.Where(x => x.OrderId == order.Id).ToList();
            this.dbContext.OrderItems.RemoveRange(alreadyDeliveredItems);
            await this.dbContext.SaveChangesAsync();

            return this.View(netAmount);
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => this.userManager.GetUserAsync(this.HttpContext.User);
    }
}
