namespace RestaurantSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using RestaurantSystem.Data.Models;

    public interface IOrderService
    {
        void AddItemToOrder(int itemId, string userId);

        bool CheckForExistingOrder(string userId);

        Task CreateOrder(string userId);

        int NewOrder();

        CheckResult GetUpdate(int orderId);
    }
}
