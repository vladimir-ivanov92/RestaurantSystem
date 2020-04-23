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

    public class DashboardsServiceTests
    {
        [Fact]
        public void TestCheckForSupply()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                   .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var itemRepository = new EfRepository<Item>(new ApplicationDbContext(options.Options));
            var orderRepository = new EfDeletableEntityRepository<Order>(new ApplicationDbContext(options.Options));
            var dashboardService = new DashboardService(itemRepository, orderRepository);

            itemRepository.AddAsync(new Item { Name = "Eggs", Quantity = 0, MenuId = 1 });
            itemRepository.SaveChangesAsync().GetAwaiter().GetResult();

            bool check = dashboardService.CheckForSupply(1);

            Assert.False(check);
        }

        [Fact]
        public async Task TestCheckForSupplyTrue()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                   .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var itemRepository = new EfRepository<Item>(new ApplicationDbContext(options.Options));
            var orderRepository = new EfDeletableEntityRepository<Order>(new ApplicationDbContext(options.Options));
            var dashboardService = new DashboardService(itemRepository, orderRepository);

            await itemRepository.AddAsync(new Item { Name = "Eggs", Quantity = 5, MenuId = 1 });
            itemRepository.SaveChangesAsync().GetAwaiter().GetResult();

            bool check = dashboardService.CheckForSupply(1);

            Assert.True(check);
        }

        [Fact]
        public void TestGetNetAmmountPerDay()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                   .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var itemRepository = new EfRepository<Item>(new ApplicationDbContext(options.Options));
            var orderRepository = new EfDeletableEntityRepository<Order>(new ApplicationDbContext(options.Options));
            var dashboardService = new DashboardService(itemRepository, orderRepository);

            DateTime date = new DateTime(2017, 1, 18);

            decimal netAmmountPerDay = dashboardService.GetNetAmmountPerDay(date);

            Assert.Equal(0.00M, netAmmountPerDay);
        }

        [Fact]
        public async Task TestGetNetAmmountPerDayToday()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                   .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var itemRepository = new EfRepository<Item>(new ApplicationDbContext(options.Options));
            var orderRepository = new EfDeletableEntityRepository<Order>(new ApplicationDbContext(options.Options));
            var dashboardService = new DashboardService(itemRepository, orderRepository);

            await orderRepository.AddAsync(new Order { NetAmount = 100.00M });
            orderRepository.SaveChangesAsync().GetAwaiter().GetResult();

            decimal netAmmountPerDay = dashboardService.GetNetAmmountPerDay(DateTime.Today);

            Assert.Equal(100.00M, netAmmountPerDay);
        }

        [Fact]
        public void TestGetOrdersPerDay()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                   .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var itemRepository = new EfRepository<Item>(new ApplicationDbContext(options.Options));
            var orderRepository = new EfDeletableEntityRepository<Order>(new ApplicationDbContext(options.Options));
            var dashboardService = new DashboardService(itemRepository, orderRepository);

            DateTime date = new DateTime(2017, 1, 18);

            int numberOfOrders = dashboardService.GetOrdersPerDay(date);

            Assert.Equal(0, numberOfOrders);
        }

        [Fact]
        public async Task TestGetOrdersPerDayToday()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                   .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var itemRepository = new EfRepository<Item>(new ApplicationDbContext(options.Options));
            var orderRepository = new EfDeletableEntityRepository<Order>(new ApplicationDbContext(options.Options));
            var dashboardService = new DashboardService(itemRepository, orderRepository);

            await orderRepository.AddAsync(new Order { NetAmount = 100.00M });
            orderRepository.SaveChangesAsync().GetAwaiter().GetResult();

            int numberOfOrders = dashboardService.GetOrdersPerDay(DateTime.Today);

            Assert.Equal(1, numberOfOrders);
        }

        [Fact]
        public async Task TestGetOrdersPerDayTodayMoreOrders()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                   .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var itemRepository = new EfRepository<Item>(new ApplicationDbContext(options.Options));
            var orderRepository = new EfDeletableEntityRepository<Order>(new ApplicationDbContext(options.Options));
            var dashboardService = new DashboardService(itemRepository, orderRepository);

            await orderRepository.AddAsync(new Order { NetAmount = 100.00M });
            await orderRepository.AddAsync(new Order { NetAmount = 200.00M });
            await orderRepository.AddAsync(new Order { NetAmount = 300.00M });
            orderRepository.SaveChangesAsync().GetAwaiter().GetResult();

            int numberOfOrders = dashboardService.GetOrdersPerDay(DateTime.Today);

            Assert.Equal(3, numberOfOrders);
        }

        [Fact]
        public async Task TestItemQuantityPerMenu()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                  .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var itemRepository = new EfRepository<Item>(new ApplicationDbContext(options.Options));
            var orderRepository = new EfDeletableEntityRepository<Order>(new ApplicationDbContext(options.Options));
            var dashboardService = new DashboardService(itemRepository, orderRepository);

            await itemRepository.AddAsync(new Item { Name = "Eggs", Quantity = 0, MenuId = 1 });
            itemRepository.SaveChangesAsync().GetAwaiter().GetResult();

            await itemRepository.AddAsync(new Item { Name = "Eggs", Quantity = 0, MenuId = 2 });
            itemRepository.SaveChangesAsync().GetAwaiter().GetResult();

            await itemRepository.AddAsync(new Item { Name = "Pizza", Quantity = 0, MenuId = 2 });
            itemRepository.SaveChangesAsync().GetAwaiter().GetResult();


            int countMenu1 = dashboardService.ItemQuantityPerMenu(1);
            int countMenu2 = dashboardService.ItemQuantityPerMenu(2);
            int countMenu3 = dashboardService.ItemQuantityPerMenu(3);

            Assert.Equal(1, countMenu1);
            Assert.Equal(2, countMenu2);
            Assert.Equal(0, countMenu3);
        }
    }
}
