namespace RestaurantSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SignalR;
    using RestaurantSystem.Data;
    using RestaurantSystem.Data.Models;
    using RestaurantSystem.Services.Data;
    using RestaurantSystem.Services.Messaging;
    using RestaurantSystem.Web.Hubs;
    using RestaurantSystem.Web.ViewModels.Orders;

    public class OrdersController : BaseController
    {
        private readonly IOrderService orderService;
        private readonly IDiscountService discountService;
        private readonly UserManager<ApplicationUser> userManager;
        ApplicationDbContext dbContext;
        private readonly IHubContext<RestaurantHub> restaurantHub;
        private readonly IEmailSender mailService;

        public OrdersController(IOrderService orderService, UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext, IHubContext<RestaurantHub> restaurantHub, IEmailSender mailService, IDiscountService discountService)
        {
            this.orderService = orderService;
            this.userManager = userManager;
            this.dbContext = dbContext;
            this.restaurantHub = restaurantHub;
            this.mailService = mailService;
            this.discountService = discountService;
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
            var user = await this.GetCurrentUserAsync();
            decimal sumPrice = 0.00M;
            decimal discount = 0.00M;

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

            discount = await this.discountService.CalculateDiscount(discount, sumPrice, userId);
            var discountCode = this.discountService.CalculateDiscountCode(sumPrice, userId);
            discount += discountCode;

            OrderViewModel netAmount = new OrderViewModel
            {
                NetAmount = sumPrice,
                Discount = discount,
                DeliveryTax = 2,
                DiscountCode = this.discountService.GetDiscountCode(userId),
            };

            Order alreadyDeliveredOrder = this.dbContext.Orders.FirstOrDefault(x => x.UserId == userId);
            alreadyDeliveredOrder.IsDeleted = true;
            alreadyDeliveredOrder.DeletedOn = DateTime.Now;
            alreadyDeliveredOrder.NetAmount = sumPrice;

            List<OrderItem> alreadyDeliveredItems = this.dbContext.OrderItems.Where(x => x.OrderId == order.Id).ToList();
            this.dbContext.OrderItems.RemoveRange(alreadyDeliveredItems);
            await this.dbContext.SaveChangesAsync();

            if (user != null)
            {
                await this.mailService.SendEmailAsync(user.Email, "MyApp", "vladimir920522@gmail.com", "Testing", "<h1>Order was made</h1>", null);
            }
            return this.View(netAmount);
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => this.userManager.GetUserAsync(this.HttpContext.User);
    }
}
