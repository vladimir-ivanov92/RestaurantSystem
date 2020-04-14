namespace RestaurantSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using RestaurantSystem.Data.Common.Repositories;
    using RestaurantSystem.Data.Models;

    public class DashboardService : IDashboardService
    {
        private readonly IRepository<Item> itemsRepository;
        private readonly IDeletableEntityRepository<Order> ordersRepository;

        public DashboardService(IRepository<Item> itemsRepository, IDeletableEntityRepository<Order> ordersRepository)
        {
            this.itemsRepository = itemsRepository;
            this.ordersRepository = ordersRepository;
        }

        public bool CheckForSupply(int number)
        {
            var numberItems = this.itemsRepository.All().Where(x => x.MenuId == number).ToList();
            bool check = true;

            foreach (var item in numberItems)
            {
                if (item.Quantity == 0)
                {
                    check = false;
                }
            }

            return check;
        }

        public decimal GetNetAmmountPerDay(DateTime date)
        {
            decimal netAmmount = 0.00M;
            var numberOrders = this.ordersRepository.AllWithDeleted().Where(x => x.CreatedOn.Day.Equals(date.Day)).ToList();
            foreach (var order in numberOrders)
            {
                netAmmount += order.NetAmount;
            }
            return netAmmount;
        }

        public int GetOrdersPerDay(DateTime date)
        {
            var numberOrders = this.ordersRepository.AllWithDeleted().Where(x => x.CreatedOn.Day.Equals(date.Day)).ToList();
            return numberOrders.Count();
        }

        public int ItemQuantityPerMenu(int number)
        {
            var numberItems = this.itemsRepository.All().Where(x => x.MenuId == number).ToList();
            return numberItems.Count();
        }
    }
}
