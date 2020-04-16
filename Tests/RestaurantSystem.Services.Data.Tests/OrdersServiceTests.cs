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
    }
}
