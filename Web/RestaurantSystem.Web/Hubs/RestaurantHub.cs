namespace RestaurantSystem.Web.Hubs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.SignalR;
    using RestaurantSystem.Data.Models;
    using RestaurantSystem.Services.Data;

    public class RestaurantHub : Hub
    {
        private readonly IOrderService orderService;

        public RestaurantHub(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        public async Task GetUpdateForOrder(int orderId)
        {
            CheckResult result;
            do
            {
                result = this.orderService.GetUpdate(orderId);
                if (result.New)
                {
                    await this.Clients.Caller.SendAsync("ReceiveOrderUpdate", result.Update);
                }
            }
            while (!result.Finished);
            await this.Clients.Caller.SendAsync("Finished");
        }

    }
}
