namespace RestaurantSystem.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using RestaurantSystem.Common;
    using RestaurantSystem.Data.Models;

    public class UsersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

            await SeedUserAsync(userManager);
        }

        private static async Task SeedUserAsync(UserManager<ApplicationUser> userManager)
        {
            var exists = await userManager.FindByNameAsync("John@abv.bg");
            if (exists == null)
            {
                var result = await userManager.CreateAsync(
                    new ApplicationUser
                    {
                        UserName = "John@abv.bg",
                        Email = "John@abv.bg",
                        EmailConfirmed = true,
                    }, "John@abv.bg");

                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
                return;
            }
        }
    }
}
