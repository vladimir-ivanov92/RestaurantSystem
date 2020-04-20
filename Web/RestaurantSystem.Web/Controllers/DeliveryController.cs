namespace RestaurantSystem.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using RestaurantSystem.Data.Models;
    using RestaurantSystem.Services.Messaging;

    public class DeliveryController : BaseController
    {
        private readonly IEmailSender mailService;
        private readonly UserManager<ApplicationUser> userManager;

        public DeliveryController(IEmailSender mailService, UserManager<ApplicationUser> userManager)
        {
            this.mailService = mailService;
            this.userManager = userManager;
        }

        public IActionResult Map()
        {
            return this.View();
        }

        public async Task<IActionResult> GetInTouch(string email, string message, string subject, string name)
        {
            var user = await this.GetCurrentUserAsync();
            if (user != null)
            {
                await this.mailService.SendEmailAsync($"{email}", $"{name}", "vladimir920522@gmail.com", $"{subject}", $"<h1>{message}</h1>", null);
            }

            return this.RedirectToAction("Map");
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => this.userManager.GetUserAsync(this.HttpContext.User);
    }
}
