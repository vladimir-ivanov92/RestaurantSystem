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

    public class OrdersServiceTests
    {
        [Fact]
        public void TestCheckForExistingOrder()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var orderRepository = new EfDeletableEntityRepository<Order>(new ApplicationDbContext(options.Options));
            var itemsRepository = new EfRepository<Item>(new ApplicationDbContext(options.Options));
            var orderItemRepository = new EfRepository<OrderItem>(new ApplicationDbContext(options.Options));

            orderRepository.AddAsync(new Order { UserId = "1" }).GetAwaiter().GetResult();
            orderRepository.SaveChangesAsync().GetAwaiter().GetResult();

            var orderService = new OrderService(itemsRepository, orderRepository, orderItemRepository);

            bool result = orderService.CheckForExistingOrder("1");

            Assert.True(result);
        }

        [Fact]
        public async Task TestAddItemToOrder()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var orderRepository = new EfDeletableEntityRepository<Order>(new ApplicationDbContext(options.Options));
            var itemsRepository = new EfRepository<Item>(new ApplicationDbContext(options.Options));
            var orderItemRepository = new EfRepository<OrderItem>(new ApplicationDbContext(options.Options));

            orderRepository.AddAsync(new Order { UserId = "1" }).GetAwaiter().GetResult();
            orderRepository.SaveChangesAsync().GetAwaiter().GetResult();

            itemsRepository.AddAsync(new Item { Name = "Pizza", Quantity = 5}).GetAwaiter().GetResult();
            itemsRepository.SaveChangesAsync().GetAwaiter().GetResult();

            var orderService = new OrderService(itemsRepository, orderRepository, orderItemRepository);

            await orderService.AddItemToOrder(1, "1", 10);

            var order = orderRepository.All().Where(x => x.UserId == "1").FirstOrDefault();

            Assert.Single(order.OrderItems);
        }

        [Fact]
        public async Task TestAddItemToOrderNoQuantity()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var orderRepository = new EfDeletableEntityRepository<Order>(new ApplicationDbContext(options.Options));
            var itemsRepository = new EfRepository<Item>(new ApplicationDbContext(options.Options));
            var orderItemRepository = new EfRepository<OrderItem>(new ApplicationDbContext(options.Options));

            orderRepository.AddAsync(new Order { UserId = "1" }).GetAwaiter().GetResult();
            orderRepository.SaveChangesAsync().GetAwaiter().GetResult();

            itemsRepository.AddAsync(new Item { Name = "Pizza", Quantity = 0 }).GetAwaiter().GetResult();
            itemsRepository.SaveChangesAsync().GetAwaiter().GetResult();

            var orderService = new OrderService(itemsRepository, orderRepository, orderItemRepository);

            await orderService.AddItemToOrder(1, "1", 10);

            var order = orderRepository.All().Where(x => x.UserId == "1").FirstOrDefault();

            Assert.Empty(order.OrderItems);
        }

        [Fact]
        public async Task TestAddItemToOrderOrderedQuantityIsBiggerThenItemQuantity()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var orderRepository = new EfDeletableEntityRepository<Order>(new ApplicationDbContext(options.Options));
            var itemsRepository = new EfRepository<Item>(new ApplicationDbContext(options.Options));
            var orderItemRepository = new EfRepository<OrderItem>(new ApplicationDbContext(options.Options));

            orderRepository.AddAsync(new Order { UserId = "1" }).GetAwaiter().GetResult();
            orderRepository.SaveChangesAsync().GetAwaiter().GetResult();

            itemsRepository.AddAsync(new Item { Name = "Pizza", Quantity = 5 }).GetAwaiter().GetResult();
            itemsRepository.SaveChangesAsync().GetAwaiter().GetResult();

            var orderService = new OrderService(itemsRepository, orderRepository, orderItemRepository);

            await orderService.AddItemToOrder(1, "1", 10);

            var orderItems = orderItemRepository.All().Where(x => x.ItemId == 1).FirstOrDefault();

            Assert.Equal(5, orderItems.Quantity);
        }

        [Fact]
        public async Task TestCreateOrder()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var orderRepository = new EfDeletableEntityRepository<Order>(new ApplicationDbContext(options.Options));
            var itemsRepository = new EfRepository<Item>(new ApplicationDbContext(options.Options));
            var orderItemRepository = new EfRepository<OrderItem>(new ApplicationDbContext(options.Options));

            var orderService = new OrderService(itemsRepository, orderRepository, orderItemRepository);

            await orderService.CreateOrder("1");

            Assert.Equal(1, orderRepository.All().Count());
        }
    }
}
