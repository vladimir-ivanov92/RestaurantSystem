namespace RestaurantSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
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

        private  string[] status =
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
            order.OrderItems.Add(new OrderItem
            {
                ItemId = item.ItemId,
                OrderId = order.Id,
            });
            this.ordersRepository.SaveChangesAsync();
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

        public async Task CreateOrder(string userId)
        {
            var order = new Order
            {
                UserId = userId,
            };
            await this.ordersRepository.AddAsync(order);
            await this.ordersRepository.SaveChangesAsync();
        }

        public CheckResult GetUpdate(int orderId)
        {
            Thread.Sleep(1000);
            //var index = 1;
            if (true)
            {
                if (this.status.Length > 0)
                {
                    var result = new CheckResult
                    {
                        New = true,
                        Update = this.status[0],
                        Finished = this.status.Length - 1 == 0,
                    };
                    this.status = this.status.Skip(1).ToArray();
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
