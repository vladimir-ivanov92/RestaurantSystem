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

    public class RecipeStepsServiceTests
    {
        [Fact]
        public void TestGetRecipeStepById()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                     .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var recipeStepRepository = new EfRepository<RecipeStep>(new ApplicationDbContext(options.Options));
            var itemRepository = new EfRepository<Item>(new ApplicationDbContext(options.Options));

            recipeStepRepository.AddAsync(new RecipeStep { Description = "Testing description", Minutes = 10 }).GetAwaiter().GetResult();
            recipeStepRepository.SaveChangesAsync().GetAwaiter().GetResult();

            var recipeStepService = new RecipeStepService(itemRepository, recipeStepRepository);

            AutoMapperConfig.RegisterMappings(typeof(MyTestRecipeStep).Assembly);

            var recipeStep = recipeStepService.GetById<MyTestRecipeStep>(1);

            Assert.Equal("Testing description", recipeStep.Description);
            Assert.Equal(10, recipeStep.Minutes);
        }

        [Fact]
        public async Task TestAddRecipeStepToItem()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                       .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var recipeStepRepository = new EfRepository<RecipeStep>(new ApplicationDbContext(options.Options));
            var itemRepository = new EfRepository<Item>(new ApplicationDbContext(options.Options));

            recipeStepRepository.AddAsync(new RecipeStep { Description = "Testing description", Minutes = 10 }).GetAwaiter().GetResult();
            recipeStepRepository.SaveChangesAsync().GetAwaiter().GetResult();

            itemRepository.AddAsync(new Item { Name = "Meatball" }).GetAwaiter().GetResult();
            itemRepository.SaveChangesAsync().GetAwaiter().GetResult();

            var item = itemRepository.All().Where(x => x.Name == "Meatball").FirstOrDefault();
            var recipeStep = recipeStepRepository.All().Where(x => x.Description == "Testing description").FirstOrDefault();

            var recipeStepService = new RecipeStepService(itemRepository, recipeStepRepository);

            await recipeStepService.AddRecipeStepToItem(recipeStep.Description, recipeStep.Minutes, item.Name);

            Assert.Equal(1, item.RecipeSteps.Count);
        }

        [Fact]
        public async Task TestEditRecipeStepToItem()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                         .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var recipeStepRepository = new EfRepository<RecipeStep>(new ApplicationDbContext(options.Options));
            var itemRepository = new EfRepository<Item>(new ApplicationDbContext(options.Options));

            recipeStepRepository.AddAsync(new RecipeStep { Description = "Testing description", Minutes = 10 }).GetAwaiter().GetResult();
            recipeStepRepository.SaveChangesAsync().GetAwaiter().GetResult();

            itemRepository.AddAsync(new Item { Name = "Meatball" }).GetAwaiter().GetResult();
            itemRepository.SaveChangesAsync().GetAwaiter().GetResult();

            var item = itemRepository.All().Where(x => x.Name == "Meatball").FirstOrDefault();
            var recipeStep = recipeStepRepository.All().Where(x => x.Description == "Testing description").FirstOrDefault();

            var recipeStepService = new RecipeStepService(itemRepository, recipeStepRepository);

            await recipeStepService.AddRecipeStepToItem(recipeStep.Description, recipeStep.Minutes, item.Name);

            await recipeStepService.EditRecipeToItem("Testing another description", 15, item.Name, 1);

            Assert.Equal(15, recipeStep.Minutes);
            Assert.Equal("Testing another description", recipeStep.Description);
        }

        [Fact]
        public async Task TestDeleteIngredientToItem()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                           .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var recipeStepRepository = new EfRepository<RecipeStep>(new ApplicationDbContext(options.Options));
            var itemRepository = new EfRepository<Item>(new ApplicationDbContext(options.Options));

            recipeStepRepository.AddAsync(new RecipeStep { Description = "Testing description", Minutes = 10 }).GetAwaiter().GetResult();
            recipeStepRepository.SaveChangesAsync().GetAwaiter().GetResult();

            itemRepository.AddAsync(new Item { Name = "Meatball" }).GetAwaiter().GetResult();
            itemRepository.SaveChangesAsync().GetAwaiter().GetResult();

            var item = itemRepository.All().Where(x => x.Name == "Meatball").FirstOrDefault();
            var recipeStep = recipeStepRepository.All().Where(x => x.Description == "Testing description").FirstOrDefault();

            var recipeStepService = new RecipeStepService(itemRepository, recipeStepRepository);

            await recipeStepService.AddRecipeStepToItem(recipeStep.Description, recipeStep.Minutes, item.Name);

            await recipeStepService.DeleteRecipeToItem(1);
 
            Assert.False(recipeStepRepository.All().FirstOrDefault().Description == null);
        }

        public class MyTestRecipeStep : IMapFrom<RecipeStep>
        {
            public int Id { get; set; }

            public string Description { get; set; }

            public int Minutes { get; set; }
        }

        public class MyTestItem : IMapFrom<Item>
        {
            public string Name { get; set; }
        }
    }
}
