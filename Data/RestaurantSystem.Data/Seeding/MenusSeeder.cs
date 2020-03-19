namespace RestaurantSystem.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using RestaurantSystem.Data.Models;

    public class MenusSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)

        {

            if (dbContext.Menus.Any())
            {
                return;
            }

            var menus = new List<string>()
            {
                "Breakfast Menu",
                "Lunch Menuu",
                "Dinner Menu",
            };

            foreach (var menu in menus)
            {
                await dbContext.Menus.AddAsync(new Menu
                {
                    Name = menu,
                });
            }
        }
    }
}
