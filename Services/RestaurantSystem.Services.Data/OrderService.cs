namespace RestaurantSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using RestaurantSystem.Data.Common.Repositories;
    using RestaurantSystem.Data.Models;

    public class OrderService : IOrderService
    {
        private readonly IRepository<Item> itemsRepository;
        private readonly IDeletableEntityRepository<Order> ordersRepository;

        /// <summary>
        /// Used for SignalR.
        /// </summary>
        private readonly Random random;
        private readonly IList<int> indexes;

        private readonly string[] status =
          {
                "Preparing the ingredients",
                "Cooking",
                "Quality control",
                "Delivering...",
                "Picked up",
          };

        public OrderService(IRepository<Item> itemsRepository, IDeletableEntityRepository<Order> ordersRepository)
        {
            this.itemsRepository = itemsRepository;
            this.ordersRepository = ordersRepository;
            this.random = new Random();
            this.indexes = new List<int>();
        }

        public void AddItemToOrder(int itemId, string userId)
        {
            var order = this.ordersRepository.All().Where(x => x.UserId == userId).FirstOrDefault();
            var item = this.itemsRepository.All().Where(x => x.ItemId == itemId).FirstOrDefault();
            order.OrderedItems.Add(item);
        }

        public bool CheckForExistingOrder(string userId)
        {
            var order = this.ordersRepository.All().Where(x => x.UserId == userId).FirstOrDefault();

            if (order == null)
            {
                return false;
            }

            return true;
        }

        public void CreateOrder(string userId)
        {
            var order = new Order
            {
                UserId = userId,
                NetAmount = 1,
                User = new ApplicationUser(),
                OrderId = 1,
                Id = 1,
            };
            this.ordersRepository.AddAsync(order);
        }

        public CheckResult GetUpdate(int orderId)
        {
            Thread.Sleep(1000);
            var index = this.indexes[orderId - 1];
            if (this.random.Next(0, 4) == 2)
            {
                if (this.status.Length > this.indexes[orderId - 1])
                {
                    var result = new CheckResult
                    {
                        New = true,
                        Update = this.status[index],
                        Finished = this.status.Length - 1 == index,
                    };
                    this.indexes[orderId - 1]++;
                    return result;
                }
            }

            return new CheckResult { New = false };
        }

        public int NewOrder()
        {
            this.indexes.Add(0);
            return this.indexes.Count;
        }
    }
}
