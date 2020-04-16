namespace RestaurantSystem.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Moq;
    using RestaurantSystem.Data;
    using RestaurantSystem.Data.Common.Repositories;
    using RestaurantSystem.Data.Models;
    using RestaurantSystem.Data.Repositories;
    using RestaurantSystem.Services.Mapping;
    using Xunit;

    public class IngredientsServiceTests
    {
        [Fact]
        public void TestGetIngredientById()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                     .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var ingredientrRepository = new EfRepository<Ingredient>(new ApplicationDbContext(options.Options));
            var itemRepository = new EfRepository<Item>(new ApplicationDbContext(options.Options));

            ingredientrRepository.AddAsync(new Ingredient { Name = "Eggs", Quantity = 5 }).GetAwaiter().GetResult();
            ingredientrRepository.SaveChangesAsync().GetAwaiter().GetResult();

            var ingredientService = new IngredientService(itemRepository, ingredientrRepository);

            AutoMapperConfig.RegisterMappings(typeof(MyTestIngredient).Assembly);

            var ingredient = ingredientService.GetById<MyTestIngredient>(1);

            Assert.Equal("Eggs", ingredient.Name);
            Assert.Equal(5, ingredient.Quantity);
        }

        public class MyTestIngredient : IMapFrom<Ingredient>
        {
            public string Name { get; set; }

            public int Quantity { get; set; }
        }
    }
}
