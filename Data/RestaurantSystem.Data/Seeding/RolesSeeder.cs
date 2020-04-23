namespace RestaurantSystem.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using RestaurantSystem.Common;
    using RestaurantSystem.Data.Models;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using System.Collections.Generic;

    internal class RolesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            List<string> roles = new List<string>
            {
                GlobalConstants.AdministratorRoleName,
                GlobalConstants.AdministratorRoleNameDriver,
                GlobalConstants.AdministratorRoleNameCook,
                GlobalConstants.AdministratorRoleNameWaiter,
            };

            foreach (string role in roles)
            {
                await this.SeedRoleAsync(roleManager, role);
            }
        }

        private async Task SeedRoleAsync(RoleManager<ApplicationRole> roleManager, string role)
        {
            var exist = await roleManager.FindByNameAsync(role);
            if (exist == null)
            {
                var result = await roleManager.CreateAsync(new ApplicationRole(role));
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
