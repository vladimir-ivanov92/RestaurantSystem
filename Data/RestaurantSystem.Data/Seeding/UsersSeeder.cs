namespace RestaurantSystem.Data.Seeding
{
    using System;
    using System.Collections.Generic;
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
            List<string> users = new List<string> { "John@abv.bg", "Kim@abv.bg" };

            foreach (var user in users)
            {
                var exists = await userManager.FindByNameAsync(user);
                if (exists == null)
                {
                    var result = await userManager.CreateAsync(
                        new ApplicationUser
                        {
                            UserName = user,
                            Email = user,
                            EmailConfirmed = true,
                        }, user);

                    if (!result.Succeeded)
                    {
                        throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                    }

                    return;
                }
            }
        }
    }
}
