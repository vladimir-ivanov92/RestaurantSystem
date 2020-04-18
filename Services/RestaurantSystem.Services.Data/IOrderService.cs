namespace RestaurantSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using RestaurantSystem.Data.Models;

    public interface IOrderService
    {
        Task AddItemToOrder(int itemId, string userId, int quantity);

        bool CheckForExistingOrder(string userId);

        Task CreateOrder(string userId);

        int NewOrder();

        CheckResult GetUpdate(int orderId);
    }
}
