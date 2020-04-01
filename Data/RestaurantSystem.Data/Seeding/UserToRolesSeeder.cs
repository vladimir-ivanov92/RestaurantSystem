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

    public class UserToRolesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            var user = await userManager.FindByNameAsync("John@abv.bg");
            var role = await roleManager.FindByNameAsync("Driver");

            var user2 = await userManager.FindByNameAsync("Kim@abv.bg");
            var role2 = await roleManager.FindByNameAsync("Administrator");

            var exists = dbContext.UserRoles.Any(x => x.UserId == user.Id && x.RoleId == role.Id);
            var exists2 = dbContext.UserRoles.Any(x => x.UserId == user2.Id && x.RoleId == role2.Id);

            if (exists || exists2)
            {
                return;
            }

            Dictionary<ApplicationUser, ApplicationRole> userRoles = new Dictionary<ApplicationUser, ApplicationRole>() { { user, role }, { user2, role2 } };

            foreach (var kvp in userRoles)
            {
                dbContext.UserRoles.Add(new IdentityUserRole<string>
                {
                    RoleId = kvp.Value.Id,
                    UserId = kvp.Key.Id,
                });
            }
        }
    }
}
