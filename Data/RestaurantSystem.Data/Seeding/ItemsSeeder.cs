namespace RestaurantSystem.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using RestaurantSystem.Data.Models;

    public class ItemsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Items.Any())
            {
                return;
            }

            var items = new List<(string Name, string ImageUrl)>
            {
                ("Meatball", "https://boutiquedekristina.com/wp-content/uploads/2018/08/18-08-18-15-51-02-081_deco.jpg"),
                ("Kebapche", "https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcQ-HpXQyzPvtvTZaNuqu2gOBQz3KE6rzyQthQTo1Bs1Uqzhidek"),
                ("Ribs", "https://gotvach.bg/files/lib/600x350/rebarca1.jpg"),
                ("Steak", "https://media1.s-nbcnews.com/i/newscms/2018_07/1318715/grilled-steak-today-tease-180216_89508b219dd455b4d43311782841f938.jpg"),
                ("Chicken soup", "https://www.fifteenspatulas.com/wp-content/uploads/2016/02/Chicken-Noodle-Soup-Fifteen-Spatulas-2-640x427.jpg"),
            };

            foreach (var item in items)
            {
                await dbContext.Items.AddAsync(new Item
                {
                    Name = item.Name,
                    ImageUrl = item.ImageUrl,
                    MenuId = 2,
                });
            }
        }
    }
}
