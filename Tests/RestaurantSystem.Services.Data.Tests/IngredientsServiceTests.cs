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

        [Fact]
        public async Task TestAddIngredientToItem()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                     .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var ingredientrRepository = new EfRepository<Ingredient>(new ApplicationDbContext(options.Options));
            var itemRepository = new EfRepository<Item>(new ApplicationDbContext(options.Options));

            ingredientrRepository.AddAsync(new Ingredient { Name = "Eggs", Quantity = 5 }).GetAwaiter().GetResult();
            ingredientrRepository.SaveChangesAsync().GetAwaiter().GetResult();

            itemRepository.AddAsync(new Item {Name = "Meatball" }).GetAwaiter().GetResult();
            itemRepository.SaveChangesAsync().GetAwaiter().GetResult();

            var item = itemRepository.All().Where(x => x.Name == "Meatball").FirstOrDefault();
            var ingredient = ingredientrRepository.All().Where(x => x.Name == "Eggs").FirstOrDefault();

            var ingredientService = new IngredientService(itemRepository, ingredientrRepository);

            //AutoMapperConfig.RegisterMappings(typeof(MyTestIngredient).Assembly);

            await ingredientService.AddIngredientToItem(ingredient.Name, ingredient.Quantity, item.Name);

            Assert.Equal(1, item.Ingredients.Count);

            ingredientrRepository.Delete(ingredient);
        }

        [Fact]
        public async Task TestEditIngredientToItem()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var ingredientrRepository = new EfRepository<Ingredient>(new ApplicationDbContext(options.Options));
            var itemRepository = new EfRepository<Item>(new ApplicationDbContext(options.Options));

            ingredientrRepository.AddAsync(new Ingredient { Name = "Eggs", Quantity = 5 }).GetAwaiter().GetResult();
            ingredientrRepository.SaveChangesAsync().GetAwaiter().GetResult();

            itemRepository.AddAsync(new Item { Name = "Meatball" }).GetAwaiter().GetResult();
            itemRepository.SaveChangesAsync().GetAwaiter().GetResult();

            var item = itemRepository.All().Where(x => x.Name == "Meatball").FirstOrDefault();
            var ingredient = ingredientrRepository.All().Where(x => x.Name == "Eggs").FirstOrDefault();

            var ingredientService = new IngredientService(itemRepository, ingredientrRepository);       
            //AutoMapperConfig.RegisterMappings(typeof(MyTestIngredient).Assembly);

            await ingredientService.AddIngredientToItem(ingredient.Name, ingredient.Quantity, item.Name);

            await ingredientService.EditIngredientToItem("Milk", 10, item.Name, 1);

            Assert.Equal(10, ingredient.Quantity);
            Assert.Equal("Milk", ingredient.Name);

            ingredientrRepository.Delete(ingredient);
        }

        [Fact]
        public async Task TestDeleteIngredientToItem()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var ingredientrRepository = new EfRepository<Ingredient>(new ApplicationDbContext(options.Options));
            var itemRepository = new EfRepository<Item>(new ApplicationDbContext(options.Options));

            ingredientrRepository.AddAsync(new Ingredient { Name = "Meat", Quantity = 5 }).GetAwaiter().GetResult();
            ingredientrRepository.SaveChangesAsync().GetAwaiter().GetResult();

            itemRepository.AddAsync(new Item { Name = "Kebapche" }).GetAwaiter().GetResult();
            itemRepository.SaveChangesAsync().GetAwaiter().GetResult();

            var item = itemRepository.All().Where(x => x.Name == "Kebapche").FirstOrDefault();
            var firstIngredient = ingredientrRepository.All().Where(x => x.Name == "Meat").FirstOrDefault();

            var ingredientService = new IngredientService(itemRepository, ingredientrRepository);

            //AutoMapperConfig.RegisterMappings(typeof(MyTestIngredient).Assembly);

            await ingredientService.AddIngredientToItem(firstIngredient.Name, firstIngredient.Quantity, item.Name);

            await ingredientService.DeleteIngredientToItem(firstIngredient.Name, firstIngredient.Quantity, item.Name, 1);
            ingredientrRepository.SaveChangesAsync().GetAwaiter().GetResult();
            Assert.True(ingredientrRepository.All().Select(x => x.Name == "Meat").FirstOrDefault());
        }

        public class MyTestIngredient : IMapFrom<Ingredient>
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public int Quantity { get; set; }
        }

        public class MyTestItem : IMapFrom<Item>
        {
            public string Name { get; set; }
        }
    }
}
