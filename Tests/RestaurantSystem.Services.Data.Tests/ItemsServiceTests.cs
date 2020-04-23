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

    public class ItemsServiceTests
    {
        [Fact]
        public void TestGetByName()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                  .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var itemRepository = new EfRepository<Item>(new ApplicationDbContext(options.Options));
            var itemService = new ItemService(itemRepository);

            itemRepository.AddAsync(new Item { Name = "Pizza" });
            itemRepository.SaveChangesAsync().GetAwaiter().GetResult();

            AutoMapperConfig.RegisterMappings(typeof(MyTestItem).Assembly);

            var item = itemService.GetByName<MyTestItem>("Pizza");

            Assert.Equal("Pizza", item.Name);
        }

        [Fact]
        public void TestGetAll()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                  .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var itemRepository = new EfRepository<Item>(new ApplicationDbContext(options.Options));
            var itemService = new ItemService(itemRepository);

            itemRepository.AddAsync(new Item { Name = "Pizza" });
            itemRepository.AddAsync(new Item { Name = "Burger" });
            itemRepository.SaveChangesAsync().GetAwaiter().GetResult();

            AutoMapperConfig.RegisterMappings(typeof(MyTestItem).Assembly);

            var items = itemService.GetAll<MyTestItem>();

            Assert.Equal(2, items.Count());
        }

        [Fact]
        public void TestGetCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                  .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var itemRepository = new EfRepository<Item>(new ApplicationDbContext(options.Options));
            var itemService = new ItemService(itemRepository);

            itemRepository.AddAsync(new Item { Name = "Pizza" });
            itemRepository.AddAsync(new Item { Name = "Burger" });
            itemRepository.SaveChangesAsync().GetAwaiter().GetResult();

            var count = itemService.GetCount();

            Assert.Equal(2, count);
        }

        [Fact]
        public void TestGetItemsPerPage()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var itemRepository = new EfRepository<Item>(new ApplicationDbContext(options.Options));
            var itemService = new ItemService(itemRepository);

            itemRepository.AddAsync(new Item { Name = "Pizza1" });
            itemRepository.AddAsync(new Item { Name = "Pizza2" });
            itemRepository.AddAsync(new Item { Name = "Pizza3" });
            itemRepository.AddAsync(new Item { Name = "Pizza4" });
            itemRepository.AddAsync(new Item { Name = "Pizza5" });
            itemRepository.AddAsync(new Item { Name = "Pizza6" });
            itemRepository.AddAsync(new Item { Name = "Pizza7" });
            itemRepository.AddAsync(new Item { Name = "Pizza8" });
            itemRepository.AddAsync(new Item { Name = "Pizza9" });
            itemRepository.AddAsync(new Item { Name = "Pizza10" });
            itemRepository.AddAsync(new Item { Name = "Pizza11" });
            itemRepository.AddAsync(new Item { Name = "Pizza12" });
            itemRepository.AddAsync(new Item { Name = "Pizza13" });
            itemRepository.SaveChangesAsync().GetAwaiter().GetResult();

            AutoMapperConfig.RegisterMappings(typeof(MyTestItem).Assembly);

            var items = itemService.GetItemsPerPage<MyTestItem>(12,0);

            Assert.Equal(12, items.Count());
        }

        [Fact]
        public void TestGetItemsPerPageSecondPage()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var itemRepository = new EfRepository<Item>(new ApplicationDbContext(options.Options));
            var itemService = new ItemService(itemRepository);

            itemRepository.AddAsync(new Item { Name = "Pizza1" });
            itemRepository.AddAsync(new Item { Name = "Pizza2" });
            itemRepository.AddAsync(new Item { Name = "Pizza3" });
            itemRepository.AddAsync(new Item { Name = "Pizza4" });
            itemRepository.AddAsync(new Item { Name = "Pizza5" });
            itemRepository.AddAsync(new Item { Name = "Pizza6" });
            itemRepository.AddAsync(new Item { Name = "Pizza7" });
            itemRepository.AddAsync(new Item { Name = "Pizza8" });
            itemRepository.AddAsync(new Item { Name = "Pizza9" });
            itemRepository.AddAsync(new Item { Name = "Pizza10" });
            itemRepository.AddAsync(new Item { Name = "Pizza11" });
            itemRepository.AddAsync(new Item { Name = "Pizza12" });
            itemRepository.AddAsync(new Item { Name = "Pizza13" });
            itemRepository.SaveChangesAsync().GetAwaiter().GetResult();

            AutoMapperConfig.RegisterMappings(typeof(MyTestItem).Assembly);

            var items = itemService.GetItemsPerPage<MyTestItem>(12, 12);

            Assert.Single(items);
        }

        public class MyTestItem : IMapFrom<Item>
        {
            public string Name { get; set; }
        }
    }
}
